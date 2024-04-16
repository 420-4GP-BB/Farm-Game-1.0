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
        animator.SetBool("IsPicking", true) ;
        if(joueur.GetComponent<Raccourcis>().perdreEnergie == true)
        {
            gameManager.VieJoueur -= ConstantesJeu.COUT_CUEILLIR * ConstantesJeu.FACTEUR_NUIT * 0.01f;
        }
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
