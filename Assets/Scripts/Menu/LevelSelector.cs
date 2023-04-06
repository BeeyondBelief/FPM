using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class LevelSelector : MonoBehaviour
    {
        public SceneAsset scene;
        public TMP_Text textMesh;
        private void Awake()
        {
            if (textMesh == null) {return;} 
            textMesh.text = scene.name;
        }
        public void Select()
        {
            SceneManager.LoadScene(scene.name);
        }
    }
}
