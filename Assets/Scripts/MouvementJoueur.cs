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
    void Start()
    {
        peutBouger = true;
        characterController = GetComponent<CharacterController>();
        gameManager = GameManager.Instance;
        animator = characterController.gameObject.GetComponent<Animator>();
        rotationY = transform.localRotation.eulerAngles.y;
    }

    private void Update()
    {
        deplacerEtRotate();
        definirEtat();
    }

    void deplacerEtRotate()
    {
        if (peutBouger)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            Vector3 forwardMovement = transform.forward * vertical * vitesseDeplacement * Time.deltaTime;
            characterController.Move(forwardMovement);
            rotationY += horizontal;
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }
        
    }

    void definirEtat()
    {
        if (Input.GetKeyDown(KeyCode.W)){
            animator.SetBool("isWalking", true);
        }

    }

}
