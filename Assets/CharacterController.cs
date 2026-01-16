using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoveFPS : MonoBehaviour
{
    public float speed = 3f;
    public float gravity = -9.81f;

    CharacterController controller;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Lecture des axes
        float horizontal = Input.GetAxis("Horizontal"); // A/D ou Q/D
        float vertical = Input.GetAxis("Vertical");     // W/S ou Z/S

        // Déplacement relatif à la rotation de la tête (caméra) si souhaité
        Vector3 move = transform.forward * vertical + transform.right * horizontal;

        // Déplacement avec CharacterController
        controller.Move(move * speed * Time.deltaTime);

        // Appliquer la gravité
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Remettre la gravité à zéro si on touche le sol
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
    }
}
