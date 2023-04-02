using Interactions;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.SceneManagement;

namespace Level
{
    public class StartEndTrigger : MonoBehaviour
    {
        public GameObject WinScreen;
        [SerializeField] private Collider _collider;

        #nullable enable
        private void OnTriggerEnter(Collider other)
        {
            var inv = other.gameObject.GetComponent<Inventory>();
            if (inv is null)
            {
                return;
            }

            foreach (var item in inv.GetItems())
            {
                if (item.type == ItemType.KeyItem)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    
                    WinScreen.SetActive(true);
                }
            }
        }
        #nullable disable


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, _collider.bounds.size);
        }
    }
}
