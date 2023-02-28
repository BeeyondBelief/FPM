using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GroundCheck))]
public class SurfaceResistance : MonoBehaviour
{
    public Rigidbody Rbody;
    public float drag;

    private GroundCheck gc;

    void Start()
    {
        gc = GetComponent<GroundCheck>();
    }

    void Update()
    {
        if (gc.onGround)
            Rbody.drag = drag;
        else
            Rbody.drag = 0;
    }
}
