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
    public Transform orientation;
    public float airResistance;
    float additionalForce = 10f;
    float hostizontalInput, verticalInput;
    public float moveSpeed { get; private set; }
    Vector3 modeDirection;

    MoveState moveState;
    enum MoveState
    {
        walking,
        sprinting,
        air
    }

    public Rigidbody Rbody { get; private set; }
    GroundCheck gc;
    SpeedControl sc;

    void Start()
    {
        Rbody = GetComponent<Rigidbody>();
        gc = GetComponent<GroundCheck>();
        Rbody.freezeRotation = true;
        sc = new SpeedControl(this);
    }

    void Update()
    {
        ReadInputs();
        sc.Control();
        StateHandler();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void StateHandler()
    {
        // sprint
        if (gc.onGround && Input.GetKey(sprintKey))
        {
            moveState = MoveState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (gc.onGround)
        {
            moveState = MoveState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            moveState = MoveState.air;
        }
    }

    private void ReadInputs()
    {
        hostizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        modeDirection = orientation.forward * verticalInput + orientation.right * hostizontalInput;

        Vector3 force = modeDirection.normalized * moveSpeed * additionalForce;
        if (gc.onGround)
            Rbody.AddForce(force, ForceMode.Force);
        // air
        else if (!gc.onGround)
            Rbody.AddForce(force * airResistance, ForceMode.Force);
    }
}
