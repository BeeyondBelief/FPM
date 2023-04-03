using UnityEngine;

namespace Game
{
    public class GameSettings : MonoBehaviour
    {
        public delegate void OnGamePaused();
        public delegate void OnGameResumed();

        public static event OnGamePaused onGamePaused;
        public static event OnGameResumed onGameResumed;


        public static void PauseGame()
        {
            Time.timeScale = 0f;
            onGamePaused?.Invoke();
        }

        public static void ResumeGame()
        {
            if (Time.timeScale != 0f)
            {
                return;
            }
            Time.timeScale = 1f;
            onGameResumed?.Invoke();
        }
    }
}
