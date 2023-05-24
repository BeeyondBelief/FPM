using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class LevelRestart : MonoBehaviour
    {
        public void RestartLevel()
        {
            GameSettings.ResumeGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
