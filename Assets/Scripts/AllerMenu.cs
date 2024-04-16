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
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }else if(GameManager.Instance.VieJoueur <= 0)
        {
            joueur.GetComponent<MouvementJoueur>().peutBouger = false;
            affichagePanel.SetActive(false);
            mortPanel.SetActive(true);
        }

    }

    public void retournerMenu()
    {
        SceneManager.LoadScene("Menu");

    }

}
