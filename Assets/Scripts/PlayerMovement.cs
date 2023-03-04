using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public Transform cam;
    public float speed = 7f;
    public float turnTime = 0.1f;
    public bool rotateWhileMoving = true;
    public bool rotateWithCamera = false;

    private float turnSmoothVelosity;

    [Header("Jumping")]
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpCooldown;
    public float jumpForce;

    private bool readyToJump = true;
    private Rigidbody body;
    private GroundCheck gc;
    private PlayerInput playerInput;

    private Vector2 directionFlat;
    private Vector3 direction;
    private float horizontal;
    private float vertial;
    private bool jumpPressed;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        gc = GetComponent<GroundCheck>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        ReadInputs();
        handleJump();
    }

    private void ReadInputs()
    {
        directionFlat = playerInput.actions["Move"].ReadValue<Vector2>();
        direction = new Vector3(directionFlat.x, 0f, directionFlat.y).normalized;
        jumpPressed = playerInput.actions["Jump"].triggered;
    }

    void FixedUpdate()
    {
        if (rotateWithCamera)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            body.AddForce(speed * 100f * Time.deltaTime * direction, ForceMode.Acceleration);
            if (rotateWhileMoving)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                                                    targetAngle,
                                                    ref turnSmoothVelosity,
                                                    turnTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }

    private void handleJump()
    {
        if (jumpPressed && readyToJump && gc.OnGround)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void Jump()
    {
        body.velocity = new Vector3(body.velocity.x, 0f, body.velocity.z);
        body.AddForce(body.transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
