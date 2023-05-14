using Player;
using UnityEngine;

namespace Guard
{
    public class OnlyRangeTactic : SearchTactic
    {
        [SerializeField] public float searchRange = 15f;

        public override bool Work(GuardMovement guard, PlayerMovement player)
        {
            var vectorToPlayer = player.transform.position - guard.transform.position;
            return vectorToPlayer.magnitude < searchRange;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, searchRange);
        }
    }
}