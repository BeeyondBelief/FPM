using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundCheck))]
public class FloatingBehavior : MonoBehaviour
{
    public LayerMask GroundIs;
    public float floatingHeight;
    public float springStrength = 100f;
    public float springDumper = 10f;

    private Rigidbody Rb;
    private GroundCheck Gch;
    private float rayVelocity;
    private float springForce;
    private float desiredHeight;
    
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Gch = GetComponent<GroundCheck>();
    }

    void Update()
    {
        if (Gch.OnGround)
        {
            rayVelocity = Vector3.Dot(Gch.RayDirection, Rb.velocity);
            desiredHeight = Gch.Hit.distance - floatingHeight;
            springForce = (desiredHeight * springStrength) - (rayVelocity * springDumper);
            Rb.AddForce(Gch.RayDirection * springForce);
        }
    }
}
