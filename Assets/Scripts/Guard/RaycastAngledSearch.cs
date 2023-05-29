using UnityEngine;
using Player;

namespace Guard
{
    
    public class RaycastAngledTactic : ITactic
    {
        private float _maxDistance;
        private float _angle;

        public RaycastAngledTactic(float maxDistance, float angle)
        {
            _maxDistance = maxDistance;
            _angle = angle;
        }

        public bool IsFound(GuardMovement guard, PlayerObject player)
        {
            var vectorToPlayer = player.transform.position - guard.transform.position;

            if (!CanStraitLook(guard, vectorToPlayer))
            {
                return false;
            }

            if (Physics.Raycast(guard.transform.position, vectorToPlayer, out var hit, _maxDistance))
            {
                return hit.transform.GetComponent<PlayerObject>() is not null;
            }

            return false;
        }
        private bool CanStraitLook(Component from, Vector3 toPlayer)
        {
            var dot = Vector3.Dot(from.transform.forward.normalized, toPlayer.normalized);
            return Mathf.Acos(dot) * Mathf.Rad2Deg <= _angle;
        }
    }
    
    public class RaycastAngledSearch : GuardSearch
    {
        public float alwaysVisibleDistance = 3f;
        public float maxVisibleDistance = 15f;
        public float angle = 30;

        private ITactic _angledTactic;
        private ITactic _raycastTactic;

        private void Awake()
        {
            _angledTactic = new RaycastAngledTactic(maxVisibleDistance, angle);
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