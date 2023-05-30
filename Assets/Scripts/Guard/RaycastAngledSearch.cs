using Behaviours;
using UnityEngine;
using Player;

namespace Guard
{
    
    public class RaycastAngledTactic : ITactic
    {
        private float _maxDistance;
        private float _angle;
        private float _stealthThreshold;
        private Vector3 _vectorToPlayer;

        public RaycastAngledTactic(float maxDistance, float angle, float stealthThreshold)
        {
            _maxDistance = maxDistance;
            _angle = angle;
            _stealthThreshold = stealthThreshold;
        }

        public bool IsFound(GuardMovement guard, PlayerObject player)
        {
            if (!CanStraitLook(guard, player, out _vectorToPlayer))
            {
                return false;
            }

            if (Physics.Raycast(guard.transform.position, _vectorToPlayer, out var hit, _maxDistance))
            {
                return hit.transform.GetComponent<PlayerObject>() is not null;
            }

            return false;
        }
        private bool CanStraitLook(GuardMovement guard, PlayerObject player, out Vector3 toPlayer)
        {
            toPlayer = player.transform.position - guard.transform.position;

            if (toPlayer.magnitude > _maxDistance)
            {
                return false;
            }

            var dot = Vector3.Dot(guard.transform.forward.normalized, toPlayer.normalized);
            if (Mathf.Acos(dot) * Mathf.Rad2Deg > _angle)
            {
                return false;
            }
            
            var stealthBoon = player.boons.GetBoon<StealthBoon>();
            if (stealthBoon is null)
            {
                return true;
            }
            return stealthBoon.CurrentStealthPower * toPlayer.magnitude < _stealthThreshold;
        }
    }
    
    public class RaycastAngledSearch : GuardSearch
    {
        public float alwaysVisibleDistance = 3f;
        public float maxVisibleDistance = 15f;
        public float stealthThreshold = 10f;
        public float angle = 30;

        private ITactic _angledTactic;
        private ITactic _raycastTactic;

        private void Awake()
        {
            _angledTactic = new RaycastAngledTactic(maxVisibleDistance, angle, stealthThreshold);
            _raycastTactic = new Raycast360Tactic(alwaysVisibleDistance);
        }

        public override bool Search(GuardMovement guard, PlayerObject player)
        {
            return _angledTactic.IsFound(guard, player) || _raycastTactic.IsFound(guard, player);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            var left = CalculateAngledVectorFromPoint(-angle);
            var right = CalculateAngledVectorFromPoint(angle);
            
            Gizmos.DrawLine(transform.position, left);
            Gizmos.DrawLine(transform.position, right);
            
            Gizmos.DrawWireSphere(transform.position, alwaysVisibleDistance);
        }
        
        public Vector3 CalculateAngledVectorFromPoint(float degAngle)
        {
            var dir = Quaternion.Euler(0, degAngle, 0) * transform.forward;
            return transform.position + dir * maxVisibleDistance;
        }
    }
}