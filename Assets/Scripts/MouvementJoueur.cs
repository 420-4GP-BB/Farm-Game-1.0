using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MouvementJoueur : MonoBehaviour
{
    [SerializeField] private float vitesseRotation;
    [SerializeField] private float vitesseDeplacement;
    [SerializeField] private Soleil soleil;
    private CharacterController characterController;
    private NavMeshAgent agent;
    private Animator animator;
    private float rotationY;
    private GameManager gameManager;
    public bool peutBouger;
    private EtatJoueur etatCourant;
    private bool marcheRapide = false;
    private GameObject target;
    [SerializeField] private GameObject prefabChou;
    private bool peutCourir = true;
    private bool enTrainAttraper;
    private bool enTrainPlanter;

    void Start()
    {
        peutBouger = true;
        characterController = GetComponent<CharacterController>();
        gameManager = GameManager.Instance;
        animator = characterController.gameObject.GetComponent<Animator>();
        rotationY = transform.localRotation.eulerAngles.y;
        changerEtat(new EtatIdle(this, animator, gameManager));
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    private void Update()
    {

        deplacerEtRotate();
        definirEtat();

        if (soleil.EstNuit)
        {
            ConstantesJeu.FACTEUR_NUIT = 2.0f;
            peutCourir = false;
        }
        else
        {
            peutCourir = true;
        }
        

    }

    void changerEtat(EtatJoueur nouvelEtat)
    {
        if (etatCourant != null)
        {
            etatCourant.Exit();
        }

        etatCourant = nouvelEtat;
        etatCourant.Enter();
    }

    void definirEtat()
    {
        bool marche;
        if (peutBouger)
        {
            bool shiftAppuye;
            if (peutCourir)
            {
                shiftAppuye = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
            }
            else
            {
                shiftAppuye = false;
            }

            if (agent.isActiveAndEnabled && !enTrainAttraper && !enTrainPlanter)
            {
                marche = true;
                Debug.Log("Agent = actif, marche : true, attraper : " + enTrainAttraper);
            }
            else
            {
                marche = Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;
            }

            if (shiftAppuye && marche)
            {
                marcheRapide = !marcheRapide;  
            }

            if (marche)
            {
                if (marcheRapide)
                {
                    changerEtat(new EtatCourse(this, animator, gameManager));  
                }
                else
                {
                    changerEtat(new EtatMarche(this, animator, gameManager));  
                }
            }else if (enTrainAttraper && !marche)
            {
                changerEtat(new EtatAttraper(this, animator, gameManager));
                Debug.Log("Etat changé Attraper");
                Destroy(target);
                // faire une condition
                resetInteraction();
            }else if (enTrainPlanter && !marche && !enTrainAttraper)
            {
                changerEtat(new EtatPlanter(this, animator, gameManager));

            }
            else
            {
                marcheRapide = false;
                changerEtat(new EtatIdle(this, animator, gameManager)); 
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                cliqueSouris();
                
            }
        }
        else
        {
            changerEtat(new EtatIdle(this, animator, gameManager));  
        }

        if (target != null && !agent.pathPending && agent.isActiveAndEnabled)
        {
            Debug.Log(agent.remainingDistance);
            if (agent.remainingDistance <= 1.2f)
            {
                Debug.Log("Faire action specifique");


                faireActionSpecifique();
            }

        }


    }

    void cliqueSouris()
    {
       RaycastHit hit;
       if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            gererClique(hit);
        }
    }

    void gererClique(RaycastHit hit)
    {
        
        target = hit.collider.gameObject;
        
        if (hit.collider.CompareTag("Oeuf"))
        {
            target = hit.collider.gameObject;
            allerVers(target.transform.position);
        }
        else if (hit.collider.CompareTag("Chou"))
        {
            target = hit.collider.gameObject;
            allerVers(target.transform.position);
        }
        

    }

    void allerVers(Vector3 position)
    {
        agent.enabled = true;
        characterController.enabled = false;
        agent.destination = position;
        changerEtat(new EtatMarche(this, animator, gameManager));

        
    }

    //void ramasser()
    //{
      //  Debug.Log("Entrer ramasser");
        //enTrainAttraper = true;
        //Debug.Log("En train ramasser : " + enTrainAttraper);
    //}

    void faireActionSpecifique()
    {
        if (target.CompareTag("Oeuf"))
        {
            enTrainAttraper = true;
            
            gameManager.Ins_Inventaire.NbOeufs++;
        }
        else if (target.CompareTag("Chou"))
        {
            Chou chou = target.gameObject.GetComponent<Chou>();
            Debug.Log($"État du chou: estPlante = {chou.estPlante}, estPret = {chou.estPret}");
            if (!chou.estPlante)  
                {
                    planterChou(target, chou);
                    resetInteraction(); 
                    Debug.Log(chou.estPlante + " = plante");
                }
            else if (chou.estPret) 
            {
                Debug.Log("Chou prêt à être récolté.");
                enTrainAttraper = true;
                gameManager.Ins_Inventaire.NbChoux++;
                chou.reinitialiserObjet();
            } 
            else
            {
                Debug.Log("Ce chou existe mais n'est pas encore prêt à être récolté.");
                resetInteraction();
            }
            
        }

    }

    void planterChou(GameObject target, Chou chou)
    {
        if (gameManager.Ins_Inventaire.NbGraines > 0)
        {
            enTrainPlanter = true;
           

            target.transform.Find("Petit").gameObject.SetActive(true);
            chou.estPlante = true;
            Debug.Log("Faire le plantage comme true");
            chou.estPetit = true;
            gameManager.Ins_Inventaire.NbGraines--;            
        }
        else
        {
            Debug.Log("Pas assez de graines pour planter");
        }

    }


    public void resetInteraction()
    {
        changerEtat(new EtatIdle(this, animator, gameManager));
        agent.enabled = false;
        characterController.enabled = true;
        target = null;
    }


    void deplacerEtRotate()
    {
        if (peutBouger)
        {
            float verticalVelocity = 0;
            if (!characterController.isGrounded)
            {
                verticalVelocity -= 9.81f * Time.deltaTime;

            }
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            Vector3 forwardMovement = transform.forward * vertical * vitesseDeplacement * Time.deltaTime;
            Vector3 gravityMovement = new Vector3(0, verticalVelocity, 0);
            rotationY += horizontal * vitesseRotation * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
            characterController.Move(forwardMovement + gravityMovement);
        }

        
    }

}







/* commentaires : 
 * 
 * 
 if (hit.collider.gameObject.CompareTag("Oeuf"))
                {
                    agent.enabled = true;
                    characterController.enabled = false;
                    target = hit.collider.gameObject;
                    agent.destination = hit.collider.transform.position;
                    changerEtat(new EtatMarche(this, animator, gameManager));

 * 
 * 
 * 
 changerEtat(new EtatAttraper(this, animator, gameManager));
            Destroy(target);
            gameManager.Ins_Inventaire.NbOeufs++;
            target = null;
            changerEtat(new EtatIdle(this, animator, gameManager)); 
            agent.enabled = false;
            characterController.enabled = true;

  
 * 
 * 
 * */
