using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public GameObject canvasTuto; // Drag ton Canvas tuto ici dans l'inspector
    private bool tutoActive = true;

    void Start()
    {
        // Active le Canvas au démarrage
        canvasTuto.SetActive(true);
    }

    void Update()
    {
        if (tutoActive && Input.anyKeyDown) // Detecte n'importe quelle touche
        {
            canvasTuto.SetActive(false); // Désactive le canvas
            tutoActive = false;
        }
    }
}
