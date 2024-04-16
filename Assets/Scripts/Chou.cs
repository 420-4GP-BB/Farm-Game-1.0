using UnityEngine;

// la classe pour le chou

public class Chou : MonoBehaviour
{
    private GameObject chouActuel;
    private Soleil soleil;

    public float tempsDebutChou;
    private float tempsDebutMoyen;

    public bool estPetit = false;
    public bool estMoyen = false;
    public bool estPret = false;
    public bool estPlante;
    public int jourPlantation;


    private void Start()
    {
        soleil = FindObjectOfType<Soleil>();
        if (soleil != null)
        {
            Debug.Log("Soleil trouvé");
        }
       
    }


    private void Update()
    {
        // si le chou est plante on gere sa croissance
        if (estPlante)
        {
            gererCroissanceChou();
        }
    }

    // la methode pour gerer la croissance du chou, apres 1 jour il devient moyen, apres 1j il devient grand
    void gererCroissanceChou()
    {
        float tempsActuel = soleil.Proportion * ConstantesJeu.MINUTES_PAR_JOUR;
        int joursEcoules = soleil.Jour - jourPlantation;
        Debug.Log (joursEcoules + "=" + soleil.Jour + " - " + jourPlantation);
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

    // une methode pour changer le prefab
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
            Debug.Log("Est pret : " + estPret);
        }
    }

    // une methode pour reinitialiser le chou apres avoir ramassé
    public void reinitialiserObjet()
    {
        transform.Find("Pret").gameObject.SetActive(false);
        estPlante = false;
        estPret = false;
    }
}