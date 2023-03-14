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

    private PlayerReader _playerReader;
    private float _ySpeed;
    private Vector3 _velocity;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _playerReader = new PlayerReader(_playerInput);
    }

    private void Update()
    {
        _playerReader.ReadInputs();
        ApplyCameraAngles();
        ApplyGravityAndJump();
        ApplyMove();

        _velocity = AdjustVelocity(_moveDirection.magnitude * speed * _moveDirection.normalized);
        _velocity.y += _ySpeed;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private Vector3 AdjustVelocity(Vector3 velocity)
    {
        Ray ray = new(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, _controller.height))
        {
            Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Vector3 adjusted = slopeRotation * velocity;
            if (adjusted.y < 0)
            {
                return adjusted;
            }
        }
        return velocity;
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
            _moveDirection.Normalize();
        }
        else
        {
            _moveDirection = Vector3.zero;
        }
    }

    /// <summary>
    /// Изменяет velocity.y, применяя к ней гравитационные силы, также
    /// обрабатывает прыжок
    /// </summary>
    private void ApplyGravityAndJump()
    {
        if (_controller.isGrounded)
        {
            if (_playerReader.JumpPressed)
            {
                _ySpeed = jumpForce;
            }
            else
            {
                _ySpeed = -1f;
            }
        }
        else
        {
            _ySpeed += gravityForce * Time.deltaTime;
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
