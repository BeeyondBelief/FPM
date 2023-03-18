using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LevelSelector : MonoBehaviour
    {
        public SceneAsset scene;
        public TMP_Text textMesh;
        void Awake()
        {
            textMesh.text = scene.name;
        }
        public void Select()
        {
            SceneManager.LoadScene(scene.name);
        }
    }
}
