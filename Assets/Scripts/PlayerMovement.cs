using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(PlayerReader))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public Transform cam;
    public float speed = 7f;
    public float turnTime = 0.1f;

    private float turnSmoothVelosity;

    [Header("Jumping")]
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpCooldown;
    public float jumpForce;

    [Header("Gravity")]
    public float gravityForce = -9.81f;

    private bool readyToJump = true;
    private CharacterController controller;
    private GroundCheck gc;
    private PlayerReader playerReader;


    private Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        gc = GetComponent<GroundCheck>();
        playerReader = GetComponent<PlayerReader>();
    }

    private void Update()
    {
        playerReader.ReadInputs();

        // обновление направления движения в зависимости от направления камеры
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.eulerAngles.y, transform.eulerAngles.z);

        if (controller.isGrounded)
        {
            velocity.y = -1f;

            if (playerReader.JumpPressed)
            {
                velocity.y = jumpForce;
            }
        }
        else
        {
            velocity.y += gravityForce * Time.deltaTime;
        }

        if (playerReader.Direction.magnitude > 0.1f)
        {
            Vector3 moveDirection = transform.right * playerReader.Direction.x +
                                    transform.forward * playerReader.Direction.z;
            controller.Move(speed * Time.deltaTime * moveDirection);
        }

        controller.Move(velocity * Time.deltaTime);

    }

    //private void HandleJump()
    //{
    //    if (playerReader.JumpPressed && readyToJump && gc.OnGround)
    //    {
    //        readyToJump = false;

    //        Jump();

    //        Invoke(nameof(ResetJump), jumpCooldown);
    //    }
    //}

    //private void Jump()
    //{
    //    //body.velocity = new Vector3(body.velocity.x, 0f, body.velocity.z);
    //    //body.AddForce(body.transform.up * jumpForce, ForceMode.Impulse);
    //    if (gc.OnGround)
    //    {
    //        controller.Move(new Vector3(0f, jumpForce, 0f) * Time.deltaTime);
    //    }
        
    //}

    //private void ResetJump()
    //{
    //    readyToJump = true;
    //}
}
