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

    [Header("Jumping")]
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce;
    public float jumpCooldown;
    public float airFroce;
    bool readyToJump = true;


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

        // need jump
        if (Input.GetKey(jumpKey) && readyToJump && gc.onGround)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void Move()
    {
        modeDirection = orientation.forward * verticalInput + orientation.right * hostizontalInput;

        Vector3 force = modeDirection.normalized * moveSpeed * additionalForce;
        if (gc.onGround)
            Rbody.AddForce(force, ForceMode.Force);
        // air
        else if (!gc.onGround)
            Rbody.AddForce(force * airFroce, ForceMode.Force);
    }

    private void Jump()
    {
        Rbody.velocity = new Vector3(Rbody.velocity.x, 0f, Rbody.velocity.z);
        Rbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
