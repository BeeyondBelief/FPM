using Player;
using UnityEngine;

namespace Guard
{
    public class OnlyRangeTactic : ITactic
    {
        private float _range;
        public OnlyRangeTactic(float range)
        {
            _range = range;
        }
        public bool IsFound(GuardMovement guard, PlayerObject player)
        {
            var vectorToPlayer = player.transform.position - guard.transform.position;
            return vectorToPlayer.magnitude < _range;
        }

    }
    public class OnlyRangeSearch : GuardSearch
    {
        [SerializeField] public float searchRange = 15f;

        private ITactic _rangeTactic;

        private void Awake()
        {
            _rangeTactic = new OnlyRangeTactic(searchRange);
        }

        public override bool Search(GuardMovement guard, PlayerObject player)
        {
            return _rangeTactic.IsFound(guard, player);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, searchRange);
        }
    }
}