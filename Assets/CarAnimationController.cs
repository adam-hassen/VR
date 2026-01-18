using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimationController : MonoBehaviour
{
    [Header("Animators")]
    public Animator mascotAnimator;
    public Animator wheelAnimator;

    void Start()
    {
        mascotAnimator.SetTrigger("Drive");
    }

    void Update()
    {
        bool turnLeft = Input.GetKey(KeyCode.A);
        bool turnRight = Input.GetKey(KeyCode.D);

        mascotAnimator.SetBool("IsTurningLeft", turnLeft);
        wheelAnimator.SetBool("IsWheelLeft", turnLeft);
        mascotAnimator.SetBool("IsTurningRight", turnRight);
        wheelAnimator.SetBool("IsWheelRight", turnRight);
    }
}
