using UnityEngine;

namespace Interactions
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private Item item;
        
        public Item Collect()
        {
            gameObject.SetActive(false);
            return item;
        }
    }
}