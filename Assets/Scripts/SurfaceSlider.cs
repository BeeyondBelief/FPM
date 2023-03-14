using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    private Vector3 _normal;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(_normal.ToString());
        _normal = collision.contacts[0].normal;
    }

    public Vector3 Project(Vector3 direction)
    {
        return direction - Vector3.Dot(direction, _normal) * _normal;
    }
}
