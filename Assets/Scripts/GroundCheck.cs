using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public Collider considerColliderAs;
    public LayerMask considerGroundAs;

    public bool onGround { get; private set; }


    private float height, additionalHeight;

    void Update()
    {
        height = considerColliderAs.bounds.size.y;
        additionalHeight = height * 0.1f;
        onGround = Physics.Raycast(considerColliderAs.transform.position,
                                   Vector3.down,
                                   height * 0.5f + additionalHeight,
                                   considerGroundAs);
    }
}
