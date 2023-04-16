using System;
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
        [SerializeField] private InputActionReference pauseAction;

        private bool _paused;

        private void Awake()
        {
            GameSettings.onGamePaused += Pause;
            GameSettings.onGameResumed += Resume;
            pauseAction.action.started += OnPausePressed;
            pauseAction.action.Enable();
        }

        private void OnDestroy()
        {
            GameSettings.onGamePaused -= Pause;
            GameSettings.onGameResumed -= Resume;
        }

        private void OnPausePressed(InputAction.CallbackContext context)
        {
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
