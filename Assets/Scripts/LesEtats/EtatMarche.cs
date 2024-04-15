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
        animator.SetBool("IsPicking", false);
        gameManager.VieJoueur -= ConstantesJeu.COUT_MARCHER;
    }

    public override void Handle()
    {
        
    }

    public override void Exit()
    {
    }
}
