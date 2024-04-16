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
        if (joueur.GetComponent<Raccourcis>().perdreEnergie == true)
            gameManager.VieJoueur -= ConstantesJeu.COUT_COURIR * 0.01 * ConstantesJeu.FACTEUR_NUIT;
    }

    public override void Handle()
    {
    }

    public override void Exit()
    {
        animator.SetBool("IsWalkingFast", false);
        animator.SetBool("IsWalking", false);
    }
}

