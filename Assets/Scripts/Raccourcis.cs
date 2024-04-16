using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raccourcis : MonoBehaviour
{
    //[SerializeField] GameObject joueur;
    [SerializeField] Soleil soleil;
    [SerializeField] GameObject magasin;
    [SerializeField] GameObject maison;
    public bool perdreEnergie = true;
    private bool tabAppuye;

    // Les raccourcis du clavier
    private void Update()
    {
        if (transform.position.y < -3)
        {
            Debug.Log("On entre");
            transform.position = maison.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            transform.position = maison.gameObject.transform.position;
            gameObject.GetComponent<CharacterController>().enabled = true;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            transform.position = magasin.transform.position;
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
            // si on clique sur L et il perd de l'énergie, il ne perd plus, si il est en train de perdre et qu'on clique sur L, il recommence a perdre
            if (perdreEnergie)
            {
                GameManager.Instance.VieJoueur = 1.0f;
                perdreEnergie = false;
            }
            else
            {
                GameManager.Instance.VieJoueur = 1.0f;
                perdreEnergie = true;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.Ins_Inventaire.NbOeufs = 0;
            GameManager.Instance.Ins_Inventaire.NbChoux = 0;
            GameManager.Instance.Ins_Inventaire.NbGraines = 0;
            GameManager.Instance.Ins_Inventaire.NbOr = 0;
            
        }
        // si on clique sur tab et que le jeu est accéléré, on le remet à la normale, sinon on l'accélère
        else if(Input.GetKeyDown(KeyCode.Tab)) 
        {
            //Time.timeScale = 45.0f;
            if(Time.timeScale == 45.0f)
            {
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale = 45.0f;
            }
        }
    }
}
