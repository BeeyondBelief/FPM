using System;
using UnityEngine;
public class SpeedControl
{
	readonly PlayerMovement pl;
	public SpeedControl(PlayerMovement pl)
	{
		this.pl = pl;
	}

	public void Control()
	{
        Vector3 flatVelocity = new(pl.Rbody.velocity.x, 0f, pl.Rbody.velocity.z);
        if (flatVelocity.magnitude > pl.moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * pl.moveSpeed;
            pl.Rbody.velocity = new Vector3(limitedVelocity.x,
                                            pl.Rbody.velocity.y,
                                            limitedVelocity.z);
        }
    }
}

