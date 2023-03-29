using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{
    public class PlayerReader
    {
        private readonly PlayerInput _inp;
        private Vector2 _directionFlat;

        public Vector3 Direction { get; private set; }
        public bool JumpPressed { get; private set; }
        public bool CrouchPressed { get; private set; }
        public bool RunPressed { get; private set; }
        public bool MouseClicked => _inp.actions["MouseClick"].IsPressed();
        public Vector2 MousePos => _inp.actions["MousePos"].ReadValue<Vector2>();


        public PlayerReader(PlayerInput inp)
        {
            _inp = inp;
            JumpPressed = false;
            CrouchPressed = false;
            RunPressed = false;
        }

        private void ReadDirection()
        {
            _directionFlat = _inp.actions["Move"].ReadValue<Vector2>();
            Direction = new Vector3(_directionFlat.x, 0f, _directionFlat.y).normalized;
        }

        private void ReadJump()
        {
            JumpPressed = _inp.actions["Jump"].IsPressed();
        }

        private void ReadCrouch()
        {
            CrouchPressed = _inp.actions["Crouch"].IsPressed();
        }

        private void ReadRun()
        {
            RunPressed = _inp.actions["Run"].IsPressed();
        }

        public void ReadInputs()
        {
            ReadDirection();
            ReadJump();
            ReadCrouch();
            ReadRun();
        }
    }
}
