using UnityEngine;

public class SimpleCar : MonoBehaviour
{
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;

    public float motorForce = 1500f;
    public float steerAngle = 25f;

    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        wheelFL.motorTorque = move * motorForce;
        wheelFR.motorTorque = move * motorForce;

        wheelFL.steerAngle = turn * steerAngle;
        wheelFR.steerAngle = turn * steerAngle;
    }
}
