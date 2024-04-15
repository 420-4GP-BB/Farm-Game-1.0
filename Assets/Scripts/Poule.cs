using UnityEngine;
using UnityEngine.AI;

public class Poule : MonoBehaviour
{
    public Transform[] pointsDePatrouille; 
    public int indicePointActuel ;
    private NavMeshAgent agent;
    private float heurePonte;
    private bool pondreAjd = false;
    [SerializeField] GameObject oeufPrefab;
    private Soleil soleil;
    //private Animator animator;

    void Start()
    {
        
        soleil = FindObjectOfType<Soleil>();  // Trouver l'instance de Soleil
        if (soleil != null)
        {
            soleil.OnJourneeTerminee += planifierPonte;
            Debug.Log("Soleil trouvé");
        }
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false; 
        agent.destination = pointsDePatrouille[indicePointActuel].position;
        planifierPonte();

    }

    void AllerAuProchainPoint()
    {
        if (pointsDePatrouille.Length == 0)
            return;

        int prochainIndice;
        do
        {
            prochainIndice = Random.Range(0, pointsDePatrouille.Length);
        } while (prochainIndice == indicePointActuel); 
        indicePointActuel = prochainIndice;
        agent.destination = pointsDePatrouille[indicePointActuel].position;
    }

    void Update()
    {
        if (!agent.pathPending)
        {
            if(agent.remainingDistance < 0.5f)
            {
                AllerAuProchainPoint();
            }
        }

        if (pondreAjd && (1 - soleil.ProportionRestante) * ConstantesJeu.MINUTES_PAR_JOUR >= heurePonte)
        {
            Instantiate(oeufPrefab, transform.position, Quaternion.identity);  
            Debug.Log("Oeuf pondu");
            pondreAjd = false;
        }

    }

    void planifierPonte()
    {
        int random = Random.RandomRange(0, 2);
        Debug.Log(random);
        pondreAjd = random == 1;  
        if (pondreAjd)
        {
            Debug.Log("Ponte : " + pondreAjd);
            heurePonte = Random.Range(0, 1440); 
            Debug.Log("Heure : " + string.Format("{0:00}:{1:00}", (int)(heurePonte / 60) , (int)(heurePonte % 60)));
        }
        else
        {
            Debug.Log("Ponte : " + pondreAjd);
        }
    }
}
