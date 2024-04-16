using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


//Le script pour le magasin
public class MagasinEntree : MonoBehaviour
{
    [SerializeField] GameObject affichagePanel;
    [SerializeField] GameObject magasinPanel;
    [SerializeField] MouvementJoueur joueur;
    [SerializeField] Button boutonVendreChoux;
    [SerializeField] Button[] boutonsAcheter;
    [SerializeField] TextMeshProUGUI[] lesPrix;
    [SerializeField] Soleil soleil;
    [SerializeField] GameObject prefabPoule; 
    [SerializeField] Transform[] pointsPoule; 

    private void OnTriggerEnter(Collider other)
    {
        // si le joueur entre dans le gameobject de magasin on affiche le panel du magasin
        if (other.CompareTag("Player"))
        {
            joueur.peutBouger = false;
            Cursor.lockState = CursorLockMode.None;
            affichagePanel.SetActive(false); 
            magasinPanel.SetActive(true);
            soleil.vitesse = 0.0f;
        }
    }

    
    private void Update()
    {
        mettreAjourLesBoutons();
    }

    // une methode pour retourner au jeu
    public void RetournerAuJeu()
    {
        joueur.peutBouger = true;
        magasinPanel.SetActive(false);    
        affichagePanel.SetActive(true);
        soleil.vitesse = 10.0f;
    }

    // une methode pour mettre a jour les boutons
    private void mettreAjourLesBoutons()
    {
        majBtnVendreChoux();
        majBtnAcheter(0, int.Parse(lesPrix[0].text));
        majBtnAcheter(1, int.Parse(lesPrix[1].text));
        majBtnAcheter(2, int.Parse(lesPrix[2].text));
    }

    // une methode pour mettre a jour le bouton de vendre des chou
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

    // une methode pour mettre a jour le bouton d'Acheter pour la poule, l'oeuf et les graines
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

    public void vendre()
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

        int spawnIndex = Random.Range(0, pointsPoule.Length);
        GameObject nouvellePoule = Instantiate(prefabPoule, pointsPoule[spawnIndex].position, Quaternion.identity);
        nouvellePoule.GetComponent<Poule>().pointsDePatrouille = pointsPoule;
        nouvellePoule.GetComponent<Poule>().indicePointActuel = spawnIndex;

    }




}
