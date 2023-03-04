using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask considerGroundAs;
    public float maxDistance;

    public bool OnGround { get; private set; }
    public RaycastHit Hit { get; private set; }
    public Vector3 RayDirection { get; private set; }

    private Ray downRay;
    private RaycastHit hit;

    void Update()
    {
        downRay = new Ray(transform.position, Vector3.down);
        RayDirection = transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(downRay, out hit, maxDistance, considerGroundAs))
        {
            Hit = hit;
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
            
    }
}