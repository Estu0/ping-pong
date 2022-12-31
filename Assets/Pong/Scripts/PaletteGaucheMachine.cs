using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteGaucheMachine : MonoBehaviour
{
    Transform monTransfo;
    float limiteDeplacement = 3.5f;

    Vector3 laPos;

    public GameObject balle;

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

        // Prendre position vertical de la balle
        laPos.y = balle.gameObject.transform.position.y;

        // Limite du haut
        laPos.y = Mathf.Min(laPos.y, limiteDeplacement);

        // Limite du bas
        laPos.y = Mathf.Max(laPos.y, -limiteDeplacement);

        // Modifier la position de la palette
        monTransfo.position = laPos;
    }
}
