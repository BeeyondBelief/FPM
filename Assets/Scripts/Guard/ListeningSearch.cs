using UnityEngine;
using Player;


namespace Guard
{
    
    public class ListeningTactic : ITactic
    {
        private float _hearThreshold;
        private float _maxDistance;
        private float _dampingPercent;

        public ListeningTactic(float maxDistance, float dampingPercent, float hearThreshold)
        {
            _maxDistance = maxDistance;
            _dampingPercent = dampingPercent;
            _hearThreshold = hearThreshold;
        }

        public bool IsFound(GuardMovement guard, PlayerObject player)
        {
            var vectorToPlayer = player.transform.position - guard.transform.position;

            var hits = Physics.RaycastAll(guard.transform.position, vectorToPlayer, _maxDistance);
            var soundRatio = 1f;
            foreach (var hit in hits)
            {
                if (hit.transform.GetComponent<PlayerObject>() is null)
                {
                    soundRatio *= _dampingPercent;
                }
                else
                {
                    var sound = player.CurrentSpeed * soundRatio;
                    return sound > _hearThreshold;
                }
            }

            return false;
        }
    }
    
    public class ListeningSearch : GuardSearch
    {
        public float maxListeningDistance = 15f;
        public float objectsLowerSound = 0.8f;
        public float hearThreshold = 5f;

        private ITactic _listeningTactic;

        private void Awake()
        {
            _listeningTactic = new ListeningTactic(maxListeningDistance, objectsLowerSound, hearThreshold);
        }

        public override bool Search(GuardMovement guard, PlayerObject player)
        {
            return _listeningTactic.IsFound(guard, player);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;

            Gizmos.DrawWireSphere(transform.position, maxListeningDistance);
        }
    }
}