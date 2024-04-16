using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllerMenu : MonoBehaviour
{
    [SerializeField] private GameObject joueur;
    [SerializeField] private GameObject affichagePanel;
    [SerializeField] private GameObject mortPanel;


    void Update()
    {
        // si on clique sur escape on revient au menu
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");

        }
        // ou si la vie du joueur est inférieure à 0 on affiche le panel de mort
        else if(GameManager.Instance.VieJoueur <= 0)
        {
            joueur.GetComponent<MouvementJoueur>().peutBouger = false;
            affichagePanel.SetActive(false);
            mortPanel.SetActive(true);
        }

    }

    // quand on clique sur le panel de mort, on revient au menu
    public void retournerMenu()
    {
        SceneManager.LoadScene("Menu");

    }

}
