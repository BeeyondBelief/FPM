using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class LevelSelector : MonoBehaviour
    {
        public SceneAsset scene;
        public TMP_Text textMesh;
        private void Awake()
        {
            textMesh.text = scene.name;
        }
        public void Select()
        {
            SceneManager.LoadScene(scene.name);
        }
    }
}
