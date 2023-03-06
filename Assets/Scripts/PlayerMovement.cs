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

    [Header("Jumping")]
    public float jumpCooldown;
    public float jumpForce;

    [Header("Gravity")]
    public float gravityForce = -9.81f;

    private CharacterController controller;
    private GroundCheck gc;
    private PlayerReader playerReader;

    private Vector3 velocity;
    private Vector3 moveDirection;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        gc = GetComponent<GroundCheck>();
        playerReader = GetComponent<PlayerReader>();
    }

    private void Update()
    {
        playerReader.ReadInputs();
        ApplyCameraAngles();
        ApplyGravity();
        ApplyJump();
        ApplyMove();

        controller.Move(speed * Time.deltaTime * moveDirection +
                        velocity * Time.deltaTime);

    }

    /// <summary>
    /// Изменяет moveDirection в соответсвии с желаемым направлением движения
    /// </summary>
    private void ApplyMove()
    {
        if (playerReader.Direction.magnitude > 0.1f)
        {
            moveDirection = transform.right * playerReader.Direction.x +
                            transform.forward * playerReader.Direction.z;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }

    /// <summary>
    /// Изменяет velocity.y для осуществления прыжка
    /// </summary>
    private void ApplyJump()
    {
        if (controller.isGrounded && playerReader.JumpPressed)
        {
            velocity.y = jumpForce;
        }
    }

    /// <summary>
    /// Изменяет velocity.y, применяя к ней гравитационные силы
    /// </summary>
    private void ApplyGravity()
    {
        if (controller.isGrounded)
        {
            velocity.y = -1f;
        }
        else
        {
            velocity.y += gravityForce * Time.deltaTime;
        }
    }

    /// <summary>
    /// Обновляет направление движения в зависимости от направления камеры
    /// </summary>
    private void ApplyCameraAngles()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.eulerAngles.y, transform.eulerAngles.z);
    }
}
