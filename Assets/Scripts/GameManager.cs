using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private string _nomJoueur;
    private Inventaire _inventaire;

    public string NomJoueur
    {
        get { return _nomJoueur; }
        set { _nomJoueur = value;}
    }

    public Inventaire Ins_Inventaire
    {
        get{return _inventaire;}
        set { _inventaire = value;}
    }

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
