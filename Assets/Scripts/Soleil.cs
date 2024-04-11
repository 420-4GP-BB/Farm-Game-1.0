using System;
using UnityEngine;

public class Soleil : MonoBehaviour
{
    // Il faut s'enregistrer pour �tre notifi� de la fin de la journ�e
    public event Action OnJourneeTerminee;


    // Le temps en minutes de jeu �coul�es entre deux images
    public float DeltaMinutesEcoulees
    {
        get
        {
            float proportion;
            float progression = 1 - ProportionRestante;
            if (_ancienPourcentage > progression)
            {
                proportion = 1 - _ancienPourcentage + progression;
            }
            else
            {
                proportion = progression - _ancienPourcentage;
            }

            return proportion * dureeJournee;
        }
    }

    public string GetHeure()
    {
        float proportion = (1 - ProportionRestante);
        float proportionEnMin = proportion * dureeJournee;
        int heure = (int)(proportionEnMin / 60); 
        int minute = (int)(proportionEnMin % 60); 
        string temps = string.Format("{0:00}:{1:00}", heure, minute);
        return (temps);
    }

    /// <summary>
    /// Proportion de la journ�e qui reste � �couler
    /// Valeur qui diminue, tout de suite apr�s minuit on est � 1.0 et on diminue avec le temps jusqu'� 0.0
    /// </summary>
    public float ProportionRestante => dureeJourneeRestante / dureeJournee;

    /// <summary>
    /// Indique si on est pr�sentement pendant la nuit (entre 21h et 5h am)
    /// </summary>
    public bool EstNuit => ProportionRestante >= progression21h || ProportionRestante <= progression5h;

    [Header("Rotation pour changer graduellement la direction des ombres")]
    [SerializeField] private Vector3 rotationDepart;
    [SerializeField] private Vector3 rotationFin;
    [Header("Couleurs projet�es par le soleil")]
    [SerializeField] private Color morningColor;
    [SerializeField] private Color noonColor;
    [SerializeField] private Color nightColor;
    [SerializeField] private float vitesse = 10.0f; // 10 minutes par seconde

    private Light _light;
    private float _ancienPourcentage;    
    private float dureeJournee = ConstantesJeu.MINUTES_PAR_JOUR; // 24 heures
    private float dureeJourneeRestante;

    // Pour les diff�rentes phases de la journ�e
    private const float progression21h = 21.0f / 24;
    private const float progression5h = 5.0f / 24;
    private const float progression8h = 8.0f / 24;
    private const float progression12h = 12.0f / 24;
    private const float progression18h = 18.0f / 24;

    void Awake()
    {
        _light = GetComponent<Light>();
        noonColor = _light.color;

        // On commence la premi�re journ�e � 8:00
        dureeJourneeRestante = dureeJournee * (1 - progression8h); // Il reste 16 heures de jour
        _ancienPourcentage = 1 - ProportionRestante;
    }

    // Update is called once per frame
    void Update()
    {
        // Pour le calcul du nombre de minutes �coul�es
        _ancienPourcentage = 1 - ProportionRestante;

        dureeJourneeRestante -= Time.deltaTime * vitesse;

        float progression = 1 - ProportionRestante;

        //Time.timeScale = vitesse;

        // De 22h00 � 4h00, il doit faire noir.
        // Pas assez noir � mon go�t
        if (progression >= progression21h || progression <= progression5h)
        {
            _light.color = nightColor;
            _light.intensity = 0.0f;
        }
        else if (progression < progression8h)
        {
            float pourcentage = (progression - progression5h) / (progression8h - progression5h);

            _light.color = vec2color(Vector3.Lerp(
                color2vec(nightColor),
                color2vec(morningColor),
                pourcentage));

            _light.intensity = Mathf.Lerp(0.0f, 1.0f, pourcentage);
        }
        else if (progression < progression12h)
        {
            float pourcentage = (progression - progression8h) / (progression12h - progression8h);

            _light.color = vec2color(Vector3.Lerp(
                color2vec(morningColor),
                color2vec(noonColor),
                pourcentage));

            _light.intensity = 1.0f;
        }
        else if (progression < progression18h)
        {
            float pourcentage = (progression - progression12h) / (progression18h - progression12h);
            _light.color = noonColor;
            _light.intensity = Mathf.Lerp(1, 0.6f, pourcentage);
        }
        else
        {
            float pourcentage = (progression - progression18h) / (progression21h - progression18h);

            _light.color = vec2color(Vector3.Lerp(
                color2vec(noonColor),
                color2vec(nightColor),
                pourcentage));

            _light.intensity = Mathf.Lerp(0.6f, 0.0f, pourcentage);
        }

        transform.rotation = Quaternion.Slerp(
            Quaternion.Euler(rotationDepart),
            Quaternion.Euler(rotationFin),
            progression
        );


        if (dureeJourneeRestante <= 0)
        {
            OnJourneeTerminee?.Invoke();
            dureeJourneeRestante = dureeJournee;
        }
    }



    public static Vector3 color2vec(Color color)
    {
        return new Vector3(color.r, color.g, color.b);
    }

    public static Color vec2color(Vector3 vec, float alpha = 1.0f)
    {
        return new Color(vec.x, vec.y, vec.z, alpha);
    }
}