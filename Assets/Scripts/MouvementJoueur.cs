using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class MouvementJoueur : MonoBehaviour
{
    [SerializeField] private float vitesseRotation;
    [SerializeField] private float vitesseDeplacement;
    private CharacterController characterController;
    //private NavMeshAgent agent;
    private Animator animator;
    private float rotationY;
    private GameManager gameManager;
    public bool peutBouger;
    private EtatJoueur etatCourant;
    private bool marcheRapide = false;

    void Start()
    {
        
        peutBouger = true;
        characterController = GetComponent<CharacterController>();
        gameManager = GameManager.Instance;
        animator = characterController.gameObject.GetComponent<Animator>();
        rotationY = transform.localRotation.eulerAngles.y;
        changerEtat(new EtatIdle(this, animator, gameManager));
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
                changerEtat(new EtatIdle(this, animator, gameManager)); 
            }
        }
        else
        {
            changerEtat(new EtatIdle(this, animator, gameManager));  // Assurer l'état idle si ne peut pas bouger
        }




    }
    void deplacerEtRotate()
    {
        if (peutBouger)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            Vector3 forwardMovement = transform.forward * vertical * vitesseDeplacement * Time.deltaTime;
            rotationY += horizontal * vitesseRotation * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
            characterController.Move(forwardMovement);
        }

    }

    

}
