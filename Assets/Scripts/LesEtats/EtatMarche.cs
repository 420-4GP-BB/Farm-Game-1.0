using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// classe de l'état marche
public class EtatMarche : EtatJoueur
{
    public EtatMarche(MouvementJoueur joueur, Animator animator, GameManager gameManager) : base(joueur, animator, gameManager) { }

    public override void Enter()
    {
        animator.SetBool("IsWalking", true);
        if (joueur.GetComponent<Raccourcis>().perdreEnergie == true)
            gameManager.VieJoueur -= ConstantesJeu.COUT_MARCHER * 0.01f * ConstantesJeu.FACTEUR_NUIT;
    }

    public override void Handle()
    {
        
    }

    public override void Exit()
    {
        animator.SetBool("IsWalking", false);
    }
}
