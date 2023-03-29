using Game;
using UnityEngine;

namespace Menu
{
    
    public class Joysticks : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
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

        private void Pause()
        {
            
            _canvas.enabled = false;
        }

        private void Resume()
        {
            _canvas.enabled = true;
        }
    }

}