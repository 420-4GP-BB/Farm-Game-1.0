using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatCourse : EtatJoueur
{
    public EtatCourse(MouvementJoueur joueur, Animator animator, GameManager gameManager) : base(joueur, animator, gameManager) { }

    public override void Enter()
    {
        animator.SetBool("IsWalkingFast", true);
        animator.SetBool("IsWalking", true);
    }

    public override void Handle()
    {
        // Vitesse accrue
    }

    public override void Exit()
    {
    }
}

