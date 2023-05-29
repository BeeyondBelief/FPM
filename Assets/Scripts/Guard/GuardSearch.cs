using Player;
using UnityEngine;

namespace Guard
{
    public interface ITactic
    {
        public bool IsFound(GuardMovement guard, PlayerObject player);
    }
    
    public abstract class GuardSearch : MonoBehaviour
    {
        public virtual bool Search(GuardMovement guard, PlayerObject player)
        {
            return false;
        }

        public virtual bool IsCaught(GuardMovement guard, PlayerObject player)
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