using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MouvementJoueur : MonoBehaviour
{
    // declaration de variables et de constantes
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
    private GameObject target; // L'objet qu'on hit 
    [SerializeField] private GameObject prefabChou;
    private bool peutCourir = true;
    private bool enTrainAttraper;
    private bool enTrainPlanter;

    // initialiser pour le start
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
            ConstantesJeu.FACTEUR_NUIT = 1.0f;
            peutCourir = true;
        }
        

        

    }

    // une methode qui change l'etat, prend en parametre le nouvel état
    void changerEtat(EtatJoueur nouvelEtat)
    {
        if (etatCourant != null)
        {
            etatCourant.Exit();
        }

        etatCourant = nouvelEtat;
        etatCourant.Enter();
    }

    // une methode pour definir l'etat du joueur
    void definirEtat()
    {
        bool marche;
        // s'il peut bouger
        if (peutBouger)
        {
            bool shiftAppuye;
            // s'il peut courir donc on peut appuyer sur le shift 
            if (peutCourir)
            {
                shiftAppuye = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
            }
            else
            {
                // sinon (s'il fait nuit donc il peut pas courir)
                shiftAppuye = false;
                changerEtat(new EtatMarche(this, animator, gameManager));
            }

            //Si l'agent est activé et en n'set pas en train d'attraper ni planter
            if (agent.isActiveAndEnabled && !enTrainAttraper && !enTrainPlanter)
            {
                marche = true;
                Debug.Log("Agent = actif, marche : true, attraper : " + enTrainAttraper);
            }
            else
            {
                // on marche en prenant les touches W-S
                marche = Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;
            }

            if (shiftAppuye && marche)
            {
                // si on appuie sur shift en marchant ca va switch la marche rapide (s'il marche rapide on revient a la marche normale et vice-versa)
                marcheRapide = !marcheRapide;  
            }

            //s'il marche
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
                // si le target est l'oeuf
                if (target != null && target.gameObject.CompareTag("Oeuf"))
                {
                    // on l'attrape et on destroy
                    changerEtat(new EtatAttraper(this, animator, gameManager));
                    Destroy(target);
                    gameManager.Ins_Inventaire.NbOeufs++;
                    resetInteraction();


                }
                // si le target est chou
                else if (target != null && target.gameObject.CompareTag("Chou"))
                {
                    // on l'attrape et on reinitialise l'objet (on detruit pas)
                    changerEtat(new EtatAttraper(this, animator, gameManager));
                    gameManager.Ins_Inventaire.NbChoux++;
                    target.GetComponent<Chou>().reinitialiserObjet();
                    resetInteraction();
                }
                // si on est en train de planter
            }else if (enTrainPlanter && !marche)
            {
                // on fait l'état planter
                changerEtat(new EtatPlanter(this, animator, gameManager));
                planterChou(target);
                //StartCoroutine(attendreFinAnimation());
                resetInteraction();
            }
            else
            {
                // sinon on revient à la normale
                marcheRapide = false;
                changerEtat(new EtatIdle(this, animator, gameManager)); 
            }

            // si on clique avec la souris
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                cliqueSouris();
                
            }
        }
        else
        {
            changerEtat(new EtatIdle(this, animator, gameManager));  
        }

        // si on arrive au target on fait une action (planter ou attraper)
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

    IEnumerator attendreFinAnimation()
    {
        yield return 10.0 * Time.deltaTime;
        resetInteraction();

    }

    // si on clique sur la souris, on fait un raycast pour gérer le clique 
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
        
        // si le target est un oeuf donc on se dirige vers lui
        if (hit.collider.CompareTag("Oeuf"))
        {
            target = hit.collider.gameObject;
            allerVers(target.transform.position);
        }
        // si le target est un chou donc on se dirige vers lui 
        else if (hit.collider.CompareTag("Chou"))
        {
            target = hit.collider.gameObject;
            allerVers(target.transform.position);
        }
        

    }

    // quand on se dirige vers l'objet, on se déplace avec un NavMeshAgent et on fait l'état de marche
    void allerVers(Vector3 position)
    {
        agent.enabled = true;
        characterController.enabled = false;
        agent.destination = position;
        changerEtat(new EtatMarche(this, animator, gameManager));

        
    }

    // lorsqu'on fait une action spécifique, si c'est un oeuf on l'attrape. si c'est un chou
    // si on a de graines et que le chou n'Est pas planté, on plante, si on a pas de graines on se dirige mais on plante pas
    // sinon on attrape le chou
    void faireActionSpecifique()
    {
        if (target.CompareTag("Oeuf"))
        {
            enTrainAttraper = true;
            
        }
        else if (target.CompareTag("Chou"))
        {
            Chou chou = target.gameObject.GetComponent<Chou>();
            if (!chou.estPlante)  
                {
                if (gameManager.Ins_Inventaire.NbGraines > 0)
                {
                    enTrainPlanter = true;
                }
                else
                {
                    resetInteraction();
                }
                }
            else if (chou.estPret) 
            {
                enTrainAttraper = true;
                
            } 
            else
            {
                resetInteraction();
            }
            
        }

    }

    // La méthode pour planter le chou et activer le prefab
    void planterChou(GameObject target)
    {
        enTrainPlanter = true;
        target.transform.Find("Petit").gameObject.SetActive(true);
        target.GetComponent<Chou>().estPlante = true;
        target.GetComponent<Chou>().estPetit = true;
        target.GetComponent<Chou>().jourPlantation = soleil.Jour;
        target.GetComponent <Chou>().tempsDebutChou = soleil.Proportion * ConstantesJeu.MINUTES_PAR_JOUR;
        gameManager.Ins_Inventaire.NbGraines--;   
    }

    

    // cette méthode remet le joueur a son état idle après avoir fait une animation
    public void resetInteraction()
    {
        changerEtat(new EtatIdle(this, animator, gameManager));
        agent.enabled = false;
        characterController.enabled = true;
        target = null;
        enTrainAttraper = false;
        enTrainPlanter=false;
    }

    // une méthode pour se déplacer avec un character Controller
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
            Debug.Log(ConstantesJeu.FACTEUR_NUIT);
            Vector3 forwardMovement = transform.forward * vertical * (vitesseDeplacement * soleil.DeltaMinutesEcoulees / (10 * ConstantesJeu.FACTEUR_NUIT));
            Vector3 gravityMovement = new Vector3(0, verticalVelocity, 0);
            rotationY += horizontal * vitesseRotation * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
            characterController.Move(forwardMovement + gravityMovement);
        }

        
    }

}





