using Player;
using UnityEngine;

namespace Guard
{
    public class Raycast360Tactic : SearchTactic
    {
        public float maxVisibleDistance = 15f;

        public override bool Work(GuardMovement guard, PlayerMovement player)
        {
            var vectorToPlayer = player.transform.position - guard.transform.position;

            if (Physics.Raycast(transform.position, vectorToPlayer.normalized, out var hit, maxVisibleDistance))
            {
                return hit.transform.GetComponent<PlayerMovement>() is not null;
            }

            return false;
        }
    }
}