using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalleHumHum : MonoBehaviour
{
    Transform monTransfo;
    Rigidbody2D monRB;
    AudioSource monAS;

    // positions ajout points
    float posDroiteAjoutPoint = 10.0f;
    float posGaucheAjoutPoint = -9.0f;

    // impulsion initiale
    public float vitesse = 10.0f;

    // sons
    public AudioClip autreSon;
    public AudioClip perdSon;

    // Pointage
    public TextMeshPro monTMPjoueurA;
    public TextMeshPro monTMPjoueurB;
    int pointageInitial = 0;
    public int pointageMaximal;
    int pointsA;
    int pointsB; 

    // écrans
    public GameObject ecranAccueil;
    public GameObject jeu;

    // Start is called before the first frame update
    void Awake()
    {
        // init
        monTransfo = gameObject.transform;
        monRB = gameObject.GetComponent<Rigidbody2D>();
        monAS = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // impulsion initiale
        vitesse = 5.0f; // évite augmentation de 10%
        monTransfo.position = Vector2.zero; // réinitialise position (centre la balle)
        monRB.velocity = Vector2.zero;
        monRB.AddForce(Vector2.one * vitesse, ForceMode2D.Impulse);
        // initialiser points
        pointsA = pointageInitial;
        pointsB = pointageInitial;
        monTMPjoueurA.text = pointsA.ToString();
        monTMPjoueurB.text = pointsB.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Ajout de point joueur A
        if (monTransfo.position.x > posDroiteAjoutPoint) // Score droite
        {
            monAS.PlayOneShot(perdSon);// son perte de point

            // mise au jeu au centre, à l'arrêt avant l'impulsion
            monTransfo.position = Vector2.zero; // réinitialise position (centre la balle)
            monRB.velocity = Vector2.zero; // réinitialise vélocité
            vitesse *= 1.1f; // Augmente vitesse de 10%
            monRB.AddForce(Vector2.one * vitesse, ForceMode2D.Impulse);

            // ajustement de points
            pointsA++;
            monTMPjoueurA.text = pointsA.ToString();

            // gestion d'écrans
            // fin de jeu
            if (pointsA == pointageMaximal)
            {
                ecranAccueil.SetActive(true); // affiche écran accueil
                jeu.SetActive(false); // masque le jeu
            }
        }

        // Ajout de point joueur B
        if (monTransfo.position.x < posGaucheAjoutPoint) // Score gauche
        {
            monAS.PlayOneShot(perdSon);// son perte de point

            // mise au jeu au centre, à l'arrêt avant l'impulsion
            monTransfo.position = Vector2.zero; // réinitialise position (centre la balle)
            monRB.velocity = Vector2.zero; // réinitialise vélocité
            vitesse *= 1.1f; // Augmente vitesse de 10%
            monRB.AddForce(new Vector2(-1f, 1f) * Vector2.one * vitesse, ForceMode2D.Impulse);

            // ajustement de points
            pointsB++;
            monTMPjoueurB.text = pointsB.ToString();

            // gestion d'écrans
            // fin de jeu
            if (pointsB == pointageMaximal)
            {
                ecranAccueil.SetActive(true); // affiche écran accueil
                jeu.SetActive(false); // masque le jeu
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // son pour collisions de la Palette
        if (collision.transform.name == "Palette Gauche" || collision.transform.name == "Palette Droite")
        {
            monAS.PlayOneShot(autreSon);
        }
        else // autres collisions
        {
            monAS.Play();
        }
    }
}