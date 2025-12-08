using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController2 : MonoBehaviour
{
    public Transform rouesAvant;
    public Transform rouesArriere;

    public float vitesse = 10f;
    public float rotationVitesse = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // avancer / reculer
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * vertical * vitesse * Time.deltaTime);

        // tourner gauche droite
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * horizontal * rotationVitesse * Time.deltaTime);

        // faire tourner les roues
        float rotationRoues = vertical * 300f * Time.deltaTime;

        if (rouesAvant != null)
            rouesAvant.Rotate(Vector3.right * rotationRoues);

        if (rouesArriere != null)
            rouesArriere.Rotate(Vector3.right * rotationRoues);
    }
}
