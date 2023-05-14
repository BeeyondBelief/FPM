using System;
using Player;
using UnityEngine;

namespace Guard
{
    public class Raycast360Tactic : ITactic
    {
        private float _maxDistance;

        public Raycast360Tactic(float maxDistance)
        {
            _maxDistance = maxDistance;
        }

        public bool IsFound(GuardMovement guard, PlayerMovement player)
        {
            var vectorToPlayer = player.transform.position - guard.transform.position;

            if (Physics.Raycast(guard.transform.position, vectorToPlayer.normalized, out var hit, _maxDistance))
            {
                return hit.transform.GetComponent<PlayerMovement>() is not null;
            }

            return false;
        }
    }

    public class Raycast360Search : GuardSearch
    {
        public float maxVisibleDistance = 15f;

        private ITactic _raycastTactic;

        private void Awake()
        {
            _raycastTactic = new Raycast360Tactic(maxVisibleDistance);
        }

        public override bool Search(GuardMovement guard, PlayerMovement player)
        {
            return _raycastTactic.IsFound(guard, player);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, maxVisibleDistance);
        }
    }
}