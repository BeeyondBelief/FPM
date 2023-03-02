using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBehavior : MonoBehaviour
{
    public LayerMask GroundIs;
    public float floatingHeight;
    public float springStrength = 1f;
    public float springDumper = 1f;
    public float drag = 5f;

    Rigidbody Rb;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, Vector3.down);

        Rb.drag = drag;

        if (Physics.Raycast(downRay, out hit))
        {
            Vector3 rayDirection = transform.TransformDirection(Vector3.down);
            //Vector3 otherVel = Vector3.zero;
            //Rigidbody hitBody = hit.rigidbody;
            //if (hitBody != null)
            //{
            //    otherVel = hitBody.velocity;
            //}
            float rayDirVel = Vector3.Dot(rayDirection, Rb.velocity);
            float x = hit.distance - floatingHeight;
            float springForce = (x * springStrength) - (rayDirVel * springDumper);
            Rb.AddForce(rayDirection * springForce);
            //if (hitBody != null)
            //{
            //    hitBody.AddForceAtPosition(rayDirection * -springForce, hit.point);
            //}
        }
    }
}
