using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Game;
using Sound;

namespace Player
{
    internal enum MoveState
    {
        Base, Run, Crouch
    }
    public class PlayerMovement : MonoBehaviour
    {

        [Header("Movement")]
        public CinemachineBrain cam;
        public float crouchSpeed = 3f;
        public float speed = 7f;
        public float runSpeed = 15f;
        private MoveState _moveState = MoveState.Base;

        [Header("Jumping")]
        public float jumpForce = 7f;

        [Header("Gravity")]
        public float gravityForce = -9.81f;

        [Header("Physics")] 
        [SerializeField] private float _playerMass = 50f;

        [Header("Sounds")]
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private float _footStepOffset = 0.5f;
        [SerializeField] private float _footStepRunOffset = 0.3f;
        [SerializeField] private float _footStepCrouchOffset = 0.9f;
        private float _footStepTimer = 0;

        [Header("Helpers")]
        [SerializeField] private CharacterController _controller;
        [SerializeField] private PlayerInput _playerInput;
        
        public float CurrentSpeed { get; private set; }

        private PlayerReader _playerReader;
        private float _ySpeed;
        private float _characterNormalHeight;
        private Vector3 _characterCenter;
        private Vector3 _velocity;

        private void Awake()
        {
            GameSettings.onGamePaused += OnGamePause;
            GameSettings.onGameResumed += OnGameResumed;
            _playerReader = new PlayerReader(_playerInput);
            _characterNormalHeight = _controller.height;
            _characterCenter = _controller.center;
            CurrentSpeed = speed;
        }

        private void OnDestroy()
        {
            GameSettings.onGamePaused -= OnGamePause;
            GameSettings.onGameResumed -= OnGameResumed;
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
            UpdateMoveState();
            HandleFootSound();
            
            var moveDirection = GetMovementDirection();
            _velocity = AdjustVelocity(moveDirection.magnitude * CurrentSpeed * moveDirection.normalized);
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
        

        private void HandleFootSound()
        {
            if (!_controller.isGrounded)
            {
                return;
            }

            _footStepTimer -= Time.deltaTime;

            if (_footStepTimer > 0 || _velocity.x == 0 || _velocity.z == 0)
            {
                return;
            }

            RaycastHit hit;
            if (!Physics.Raycast(transform.position, Vector3.down, out hit, _controller.height))
            {
                return;
            }

            FootSoundEffect sound = hit.collider.gameObject.GetComponent<FootSoundEffect>();
            if (sound is not null)
            {
                var clip = sound.GetSound();
                _audioSource.PlayOneShot(clip);
                _footStepTimer = GetFootStepOffset();
            }
        }
        /**
         * Возвращает время ожидания до следующего шага
         */
        private float GetFootStepOffset()
        {
            var stepOffset = _footStepOffset;
            switch (_moveState)
            {
                case MoveState.Base:
                    stepOffset = _footStepOffset;
                    break;
                case MoveState.Crouch:
                    stepOffset = _footStepCrouchOffset;
                    break;
                case MoveState.Run:
                    stepOffset = _footStepRunOffset;
                    break;
            }
            return stepOffset;
        }

        private void UpdateMoveState()
        {
            if (!_controller.isGrounded)
            {
                return;
            }
            if (_playerReader.CrouchPressed)
            {
                _moveState = MoveState.Crouch;
                CurrentSpeed = crouchSpeed;
            }
            else if (_playerReader.RunPressed)
            {
                _moveState = MoveState.Run;
                CurrentSpeed = runSpeed;
            }
            else
            {
                _moveState = MoveState.Base;
                CurrentSpeed = speed;   
            }
        }

        private Vector3 AdjustVelocity(Vector3 velocity)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _controller.height))
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