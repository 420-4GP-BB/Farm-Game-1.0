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
    [SerializeField] Button[] lesBoutons;
    [SerializeField] Soleil soleil;
    private double tempsDebutDormir;
    private double tempsFinDormir;
    private bool estEnTrainDeDormir = false;
    private double derniereFoisMange;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On rentre a la maison");
        if (other.CompareTag("Player"))
        {
            joueur.peutBouger = false;
            Cursor.lockState = CursorLockMode.None;
            affichagePanel.SetActive(false);
            maisonPanel.SetActive(true);
            soleil.vitesse = 10.0f;
        }
    }
    private void Update()
    {
        mettreAjourBtnManger();
        if (estEnTrainDeDormir)
        {

            double tempsActuel = soleil.Proportion * ConstantesJeu.MINUTES_PAR_JOUR;
            Debug.Log("Temps actuel: " + tempsActuel);

            if ((tempsDebutDormir < tempsFinDormir && tempsActuel >= tempsFinDormir) ||
                (tempsDebutDormir > tempsFinDormir && (tempsActuel >= tempsFinDormir && tempsActuel < tempsDebutDormir)))
            {
                FinDuSommeil();
            }
        }
    }

    // si on finit le sommeil on retourne au jeu 
    void FinDuSommeil()
    {
        estEnTrainDeDormir = false;
        foreach(Button btn in lesBoutons)
        {
            btn.interactable = true;
        }
        
        soleil.vitesse = 10.0f;
        Debug.Log("Fin du sommeil, retour au jeu");
        RetournerAuJeu();
    }

    // s'il n'a pas a manger, le bouton est desactive
    public void mettreAjourBtnManger()
    {
        if(GameManager.Instance.Ins_Inventaire.NbOeufs ==0 && GameManager.Instance.Ins_Inventaire.NbChoux == 0)
        {
            lesBoutons[0].interactable = false;
        }
        else
        {
            lesBoutons[0].interactable= true;
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
        
        if(GameManager.Instance.VieJoueur + ConstantesJeu.GAIN_ENERGIE_MANGER_OEUF >= 100)
        {
            GameManager.Instance.VieJoueur =100;
        }
        else
        {
            GameManager.Instance.VieJoueur += ConstantesJeu.GAIN_ENERGIE_MANGER_OEUF;
        }

        derniereFoisMange = soleil.Proportion * ConstantesJeu.MINUTES_PAR_JOUR;
        GameManager.Instance.Ins_Inventaire.NbOeufs--;
        joueur.peutBouger = true;
        mangerOeufPanel.SetActive(false);
        affichagePanel.SetActive(true);
    }

    public void mangerChou()
    {
        if (GameManager.Instance.VieJoueur + ConstantesJeu.GAIN_ENERGIE_MANGER_CHOU >= 100)
        {
            GameManager.Instance.VieJoueur = 100;
        }
        else
        {
            GameManager.Instance.VieJoueur += ConstantesJeu.GAIN_ENERGIE_MANGER_CHOU;
        }
        derniereFoisMange = soleil.Proportion;
        GameManager.Instance.Ins_Inventaire.NbChoux--;
        joueur.peutBouger = true;
        mangerChouPanel.SetActive(false);
        affichagePanel.SetActive(true);
    }

    // la methode pour dormir
    public void dormir()
    {
        double tempsActuel = soleil.Proportion * ConstantesJeu.MINUTES_PAR_JOUR;
        tempsDebutDormir = tempsActuel;
        tempsFinDormir = tempsActuel + 10 * 60;

        if (tempsFinDormir >= ConstantesJeu.MINUTES_PAR_JOUR)
        {
            tempsFinDormir -= ConstantesJeu.MINUTES_PAR_JOUR;
        }

        foreach(Button btn in lesBoutons)
        {
            btn.interactable = false;
        }

        estEnTrainDeDormir = true;
        soleil.vitesse = 90.0f;
        Debug.Log("Commencer � dormir � : " + tempsDebutDormir + ", fin � : " + tempsFinDormir);

        

    }

    
    // la methode pour retourner au jeu
    public void RetournerAuJeu()
    {
        joueur.peutBouger = true;
        maisonPanel.SetActive(false);
        affichagePanel.SetActive(true);
        soleil.vitesse = 10.0f;
    }
}


