using Behaviours;
using Cinemachine;
using Game;
using Interactions;
using UnityEngine;


namespace Player
{
    public class PlayerObject: MonoBehaviour
    {
        [Header("Camera")]
        public CinemachineBrain view;
        
        [Header("Movement")]
        public float crouchSpeed = 3f;
        public float walkSpeed = 7f;
        public float runSpeed = 15f;

        [Header("Mover")]
        [SerializeField] private PlayerMovement _mover;

        [Header("Jumping")]
        public float jumpForce = 7f;

        [Header("Physics")] 
        public float gravityForce = -9.81f;
        public float mass = 50f;

        public float CurrentSpeed => _mover.CurrentSpeed;

        [Header("Inventory")] public Inventory inventory;
        [Header("Boons")] public Boons boons;

        private void Awake()
        {
            GameSettings.onGamePaused += OnGamePause;
            GameSettings.onGameResumed += OnGameResumed;
        }

        private void Update()
        {
            ApplyCameraAngles();
        }

        private void OnDestroy()
        {
            GameSettings.onGamePaused -= OnGamePause;
            GameSettings.onGameResumed -= OnGameResumed;
        }
        
        private void OnGamePause()
        {
            gameObject.SetActive(false);
            view.enabled = false;
        }

        private void OnGameResumed()
        {
            gameObject.SetActive(true);
            view.enabled = true;
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
            body.AddForce(_mover.Velocity * mass * Time.deltaTime, ForceMode.Impulse);
        }
        
        /// <summary>
        /// Обновляет направление движения в зависимости от направления камеры
        /// </summary>
        private void ApplyCameraAngles()
        {
            var trans = _mover.transform;
            var eulerAngles = trans.eulerAngles;
            trans.eulerAngles = new Vector3(eulerAngles.x, view.transform.eulerAngles.y, eulerAngles.z);
        }
    }
}