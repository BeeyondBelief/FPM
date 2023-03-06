using UnityEngine;
using UnityEngine.InputSystem;


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

    [Header("Helpers")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private SurfaceSlider _slider;

    private PlayerReader _playerReader;
    private Vector3 _velocity;
    private Vector3 _slopeVelocity;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _playerReader = new PlayerReader(_playerInput);
    }

    private void Update()
    {
        _playerReader.ReadInputs();
        ApplyCameraAngles();
        ApplyGravity();
        ApplyJump();
        ApplyMove();

        Vector3 moveAlongSurface = _slider.Project(_moveDirection);
        _controller.Move(speed * Time.deltaTime * moveAlongSurface +
                        _velocity * Time.deltaTime);

    }
   

    /// <summary>
    /// Изменяет moveDirection в соответсвии с желаемым направлением движения
    /// </summary>
    private void ApplyMove()
    {
        if (_playerReader.Direction.magnitude > 0.1f)
        {
            _moveDirection = transform.right * _playerReader.Direction.x +
                            transform.forward * _playerReader.Direction.z;
        }
        else
        {
            _moveDirection = Vector3.zero;
        }
    }

    /// <summary>
    /// Изменяет velocity.y для осуществления прыжка
    /// </summary>
    private void ApplyJump()
    {
        if (_controller.isGrounded && _playerReader.JumpPressed)
        {
            _velocity.y = jumpForce;
        }
    }

    /// <summary>
    /// Изменяет velocity.y, применяя к ней гравитационные силы
    /// </summary>
    private void ApplyGravity()
    {
        if (_controller.isGrounded)
        {
            _velocity.y = -1f;
        }
        else
        {
            _velocity.y += gravityForce * Time.deltaTime;
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
