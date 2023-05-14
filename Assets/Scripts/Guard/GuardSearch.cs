using Player;
using UnityEngine;

namespace Guard
{
    public interface ITactic
    {
        public bool IsFound(GuardMovement guard, PlayerMovement player);
    }
    
    public abstract class GuardSearch : MonoBehaviour
    {
        public virtual bool Search(GuardMovement guard, PlayerMovement player)
        {
            return false;
        }

        public virtual bool IsCaught(GuardMovement guard, PlayerMovement player)
        {
            if (!guard.Chasing)
            {
                return false;
            }
            var vectorToPlayer = player.transform.position - transform.position;

            return vectorToPlayer.magnitude < guard.catchDistance;
        }
    }
}