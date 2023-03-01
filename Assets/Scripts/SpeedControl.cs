using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]

public class SpeedControl : MonoBehaviour
{
    public float speedLimit;

    private Vector3 flatVelocity;
    private Vector3 limitedVelocity;
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        flatVelocity = new(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVelocity.magnitude > speedLimit)
        {
            limitedVelocity = flatVelocity.normalized * speedLimit;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y,
                                      limitedVelocity.z);
        }
    }
}
