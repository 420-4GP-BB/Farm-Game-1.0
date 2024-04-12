using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaisonEntree : MonoBehaviour
{
    [SerializeField] GameObject affichagePanel;
    [SerializeField] GameObject maisonPanel;
    [SerializeField] MouvementJoueur joueur;
    [SerializeField] GameObject mangerOeufPanel;
    [SerializeField] GameObject mangerChouPanel;
    [SerializeField] Button mangerBtn;
    [SerializeField] Soleil soleil;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On rentre a la maison");
        if (other.CompareTag("Player"))
        {
            joueur.peutBouger = false;
            Cursor.lockState = CursorLockMode.None;
            affichagePanel.SetActive(false);
            maisonPanel.SetActive(true);
            soleil.vitesse = 0.0f;
        }
    }
    private void Update()
    {
        mettreAjourBtnManger();
    }

    public void mettreAjourBtnManger()
    {
        if(GameManager.Instance.Ins_Inventaire.NbOeufs ==0 && GameManager.Instance.Ins_Inventaire.NbChoux == 0)
        {
            mangerBtn.interactable = false;
        }
        
    }

    public void manger()
    {
        maisonPanel.SetActive(false);
        if (GameManager.Instance.Ins_Inventaire.NbOeufs > 0)
        {
            mangerOeufPanel.SetActive(true);
        }
        else if(GameManager.Instance.Ins_Inventaire.NbChoux > 0)
        {
            mangerChouPanel.SetActive(true);
        }
    }

    public void mangerOeuf()
    {
        GameManager.Instance.Ins_Inventaire.NbOeufs--;
        Cursor.lockState = CursorLockMode.Locked;
        joueur.peutBouger = true;
        mangerOeufPanel.SetActive(false);
        affichagePanel.SetActive(true);
    }

    public void mangerChou()
    {
        GameManager.Instance.Ins_Inventaire.NbChoux--;
        Cursor.lockState = CursorLockMode.Locked;
        joueur.peutBouger = true;
        mangerChouPanel.SetActive(false);
        affichagePanel.SetActive(true);
    }

    public void dormir()
    {
        // A faire
    }

    

    public void RetournerAuJeu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        joueur.peutBouger = true;
        maisonPanel.SetActive(false);
        affichagePanel.SetActive(true);
        soleil.vitesse = 10.0f;
    }
}


