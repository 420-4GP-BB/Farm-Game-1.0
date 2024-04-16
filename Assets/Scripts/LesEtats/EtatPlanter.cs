using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatPlanter : EtatJoueur
{
    public EtatPlanter(MouvementJoueur joueur, Animator animator, GameManager gameManager) : base(joueur, animator, gameManager) { }
    public override void Enter()
    {
        animator.SetBool("IsPlanting", true);

        if (joueur.GetComponent<Raccourcis>().perdreEnergie == true)
            gameManager.VieJoueur -= ConstantesJeu.COUT_PLANTER* ConstantesJeu.FACTEUR_NUIT * 0.01f;

    }

    public override void Handle()
    {
    }

    public override void Exit()
    {
        animator.SetBool("IsPlanting", false);
        //joueur.resetInteraction();
    }
}
