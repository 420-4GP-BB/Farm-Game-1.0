using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Affichage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nomJoueur;
    [SerializeField] TextMeshProUGUI nbOr;
    [SerializeField] TextMeshProUGUI nbOeuf;
    [SerializeField] TextMeshProUGUI nbGraine;
    [SerializeField] TextMeshProUGUI nbChoux;
    [SerializeField] TextMeshProUGUI nbHeure;
    [SerializeField] TextMeshProUGUI nbJour;
    [SerializeField] TextMeshProUGUI vieJoueur;
    [SerializeField] Soleil soleil;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        //soleil.OnJourneeTerminee += ajouterJour;
    }

    void Update()
    {
        nomJoueur.text = gameManager.NomJoueur.ToString();
        nbOr.text = gameManager.Ins_Inventaire.NbOr.ToString();
        nbOeuf.text = gameManager.Ins_Inventaire.NbOeufs.ToString();
        nbGraine.text = gameManager.Ins_Inventaire.NbGraines.ToString();
        nbChoux.text = gameManager.Ins_Inventaire.NbChoux.ToString();
        nbHeure.text = soleil.GetHeure();
        nbJour.text = "Jour " + soleil.Jour.ToString();
        int vie = (int)(gameManager.VieJoueur * 100);
        if(vie <= 20)
        {
            vieJoueur.color = Color.red;
        }
        vieJoueur.text = vie.ToString() +"%";


    }


   
}
