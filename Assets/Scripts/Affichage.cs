using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    [SerializeField] GameObject mortPanel;
    [SerializeField] MouvementJoueur joueur;
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
        
        if(gameManager.VieJoueur <= 0.2)
        {
            vieJoueur.color = Color.red;
        }
        else
        {
            vieJoueur.color= Color.white;
        }
        int vie = (int)(gameManager.VieJoueur * 100);
        vieJoueur.text = vie.ToString() +"%";

        


    }
   
    



}
