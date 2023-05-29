using UnityEngine;
using UnityEngine.InputSystem;
using Sound;

namespace Player
{
    public enum MoveState
    {
        Walk, Run, Crouch
    }
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private Player _player;

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
        public MoveState MoveState { get; private set; }
        public Vector3 Velocity { get; private set; }

        private PlayerReader _playerReader;
        private float _ySpeed;
        private float _characterNormalHeight;
        private Vector3 _characterCenter;

        private void Awake()
        {
            _playerReader = new PlayerReader(_playerInput);
            _characterNormalHeight = _controller.height;
            _characterCenter = _controller.center;
            CurrentSpeed = _player.walkSpeed;
            MoveState = MoveState.Walk;
        }

        private void Update()
        {
            _playerReader.ReadInputs();
            ApplyCameraAngles();
            ApplyGravityAndJump();
            HandleCrouching();
            UpdateMoveState();
            HandleFootSound();
        }

        private void FixedUpdate()
        {
            var moveDirection = GetMovementDirection();
            var velocity = AdjustVelocity(moveDirection.magnitude * CurrentSpeed * moveDirection.normalized);
            velocity.y += _ySpeed;
            Velocity = velocity;
            _controller.Move(Velocity * Time.deltaTime);
            _player.transform.position = _controller.transform.position;
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

            if (_footStepTimer > 0 || Velocity.x == 0 || Velocity.z == 0)
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
            switch (MoveState)
            {
                case MoveState.Walk:
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
                MoveState = MoveState.Crouch;
                CurrentSpeed = _player.crouchSpeed;
            }
            else if (_playerReader.RunPressed)
            {
                MoveState = MoveState.Run;
                CurrentSpeed = _player.runSpeed;
            }
            else
            {
                MoveState = MoveState.Walk;
                CurrentSpeed = _player.walkSpeed;   
            }
        }

        private Vector3 AdjustVelocity(Vector3 velocity)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _controller.height))
            {
                var slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                var adjusted = slopeRotation * velocity;
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
                _ySpeed += _player.gravityForce * Time.deltaTime;
                return;
            }
            if (_playerReader.JumpPressed)
            {
                _ySpeed = _player.jumpForce;
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
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, _player.view.transform.eulerAngles.y,
                                                transform.eulerAngles.z);
        }
    }
}