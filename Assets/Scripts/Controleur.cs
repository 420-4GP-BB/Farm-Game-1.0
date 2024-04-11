using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Controleur : MonoBehaviour
{
    [SerializeField] private TMP_InputField saisieNom;
    [SerializeField] private TMP_Dropdown saisieNivDiff;
    [SerializeField] private TextMeshProUGUI oeufs;
    [SerializeField] private TextMeshProUGUI graines;
    [SerializeField] private TextMeshProUGUI ors;

    private GameManager gameManager;
    
    void Start()
    {
        gameManager = GameManager.Instance;
        saisieNom.text = gameManager.NomJoueur;
    }

    void Update()
    {
        if(saisieNivDiff != null)
        {
            switch (saisieNivDiff.value)
            {
                case 0:
                    ors.text = "200";
                    oeufs.text = "5";
                    graines.text = "5";
                    break;
                case 1:
                    ors.text = "100";
                    oeufs.text = "3";
                    graines.text = "2";
                    break;
                case 2:
                    ors.text = "50";
                    oeufs.text = "0";
                    graines.text = "2";
                    break;
                default:
                    break;
            }
        }
        
    }

    public void Quitter()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void ChargerJeu()
    {
        ChangerNom();
        ChangerInventaire();
        SceneManager.LoadScene("Ferme");
    }

    public void ChangerNom()
    {
        if(saisieNom.text != null)
        {
            gameManager.NomJoueur = saisieNom.text;
            Debug.Log(gameManager.NomJoueur);
        }
    }

    public void ChangerInventaire()
    {
        if(saisieNivDiff != null)
        {
            gameManager.Ins_Inventaire = new Inventaire(int.Parse(oeufs.text), int.Parse(graines.text), int.Parse(ors.text));
            Debug.Log(gameManager.Ins_Inventaire.NbGraines.ToString());
            Debug.Log(gameManager.Ins_Inventaire.NbOeufs.ToString());
            Debug.Log(gameManager.Ins_Inventaire.NbOr.ToString());
        }
    }
}
