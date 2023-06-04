using System;
using UnityEngine;

namespace Behaviours
{
    public sealed class RadiusActivator : BoonActivator
    {
        public float radius = 3f;
        [SerializeField] private SphereCollider _collider;
        
        public override event Activate ShouldActivate;

        private void Awake()
        {
            _collider.radius = radius;
        }

        private void OnValidate()
        {

            _collider.radius = radius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (RadiusFunctions.HasPlayer(other, out var player))
            {
                Debug.Log('1');
                ShouldActivate?.Invoke(player);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}