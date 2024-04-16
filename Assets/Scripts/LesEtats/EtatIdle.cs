using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// classe de l'�tat Idle
public class EtatIdle : EtatJoueur
{
    public EtatIdle(MouvementJoueur joueur, Animator animator, GameManager gameManager) : base(joueur, animator, gameManager) { }

    public override void Enter()
    {
        if (joueur.GetComponent<Raccourcis>().perdreEnergie == true)
            gameManager.VieJoueur -= ConstantesJeu.COUT_IMMOBILE * ConstantesJeu.FACTEUR_NUIT * 0.01f;
    }

    public override void Handle()
    {
    }

    public override void Exit()
    {
    }
}
