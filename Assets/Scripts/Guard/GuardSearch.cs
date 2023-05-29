using UnityEngine;

namespace Guard
{
    public interface ITactic
    {
        public bool IsFound(GuardMovement guard, Player.Player player);
    }
    
    public abstract class GuardSearch : MonoBehaviour
    {
        public virtual bool Search(GuardMovement guard, Player.Player player)
        {
            return false;
        }

        public virtual bool IsCaught(GuardMovement guard, Player.Player player)
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