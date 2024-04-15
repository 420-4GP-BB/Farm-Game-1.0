using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatIdle : EtatJoueur
{
    public EtatIdle(MouvementJoueur joueur, Animator animator, GameManager gameManager) : base(joueur, animator, gameManager) { }

    public override void Enter()
    {
        gameManager.VieJoueur -= ConstantesJeu.COUT_IMMOBILE * ConstantesJeu.FACTEUR_NUIT * 0.01f;
    }

    public override void Handle()
    {
    }

    public override void Exit()
    {
    }
}
