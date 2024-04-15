using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EtatAttraper : EtatJoueur
{
    public EtatAttraper(MouvementJoueur joueur, Animator animator, GameManager gameManager) : base(joueur, animator, gameManager) { }
    public override void Enter()
    {
        Debug.Log("Enter");
        //animator.SetBool("IsWalking", false);
        //animator.SetBool("IsWalkingFast", false);
        animator.SetBool("IsPicking", true) ;
        gameManager.VieJoueur -= ConstantesJeu.COUT_CUEILLIR * ConstantesJeu.FACTEUR_NUIT * 0.01f;
    }

    public override void Handle()
    {
    }

    public override void Exit()
    {
        animator.SetBool("IsPicking", false);
        
        //joueur.resetInteraction();
    }
}
