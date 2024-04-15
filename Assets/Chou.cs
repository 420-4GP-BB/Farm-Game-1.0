using UnityEngine;

public class Chou : MonoBehaviour
{
    private GameObject chouActuel;
    private Soleil soleil;

    private float tempsDebutChou;
    private float tempsDebutMoyen;

    public bool estPetit = false;
    public bool estMoyen = false;
    public bool estPret = false;
    public bool estPlante;
    private int jourPlantation =0;


    private void Start()
    {
        soleil = FindObjectOfType<Soleil>();  
        if (soleil != null)
        {
            Debug.Log("Soleil trouv�");
        }
        if (estPlante)
        {
            tempsDebutChou = soleil.Proportion * ConstantesJeu.MINUTES_PAR_JOUR;
            jourPlantation = soleil.Jour;

            Debug.Log("Temps debut : " + tempsDebutChou + "Jour : " + jourPlantation);
        }
    }


    private void Update()
    {
        if (estPlante)
        {
            gererCroissanceChou();
        }
    }

    void gererCroissanceChou()
    {
        float tempsActuel = soleil.Proportion * ConstantesJeu.MINUTES_PAR_JOUR;
        //Debug.Log("temps actuel " + tempsActuel);

        int joursEcoules = soleil.Jour - jourPlantation;
        if (estPetit && tempsActuel >= tempsDebutChou && joursEcoules == 1)
        {
            EvoluerVersMoyen();
            tempsDebutMoyen = tempsActuel;
            Debug.Log("Moyen");
        }

        if (estMoyen && tempsActuel >= tempsDebutChou && joursEcoules == 3)
        {
            Debug.Log("Grand");
            EvoluerVersPret();
        }
    }


    public void InitialiserChouPetit()
    {
        ChangerPrefabChou("Petit");
    }

    public void EvoluerVersMoyen()
    {
        if (!estMoyen)
        {
            ChangerPrefabChou("Moyen");
        }
    }

    public void EvoluerVersPret()
    {
        if (!estPret)
        {
            ChangerPrefabChou("Pret");
        }
    }

    private void ChangerPrefabChou(string nouveauPrefab)
    {
        if (chouActuel != null)
        {
            Destroy(chouActuel);
        }

        if (nouveauPrefab == "Petit")
        {
            estPetit = true;
            transform.Find("Petit").gameObject.SetActive(true);
        }
        else if (nouveauPrefab == "Moyen")
        {
            estPetit = false;
            estMoyen = true;
            transform.Find("Petit").gameObject.SetActive(false);
            transform.Find("Moyen").gameObject.SetActive(true);
        }
        else if (nouveauPrefab == "Pret")
        {
            estMoyen = false;
            estPret = true;
            transform.Find("Moyen").gameObject.SetActive(false);
            transform.Find("Pret").gameObject.SetActive(true);
        }
    }

    public void reinitialiserObjet()
    {
        transform.Find("Pret").gameObject.SetActive(false);
        estPlante = false;
        estPret = false;
    }
}
