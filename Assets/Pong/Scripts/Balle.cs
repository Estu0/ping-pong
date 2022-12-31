using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Balle : MonoBehaviour
{
    Transform monTransfo;
    Rigidbody2D monRB;
    AudioSource monAS;

    // position perte point
    float posPertePoint = 10.0f;

    // impulsion initiale
    public float vitesse;

    // sons
    public AudioClip autreSon;
    public AudioClip perdSon;

    // Pointage
    public TextMeshPro monTMP;
    public int pointageInitial = 10;
    int points;

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
        vitesse = 10.0f; // évite augmentation de 10%
        monTransfo.position = Vector2.zero; // réinitialise position (centre la balle)
        monRB.velocity = Vector2.zero;
        monRB.AddForce(Vector2.one * vitesse, ForceMode2D.Impulse);
        // initialiser points
        points = pointageInitial;
        monTMP.text = points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // perte de point
        if (monTransfo.position.x > posPertePoint) // si hors portée
        {
            monAS.PlayOneShot(perdSon);// son perte de point

            // mise au jeu au centre, à l'arrêt avant l'impulsion
            monTransfo.position = Vector2.zero; // réinitialise position (centre la balle)
            monRB.velocity = Vector2.zero; // Puisque force incrémentée, réinitialise chaque itération
            vitesse *= 1.1f; // Augmente vitesse de 10%
            monRB.AddForce(Vector2.one * vitesse, ForceMode2D.Impulse);

            // ajustement de points
            points--; // points = points - 1
            monTMP.text = points.ToString();

            // gestion d'écrans
            // fin de jeu
            if (points == 0) 
            {
                ecranAccueil.SetActive(true); // affiche écran accueil
                jeu.SetActive(false); // masque le jeu
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // son pour collisions de la Palette
        if (collision.transform.name == "Palette")
        {
            monAS.PlayOneShot(autreSon);
        }
        else // autres collisions
        {
            monAS.Play();
        }
    }
}