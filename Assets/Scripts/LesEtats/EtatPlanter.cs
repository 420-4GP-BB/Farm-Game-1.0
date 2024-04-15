using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatPlanter : EtatJoueur
{
    public EtatPlanter(MouvementJoueur joueur, Animator animator, GameManager gameManager) : base(joueur, animator, gameManager) { }
    public override void Enter()
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsWalkingFast", false);
        animator.SetBool("IsPicking", false);
        animator.SetBool("IsPlanting", true);
    }

    public override void Handle()
    {
    }

    public override void Exit()
    {
        animator.SetBool("IsPlanting", false);
    }
}
