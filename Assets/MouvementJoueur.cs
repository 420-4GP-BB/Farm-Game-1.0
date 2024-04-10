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
    private NavMeshAgent agent;
    private Animator animator;
    private float rotationY;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotationY = transform.localRotation.eulerAngles.y;
    }

    private void Update()
    {
        deplacerEtRotate();
    }
    void LateUpdate()
    {
        
    }

    void deplacerEtRotate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 forwardMovement = transform.forward * vertical * vitesseDeplacement * Time.deltaTime;
        characterController.Move(forwardMovement);
        rotationY += horizontal;
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
