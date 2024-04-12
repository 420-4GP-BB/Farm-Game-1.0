using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatMarche : EtatJoueur
{
    public EtatMarche(MouvementJoueur joueur, Animator animator, GameManager gameManager) : base(joueur, animator, gameManager) { }

    public override void Enter()
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsWalkingFast", false);
        gameManager.VieJoueur -= (int)(ConstantesJeu.COUT_MARCHER * 800.0);
        //gameManager.VieJoueur--;
    }

    public override void Handle()
    {
        
    }

    public override void Exit()
    {
    }
}
