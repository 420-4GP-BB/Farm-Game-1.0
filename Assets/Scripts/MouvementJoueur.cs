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

    void Start()
    {
        peutBouger = true;
        characterController = GetComponent<CharacterController>();
        gameManager = GameManager.Instance;
        animator = characterController.gameObject.GetComponent<Animator>();
        rotationY = transform.localRotation.eulerAngles.y;
        changerEtat(new EtatIdle(this, animator, gameManager));
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {

        deplacerEtRotate();
        definirEtat();
        

    }

    private void FixedUpdate()
    {

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
        if (peutBouger)
        {
            bool shiftAppuye = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
            bool marche = Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;

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
            }
            else
            {
                marcheRapide = false;
                changerEtat(new EtatIdle(this, animator, gameManager)); 
            }

            cliqueSouris();
        }
        else
        {
            changerEtat(new EtatIdle(this, animator, gameManager));  
        }




    }

    void cliqueSouris()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
            {
                gererClique(hit);
            }
        }

        
        if (target != null && !agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            Debug.Log("Faire action specifique");
            faireActionSpecifique();
        }
    }

    void gererClique(RaycastHit hit)
    {
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
        else if (hit.collider.CompareTag("TerrePotager"))
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

    void ramasser()
    {
        changerEtat(new EtatAttraper(this, animator, gameManager));
        resetInteraction();
        Debug.Log("Objet ramassé");
    }

    void faireActionSpecifique()
    {
        if (target.CompareTag("Oeuf"))
        {
            ramasser();
            Destroy(target);
            gameManager.Ins_Inventaire.NbOeufs++;
        }
        else if (target.CompareTag("Chou"))
        {
            Chou chou = target.GetComponent<Chou>();
            if (chou != null)
            {
                Debug.Log("Chou pret : "+ chou.estPret);
                if (!chou.estPlante)  
                {
                    Debug.Log("Planter un nouveau chou ici.");
                    planterChou();
                    chou.estPlante = true;
                    Debug.Log("Faire le plantage comme true");
                    chou.estPetit = true;

                    resetInteraction(); 
        
                    Debug.Log(chou.estPlante + " = plante");
                }
                else if (chou.estPret && chou.estPlante) 
                {
                    Debug.Log("Chou prêt à être récolté.");
                    ramasser();
                    gameManager.Ins_Inventaire.NbChoux++;
                    chou.reinitialiserObjet();
                    resetInteraction();
                }
                else
                {
                    Debug.Log("Ce chou existe mais n'est pas encore prêt à être récolté.");
                    resetInteraction();
                }
            }
        }

    }

    void planterChou()
    {
        if (gameManager.Ins_Inventaire.NbGraines > 0)
        {
            Vector3 positionPlantation = agent.destination;
            //animator.SetTrigger("Planter");

            GameObject chouGm = Instantiate(prefabChou, positionPlantation, Quaternion.identity, target.transform);

            chouGm.transform.Find("Petit").gameObject.SetActive(true);

            gameManager.Ins_Inventaire.NbGraines--;

            
            resetInteraction(); 
        }
        else
        {
            Debug.Log("Pas assez de graines pour planter");
        }

    }


    void resetInteraction()
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
