using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask considerGroundAs;
    public float maxDistance;

    public bool OnGround { get; private set; }

    private Ray downRay;
    private RaycastHit hit;

    private void Update()
    {
        downRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(downRay, out hit, maxDistance, considerGroundAs))
        {
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
            
    }
}