using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkingCarController : MonoBehaviour
{
    [Header("WHEELS (WheelCollider ONLY)")]
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    [Header("CAR SETTINGS")]
    public float motorPower = 1500f;
    public float steeringAngle = 30f;
    public float brakePower = 3000f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Rigidbody SAFE SETTINGS
        rb.mass = 1200f;
        rb.drag = 0.05f;
        rb.angularDrag = 0.5f;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.centerOfMass = new Vector3(0, -0.4f, 0);

        ConfigureWheel(frontLeft);
        ConfigureWheel(frontRight);
        ConfigureWheel(rearLeft);
        ConfigureWheel(rearRight);

        Debug.Log("✅ CAR READY – PRESS Z TO MOVE");
    }

    void ConfigureWheel(WheelCollider wheel)
    {
        if (wheel == null) return;

        wheel.radius = 0.4f;
        wheel.suspensionDistance = 0.25f;
        wheel.mass = 40f;

        JointSpring spring = wheel.suspensionSpring;
        spring.spring = 35000f;
        spring.damper = 4500f;
        wheel.suspensionSpring = spring;

        WheelFrictionCurve forward = wheel.forwardFriction;
        forward.extremumSlip = 0.4f;
        forward.extremumValue = 1.2f;
        forward.asymptoteSlip = 0.8f;
        forward.asymptoteValue = 0.6f;
        forward.stiffness = 1.5f;
        wheel.forwardFriction = forward;

        WheelFrictionCurve side = wheel.sidewaysFriction;
        side.extremumSlip = 0.2f;
        side.extremumValue = 1.1f;
        side.asymptoteSlip = 0.5f;
        side.asymptoteValue = 0.75f;
        side.stiffness = 1.3f;
        wheel.sidewaysFriction = side;
    }

    void FixedUpdate()
    {
        float accel = Input.GetAxis("Vertical");   // Z / S
        float steer = Input.GetAxis("Horizontal"); // Q / D
        bool brake = Input.GetKey(KeyCode.Space);

        // STEERING
        frontLeft.steerAngle = steer * steeringAngle;
        frontRight.steerAngle = steer * steeringAngle;

        // MOTOR (REAR WHEEL DRIVE)
        rearLeft.motorTorque = accel * motorPower;
        rearRight.motorTorque = accel * motorPower;

        // BRAKE
        float brakeTorque = brake ? brakePower : 0f;
        frontLeft.brakeTorque = brakeTorque;
        frontRight.brakeTorque = brakeTorque;
        rearLeft.brakeTorque = brakeTorque;
        rearRight.brakeTorque = brakeTorque;
    }

    void OnGUI()
    {
        int grounded = 0;
        if (frontLeft.isGrounded) grounded++;
        if (frontRight.isGrounded) grounded++;
        if (rearLeft.isGrounded) grounded++;
        if (rearRight.isGrounded) grounded++;

        GUI.color = Color.green;
        GUI.Label(new Rect(10, 10, 400, 30), $"🚗 CAR ACTIVE – {grounded}/4 WHEELS GROUNDED");
        GUI.Label(new Rect(10, 40, 400, 30), $"SPEED: {(rb.velocity.magnitude * 3.6f):F0} km/h");
        GUI.Label(new Rect(10, 70, 500, 30), "Z=ACCEL | S=REVERSE | Q/D=STEER | SPACE=BRAKE");
    }
}
