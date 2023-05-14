using Player;
using UnityEngine;

namespace Guard
{
    public abstract class SearchTactic : MonoBehaviour
    {
        public virtual bool Work(GuardMovement guard, PlayerMovement player)
        {
            return false;
        }

        public virtual bool IsCaught(GuardMovement guard, PlayerMovement player)
        {
            return false;
        }
    }
}