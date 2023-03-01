using System;
using UnityEngine;

public class InputController: Input
{
	public static float GetHorizontalRaw()
	{
		return GetAxisRaw("Horizontal");
	}

	public static float GetVertialRaw()
	{
		return GetAxisRaw("Vertical");
	}
}
