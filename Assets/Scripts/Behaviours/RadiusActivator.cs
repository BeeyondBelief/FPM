using UnityEngine;

namespace Behaviours
{
    public sealed class RadiusActivator : BoonActivator
    {
        public float radius = 3f;
        public override event Activate ShouldActivate;

        private void Awake()
        {
            RadiusFunctions.AddSphereTrigger(gameObject, radius);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (RadiusFunctions.HasPlayer(other, out var player))
            {
                ShouldActivate?.Invoke(player);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}