using Interactions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class StartEndTrigger : MonoBehaviour
    {
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
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
