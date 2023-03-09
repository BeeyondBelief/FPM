using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerReader
{
    private PlayerInput inp;

    private Vector2 directionFlat;
    public Vector3 Direction { get; private set; }
    public bool JumpPressed { get; private set; }

    public PlayerReader(PlayerInput inp)
    {
        this.inp = inp;
    }

    public Vector3 ReadDirection()
    {
        directionFlat = inp.actions["Move"].ReadValue<Vector2>();
        Direction = new Vector3(directionFlat.x, 0f, directionFlat.y).normalized;
        return Direction;
    }

    public bool ReadJump()
    {
        JumpPressed = inp.actions["Jump"].IsPressed();
        return JumpPressed;
    }

    public void ReadInputs()
    {
        ReadDirection();
        ReadJump();
    }
}
