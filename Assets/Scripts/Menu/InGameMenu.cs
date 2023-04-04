using UnityEngine;
using UnityEngine.InputSystem;
using Game;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

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

        private void OnValidate()
        {
            if (FindObjectOfType<EventSystem>() is null)
            {
                EditorApplication.delayCall += () =>
                {
                    var eventSystemObject = new GameObject("EventSystem");
                    eventSystemObject.AddComponent<EventSystem>();
                    eventSystemObject.AddComponent<InputSystemUIInputModule>();
                };
            }
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
