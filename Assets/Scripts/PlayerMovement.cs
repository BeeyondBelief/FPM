using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundCheck))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float walkSpeed;
    public float sprintSpeed;
    public float airResistance;
    readonly float additionalForce = 10f;
    float hostizontalInput;
    float verticalInput;
    public float MoveSpeed { get; private set; }
    Vector3 modeDirection;

    private Rigidbody Rbody;
    private GroundCheck gc;

    void Start()
    {
        Rbody = GetComponent<Rigidbody>();
        gc = GetComponent<GroundCheck>();
        Rbody.freezeRotation = true;
    }

    void Update()
    {
        ReadInputs();
        StateHandler();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void StateHandler()
    {
        if (gc.onGround && Input.GetKey(sprintKey))
        {
            MoveSpeed = sprintSpeed;
        }
        else if (gc.onGround)
        {
            MoveSpeed = walkSpeed;
        }
    }

    private void ReadInputs()
    {
        hostizontalInput = InputController.GetHorizontalRaw();
        verticalInput = InputController.GetVertialRaw();
    }

    private void Move()
    {
        modeDirection = Rbody.transform.forward * verticalInput +
                        Rbody.transform.right * hostizontalInput;

        Vector3 force = modeDirection.normalized * MoveSpeed * additionalForce;
        if (gc.onGround)
        {
            Rbody.AddForce(force, ForceMode.Force);
        }
        // air
        else if (!gc.onGround)
        {
            Rbody.AddForce(force * airResistance, ForceMode.Force);
        }
    }
}
