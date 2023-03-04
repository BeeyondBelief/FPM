using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(Rigidbody))]
public class SurfaceResistance : MonoBehaviour
{
    public float resistance;

    private GroundCheck gc;
    private Rigidbody body;

    void Awake()
    {
        gc = GetComponent<GroundCheck>();
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (gc.OnGround)
            body.drag = resistance;
        else
            body.drag = 0;
    }
}
