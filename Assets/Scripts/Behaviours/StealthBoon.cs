using UnityEngine;
using Player;

namespace Behaviours
{
    public class StealthBoon: Boon
    {
        private Vector3 _initialPosition; 
        private float _distance;
        private float _stealthPower;
        public float CurrentStealthPower { get; private set; }
        
        public StealthBoon(Vector3 initialPosition, float edgeDistance, float stealthPower)
        {
            _initialPosition = initialPosition;
            _distance = edgeDistance;
            _stealthPower = stealthPower;
        }
        
        public override bool Tick(PlayerObject player)
        {
            var awayDistance = (player.transform.position - _initialPosition).magnitude;
            var dev = awayDistance == 0 ? 1 : awayDistance;
            CurrentStealthPower = _stealthPower / dev;
            return awayDistance > _distance;
        }
    }
}