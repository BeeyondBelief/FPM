using UnityEngine;

namespace Interactions
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Collectables/Item")]
    public class Item : ScriptableObject
    {
        public string description = "Sorry, the developer thinks it's good to leave the description empty :c";
        public ItemType type = ItemType.RegularItem;
    }
    
    public enum ItemType
    {
        RegularItem,
        KeyItem
    }
}