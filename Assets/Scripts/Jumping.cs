using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(Rigidbody))]
public class Jumping : MonoBehaviour
{

	public KeyCode jumpKey = KeyCode.Space;
    public float jumpCooldown;
    public float jumpForce;

	private bool readyToJump = true;


    GroundCheck gc;
    Rigidbody Rbody;
	void Start()
	{
		gc = GetComponent<GroundCheck>();
		Rbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
        if (InputController.GetKey(jumpKey) && readyToJump && gc.onGround)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void Jump()
    {
        Rbody.velocity = new Vector3(Rbody.velocity.x, 0f, Rbody.velocity.z);
        Rbody.AddForce(Rbody.transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}

