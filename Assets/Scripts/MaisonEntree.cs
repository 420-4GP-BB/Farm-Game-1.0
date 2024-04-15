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

    public void mettreAjourBtnManger()
    {
        if(GameManager.Instance.Ins_Inventaire.NbOeufs ==0 && GameManager.Instance.Ins_Inventaire.NbChoux == 0)
        {
            lesBoutons[0].interactable = false;
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
        soleil.vitesse = 100.0f;
        Debug.Log("Commencer à dormir à : " + tempsDebutDormir + ", fin à : " + tempsFinDormir);

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


