using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raccourcis : MonoBehaviour
{
    [SerializeField] GameObject joueur;
    [SerializeField] Soleil soleil;
    [SerializeField] GameObject magasin;
    [SerializeField] GameObject maison;
    private bool tabAppuye;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            transform.position = maison.gameObject.transform.position;
            Debug.Log("Joueur TP");
            gameObject.GetComponent<CharacterController>().enabled = true;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            joueur.transform.position = magasin.transform.position;
            Debug.Log("Joueur TP");
            gameObject.GetComponent<CharacterController>().enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.Ins_Inventaire.NbOeufs += 10;
        }
        else if(Input.GetKeyDown(KeyCode.O)) 
        {
            GameManager.Instance.Ins_Inventaire.NbOr += 100;
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            GameManager.Instance.Ins_Inventaire.NbChoux += 10;
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            GameManager.Instance.Ins_Inventaire.NbGraines += 10;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            //Energie 100% 
            // perd energie = false;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.Ins_Inventaire.NbOeufs = 0;
            GameManager.Instance.Ins_Inventaire.NbChoux = 0;
            GameManager.Instance.Ins_Inventaire.NbGraines = 0;
            GameManager.Instance.Ins_Inventaire.NbOr = 0;
        }else if(Input.GetKeyDown(KeyCode.Tab)) 
        {
            if(soleil.vitesse == 45.0f)
            {
                soleil.vitesse = 10.0f;
            }
            else
            {
                soleil.vitesse = 190.0f;
            }
        }
    }
}
