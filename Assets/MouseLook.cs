using UnityEngine;

public class HeadLookFPS : MonoBehaviour
{
    public Transform head; // bone de la tête
    public Transform cameraTransform; // caméra attachée au head pivot
    public float sensitivity = 2f;

    float pitch = 0f; // haut/bas
    float yaw = 0f;   // gauche/droite

    public float yawLimit = 90f; // ±90°

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Lire la souris
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Pitch (caméra haut/bas)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -100f, 100f);

        // Yaw (tête gauche/droite)
        yaw += mouseX;
        yaw = Mathf.Clamp(yaw, -yawLimit, yawLimit);

        // Appliquer rotation sur la tête
        head.localRotation = Quaternion.Euler(pitch, -yaw, 0f);
    }
}
