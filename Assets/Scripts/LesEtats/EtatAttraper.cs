using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatAttraper : EtatJoueur
{
    public EtatAttraper(MouvementJoueur joueur, Animator animator, GameManager gameManager) : base(joueur, animator, gameManager) { }
    public override void Enter()
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsWalkingFast", false);
        animator.SetBool("IsPicking", true) ;
    }

    public override void Handle()
    {
    }

    public override void Exit()
    {
        animator.SetBool("IsPicking", false);
    }
}
