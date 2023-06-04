using UnityEngine;

namespace Behaviours
{
    public sealed class RadiusDeactivator : BoonDeactivator
    {
        public float radius = 3f;
        [SerializeField] private SphereCollider _collider;

        public override event Deactivate ShouldDeactivate;
        
        private void Awake()
        {
            _collider.radius = radius;
        }

        private void OnValidate()
        {

            _collider.radius = radius;
        }

        private void OnTriggerExit(Collider other)
        {
            if (RadiusFunctions.HasPlayer(other, out var player))
            {
                ShouldDeactivate?.Invoke(player);
            }
        }
    }
}