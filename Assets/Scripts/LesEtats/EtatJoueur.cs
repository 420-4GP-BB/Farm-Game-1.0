using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EtatJoueur
{
    protected Animator animator;
    protected MouvementJoueur joueur;
    protected GameManager gameManager;

    public EtatJoueur(MouvementJoueur joueur, Animator animator, GameManager gameManager)
    {
        this.joueur = joueur;
        this.animator = animator;
        this.gameManager = gameManager;
    }

    public abstract void Enter();
    public abstract void Handle();
    public abstract void Exit();
}
