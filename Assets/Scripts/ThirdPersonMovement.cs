using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public Rigidbody body;
    public Transform cam;
    public float speed = 7f;
    public float turnTime = 0.1f;

    private float turnSmoothVelosity;

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertial = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertial).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                                                targetAngle,
                                                ref turnSmoothVelosity,
                                                turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            body.AddForce(speed * 100f * Time.deltaTime * direction, ForceMode.Acceleration);
        }

    }
}
