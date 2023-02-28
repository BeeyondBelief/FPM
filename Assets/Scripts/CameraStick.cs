using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStick : MonoBehaviour
{
    public Transform cameraPosition;

    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
