using UnityEngine;

namespace Behaviours
{
    public sealed class RadiusDeactivator : BoonDeactivator
    {
        public float radius = 3f;
        public override event Deactivate ShouldDeactivate;
        
        private void Awake()
        {
            RadiusFunctions.AddSphereTrigger(gameObject, radius);
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