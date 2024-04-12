using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private string _nomJoueur;
    private Inventaire _inventaire;
    private int _vieJoueur;

    public int VieJoueur
    {
        get { return _vieJoueur; }
        set { _vieJoueur = value;}
    }

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


    private void Awake()
    {
        // On vérifie si c'est la première fois que la variable statique est affectée
        // et on s'assure que l'objet ne sera pas détruit lors du chargement d'une autre scène
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);     // La source citée plus haut fait Destroy(gameObject) ce qui semble suspect.
        }
    }
}
