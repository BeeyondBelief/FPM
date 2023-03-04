using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GroundCheck))]
public class AirResistance : MonoBehaviour
{
    
    public float resistance;

    private Rigidbody Rbody;
    private GroundCheck gc;

    void Start()
    {
        gc = GetComponent<GroundCheck>();
        Rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        float hostizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Transform orientation = Rbody.transform;
        Vector3 modeDirection = orientation.forward * verticalInput + orientation.right * hostizontalInput;

        Vector3 force = modeDirection.normalized * resistance;
        if (!gc.OnGround)
            Rbody.AddForce(-force, ForceMode.Force);
    }
}
