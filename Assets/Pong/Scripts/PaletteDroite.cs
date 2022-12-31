using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteDroite : MonoBehaviour
{
    Transform monTransfo;
    float limiteDeplacement = 3.5f;

    public float vitesseDeplacement = 1.0f;
    Vector3 laPos;

    // Start is called before the first frame update
    void Start()
    {
        monTransfo = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Prendre la position de la palette
        laPos = monTransfo.position;

        // Calcul le déplacement
        laPos.y += (vitesseDeplacement * Time.deltaTime) * Input.GetAxis("VerticalDroite");

        // Limite du haut
        laPos.y = Mathf.Min(laPos.y, limiteDeplacement);

        // Limite du bas
        laPos.y = Mathf.Max(laPos.y, -limiteDeplacement);

        // Modifier la position de la palette
        monTransfo.position = laPos;
    }
}
