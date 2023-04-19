using Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class LevelSelector : MonoBehaviour
    {
        public string sceneName;
        #nullable enable
        [SerializeField] private string? title;
        public TMP_Text? textMesh;
        #nullable disable
        private void OnValidate()
        {
            if (textMesh is null)
            {
                return;
            }

            if (title is not null)
            {
                textMesh.text = title; 
            }
        }
        public void Select()
        {
            SceneManager.LoadScene(sceneName);
            GameSettings.ResumeGame();
        }
    }
}
