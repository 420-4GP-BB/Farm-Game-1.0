using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatIdle : EtatJoueur
{
    public EtatIdle(MouvementJoueur joueur, Animator animator, GameManager gameManager) : base(joueur, animator, gameManager) { }

    public override void Enter()
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsWalkingFast", false) ;
        gameManager.VieJoueur -= ConstantesJeu.COUT_IMMOBILE;
    }

    public override void Handle()
    {
    }

    public override void Exit()
    {
    }
}
