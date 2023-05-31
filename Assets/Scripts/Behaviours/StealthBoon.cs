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
        public override string CharImage => "S";
        
        public StealthBoon(Vector3 initialPosition, float edgeDistance, float stealthPower)
        {
            _initialPosition = initialPosition;
            _distance = edgeDistance;
            _stealthPower = stealthPower;
        }
        
        public override bool Tick(PlayerObject player)
        {
            var awayDistance = Vector3.Distance(player.transform.position, _initialPosition);
            CurrentStealthPower = Mathf.Lerp(_stealthPower, 0f, awayDistance / _distance);
            return awayDistance > _distance;
        }
    }
}