using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MagasinEntree : MonoBehaviour
{
    [SerializeField] GameObject affichagePanel;
    [SerializeField] GameObject magasinPanel;
    [SerializeField] MouvementJoueur joueur;
    [SerializeField] Button boutonVendreChoux;
    [SerializeField] Button[] boutonsAcheter;
    [SerializeField] TextMeshProUGUI[] lesPrix;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            joueur.peutBouger = false;
            affichagePanel.SetActive(false); // Désactiver l'affichage actuel
            magasinPanel.SetActive(true);    // Activer l'affichage du magasin
        }
    }

    private void Update()
    {
        mettreAjourLesBoutons();
    }

    public void RetournerAuJeu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        joueur.peutBouger = true;
        magasinPanel.SetActive(false);    // Désactiver l'affichage du magasin
        affichagePanel.SetActive(true); // Activer l'affichage actuel
    }

    private void mettreAjourLesBoutons()
    {
        majBtnVendreChoux();
        majBtnAcheter(0, int.Parse(lesPrix[0].text));
        majBtnAcheter(1, int.Parse(lesPrix[1].text));
        majBtnAcheter(2, int.Parse(lesPrix[2].text));
    }

    private void majBtnVendreChoux()
    {
        if(GameManager.Instance.Ins_Inventaire.NbChoux > 0)
        {
            boutonVendreChoux.interactable = true;
        }
        else
        {
            boutonVendreChoux.interactable= false;
        }
    }

    private void majBtnAcheter(int pos, int prix)
    {
        if(GameManager.Instance.Ins_Inventaire.NbOr >= prix)
        {
            boutonsAcheter[pos].interactable = true;
        }
        else
        {
            boutonsAcheter[pos].interactable= false;
        }
    }

    private void vendre()
    {
        GameManager.Instance.Ins_Inventaire.NbOr += int.Parse(lesPrix[3].text);
        GameManager.Instance.Ins_Inventaire.NbChoux--;
    }

    public void acheterGraines()
    {
        GameManager.Instance.Ins_Inventaire.NbOr -= int.Parse(lesPrix[2].text);
        GameManager.Instance.Ins_Inventaire.NbGraines++;
    }

    public void acheterOeufs()
    {
        GameManager.Instance.Ins_Inventaire.NbOr -= int.Parse(lesPrix[0].text);
        GameManager.Instance.Ins_Inventaire.NbOeufs++;
    }

    public void acheterPoules()
    {
        GameManager.Instance.Ins_Inventaire.NbOr -= int.Parse(lesPrix[1].text);
        GameManager.Instance.Ins_Inventaire.NbPoules++;

        //Faire le tp d'une poule dans la ferme
    }




}
