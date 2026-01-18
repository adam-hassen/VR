using UnityEngine;

public class Buggy : MonoBehaviour
{
    [Header("Car Settings")]
    public float motorPower = 1500f;

    public Wheel[] wheels;

    private float horInput;
    private float verInput;
    private Rigidbody rb;

    [Header("Engine Sound")]
    public AudioSource engineAudio;
    public float minVolume = 0.15f;
    public float maxVolume = 0.8f;
    public float maxSpeedForSound = 100f; // km/h


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

        if (engineAudio != null)
        {
            engineAudio.loop = true;
            engineAudio.volume = 0f;
        }
        else
        {
            Debug.LogError("❌ Engine AudioSource non assignée !");
        }

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

        HandleEngineSound();
    }

    void OnGUI()
    {
        float speed = rb.velocity.magnitude * 3.6f;
        GUI.Label(new Rect(20, 20, 300, 30), $"Speed : {speed:F1} km/h");
    }

    void HandleEngineSound()
    {
        if (engineAudio == null || rb == null) return;

        float speed = rb.velocity.magnitude * 3.6f; // km/h
        bool accelerating = Mathf.Abs(verInput) > 0.05f;

        if (accelerating)
        {
            if (!engineAudio.isPlaying)
                engineAudio.Play();

            float speedRatio = Mathf.Clamp01(speed / maxSpeedForSound);

            // Volume évolue avec la vitesse
            engineAudio.volume = Mathf.Lerp(minVolume, maxVolume, speedRatio);

            // Pitch moteur réaliste
            engineAudio.pitch = Mathf.Lerp(0.9f, 1.5f, speedRatio);
        }
        else
        {
            // Fade out doux
            engineAudio.volume = Mathf.Lerp(engineAudio.volume, 0f, Time.deltaTime * 4f);

            if (engineAudio.volume < 0.02f)
                engineAudio.Stop();
        }
    }

}
