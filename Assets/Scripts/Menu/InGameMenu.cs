using UnityEngine;
using UnityEngine.InputSystem;
using Game;

namespace Menu
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private Canvas menuCanvas;

        private bool _paused;

        private void Awake()
        {
            GameSettings.onGamePaused += Pause;
            GameSettings.onGameResumed += Resume;
        }

        private void OnDestroy()
        {
            GameSettings.onGamePaused -= Pause;
            GameSettings.onGameResumed -= Resume;
        }

        public void OnPausePressed(InputAction.CallbackContext context)
        {
            if (!context.started)
            {
                return;
            }
            if (_paused)
            {
                GameSettings.ResumeGame();
            }
            else
            {
                GameSettings.PauseGame();
            }
        }
        
        private void Pause()
        {
            _paused = true;
            menuCanvas.gameObject.SetActive(true);
        }

        private void Resume()
        {
            menuCanvas.gameObject.SetActive(false);
            _paused = false;
        }
    }
}
