using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveMaison : MonoBehaviour
{
    [SerializeField] GameObject affichagePanel;
    [SerializeField] GameObject maisonPanel;
    [SerializeField] MouvementJoueur joueur;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            joueur.peutBouger = false;
            affichagePanel.SetActive(false); // Désactiver l'affichage actuel
            maisonPanel.SetActive(true);    // Activer l'affichage du magasin
        }
    }
}
