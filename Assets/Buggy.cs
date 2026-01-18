using UnityEngine;

public class Buggy : MonoBehaviour
{
    [Header("Car Settings")]
    public float motorPower = 1500f;

    public Wheel[] wheels;

    private float horInput;
    private float verInput;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Voiture normale
        rb.useGravity = true;
        rb.drag = 0f;
        rb.angularDrag = 0.5f;

        // Stabilité
        rb.constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        horInput = Input.GetAxis("Horizontal");
        verInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horInput) < 0.01f) horInput = 0f;
        if (Mathf.Abs(verInput) < 0.01f) verInput = 0f;
    }

    void FixedUpdate()
    {
        foreach (Wheel w in wheels)
        {
            w.Steer(horInput);
            w.Accelerate(verInput * motorPower);
            w.UpdateVisual();
        }
    }

    void OnGUI()
    {
        float speed = rb.velocity.magnitude * 3.6f;
        GUI.Label(new Rect(20, 20, 300, 30), $"Speed : {speed:F1} km/h");
    }
}
