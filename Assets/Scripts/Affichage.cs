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
        int vie = (int)(gameManager.VieJoueur * 100);
        if(vie <= 20)
        {
            vieJoueur.color = Color.red;
        }
        vieJoueur.text = vie.ToString() +"%";

        if(vie <= 0)
        {
            joueur.peutBouger = false;
            //Cursor.lockState = CursorLockMode.None;
            gameObject.SetActive(false);
            mortPanel.SetActive(true);
            soleil.vitesse = 0.0f;
            StartCoroutine(retournerMenu());
            SceneManager.LoadScene("Ferme");
        }


    }
    IEnumerator retournerMenu()
    {
        yield return new WaitForSeconds(3);  // Donne 3 secondes pour que le joueur voie le panneau de mort
        SceneManager.LoadScene("MenuScene");  // Assurez-vous que le nom de la scène du menu est correct
    }



}
