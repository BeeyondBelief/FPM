using System;
using UnityEngine;

class GroundControl
{
    public bool onGround { get; private set; }

    private readonly float additionalHeight = 0.2f;
    readonly PlayerMovement pl;
    public GroundControl(PlayerMovement pl)
    {
        this.pl = pl;
    }
    public void Control()
    {
        onGround = Physics.Raycast(pl.transform.position, Vector3.down,
                                   pl.playerHeight * 0.5f + additionalHeight,
                                   pl.ground);
        if (onGround)
            pl.Rbody.drag = pl.groundDrag;
        else
            pl.Rbody.drag = 0;
    }
}

