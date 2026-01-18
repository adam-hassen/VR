using UnityEngine;

public class Wheel : MonoBehaviour
{
    [Header("Wheel Type")]
    public bool powered = false;   // moteur
    public bool steering = false;  // direction

    [Header("Steering")]
    public float maxAngle = 30f;

    private WheelCollider wcol;
    private Transform wmesh;

    void Start()
    {
        wcol = GetComponentInChildren<WheelCollider>();
        wmesh = transform.Find("mesh_Wheel");
    }

    public void Steer(float input)
    {
        if (steering)
            wcol.steerAngle = input * maxAngle;
        else
            wcol.steerAngle = 0f;
    }

    public void Accelerate(float power)
    {
        if (powered)
            wcol.motorTorque = power;
        else
            wcol.motorTorque = 0f;
    }

    // 🔥 LA CLÉ : Unity gère TOUT (steering + rotation)
    public void UpdateVisual()
    {
        Vector3 pos;
        Quaternion rot;

        wcol.GetWorldPose(out pos, out rot);
        wmesh.position = pos;
        wmesh.rotation = rot;
    }
}
