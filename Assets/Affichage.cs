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
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        nomJoueur.text = gameManager.NomJoueur.ToString();
        nbOr.text = gameManager.Ins_Inventaire.NbOr.ToString();
        nbOeuf.text = gameManager.Ins_Inventaire.NbOeufs.ToString();
        nbGraine.text = gameManager.Ins_Inventaire.NbGraines.ToString();
        nbChoux.text = gameManager.Ins_Inventaire.NbChoux.ToString();
    }
}
