using UnityEngine;

namespace Behaviours
{
    public sealed class RadiusActivator : BoonActivator
    {
        public float radius = 3f;
        [SerializeField] private ParticleSystem _particles; 
        public override event Activate ShouldActivate;

        private void Awake()
        {
            RadiusFunctions.AddSphereTrigger(gameObject, radius);
            Debug.Log("radius is " + radius);
            var shape = _particles.shape;
            shape.radius = radius;
            var emission = _particles.emission;
            float base_rate = emission.rateOverTimeMultiplier;
            Debug.Log("mult is " + emission.rateOverTimeMultiplier);
            emission.rateOverTimeMultiplier = radius*base_rate;
            Debug.Log("new mult is " + emission.rateOverTimeMultiplier);
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