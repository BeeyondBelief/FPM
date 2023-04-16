using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class LevelRestart : MonoBehaviour
    {
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
