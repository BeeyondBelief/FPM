using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Game;
using UnityEditor;


namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {

        [Header("Movement")]
        public CinemachineBrain cam;
        public float crouchSpeed = 3f;
        public float speed = 7f;
        public float runSpeed = 15f;

        [Header("Jumping")]
        public float jumpForce = 7f;

        [Header("Gravity")]
        public float gravityForce = -9.81f;

        [Header("Physics")] 
        [SerializeField] private float _playerMass = 50f;

        [Header("Helpers")]
        [SerializeField] private CharacterController _controller;
        [SerializeField] private PlayerInput _playerInput;

        private PlayerReader _playerReader;
        private float _ySpeed;
        private float _currentSpeed;
        private float _characterNormalHeight;
        private Vector3 _characterCenter;
        private Vector3 _velocity;
        
        private void OnValidate()
        {
            var foundCamera = FindObjectOfType<Camera>();
            if (foundCamera is null)
            {
                EditorApplication.delayCall += () =>
                {
                    var cameraObject = new GameObject("CameraRoot");
                    cameraObject.AddComponent<Camera>();
                    cameraObject.AddComponent<AudioListener>();
                    cameraObject.AddComponent<CinemachineBrain>();
                    cam = cameraObject.GetComponent<CinemachineBrain>();
                };
            }
            else
            {
                cam = foundCamera.GetComponent<CinemachineBrain>();
            }
        }

        private void Awake()
        {
            GameSettings.onGamePaused += OnGamePause;
            GameSettings.onGameResumed += OnGameResumed;
            _playerReader = new PlayerReader(_playerInput);
            _characterNormalHeight = _controller.height;
            _characterCenter = _controller.center;
            _currentSpeed = speed;
        }
        
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            var body = hit.collider.attachedRigidbody;
            if (body == null || body.isKinematic)
                return;

            // Не применять силу к объектам на которых стоим
            if (hit.moveDirection.y < -0.3f)
                return;
            //Adds force to the object
            body.AddForce(_velocity * _playerMass * Time.deltaTime, ForceMode.Impulse);
        }

        private void OnGamePause()
        {
            gameObject.SetActive(false);
            cam.enabled = false;
        }

        private void OnGameResumed()
        {
            gameObject.SetActive(true);
            cam.enabled = true;
        }

        private void Update()
        {
            _playerReader.ReadInputs();
            ApplyCameraAngles();
            ApplyGravityAndJump();
            HandleCrouching();
            UpdateSpeed();
            
            var moveDirection = GetMovementDirection();
            _velocity = AdjustVelocity(moveDirection.magnitude * _currentSpeed * moveDirection.normalized);
            _velocity.y += _ySpeed;
            _controller.Move(_velocity * Time.deltaTime);
        }

        private void HandleCrouching()
        {
            if (!_playerReader.CrouchPressed)
            {
                _controller.height = _characterNormalHeight;
                _controller.center = _characterCenter;
            }
            else
            {
                _controller.height = _characterNormalHeight / 2;
                _controller.center = new Vector3(_characterCenter.x, _characterCenter.y / 2, _characterCenter.z);
            }
        }

        private void UpdateSpeed()
        {
            if (!_controller.isGrounded)
            {
                return;
            }
            if (_playerReader.RunPressed)
            {
                _currentSpeed = runSpeed;
            }
            else if (_playerReader.CrouchPressed)
            {
                _currentSpeed = crouchSpeed;
            }
            else
            {
                _currentSpeed = speed;   
            }
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
        /// Возвращает вектор направления движения
        /// </summary>
        private Vector3 GetMovementDirection()
        {
            if (_playerReader.Direction.magnitude < _controller.minMoveDistance)
            {
                return Vector3.zero;
            }
            var moveDirection = transform.right * _playerReader.Direction.x +
                                      transform.forward * _playerReader.Direction.z;
            moveDirection.Normalize();
            return moveDirection;
        }

        /// <summary>
        /// Изменяет вертикальную скорость в зависимости от гравитации и прыжка.
        /// </summary>
        private void ApplyGravityAndJump()
        {
            if (!_controller.isGrounded)
            {
                _ySpeed += gravityForce * Time.deltaTime;
                return;
            }
            if (_playerReader.JumpPressed)
            {
                _ySpeed = jumpForce;
            }
            else
            {
                _ySpeed = -1f;
            }
        }

        /// <summary>
        /// Обновляет направление движения в зависимости от направления камеры
        /// </summary>
        private void ApplyCameraAngles()
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}