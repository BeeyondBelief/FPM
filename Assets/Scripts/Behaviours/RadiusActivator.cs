using System;
using Player;
using UnityEngine;

namespace Behaviours
{
    public sealed class RadiusActivator : BoonActivator
    {
        public float radius = 3f;

        public override event Deactivate ShouldDeactivate;
        public override event Activate ShouldActivate;

        private void Awake()
        {
            var sc = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
            sc.radius = radius;
            sc.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (HasPlayer(other, out var player))
            {
                ShouldActivate?.Invoke(player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (HasPlayer(other, out var player))
            {
                ShouldDeactivate?.Invoke(player);
            }
        }

        private static bool HasPlayer(Component other, out PlayerObject player)
        {
            player = other.gameObject.GetComponent<PlayerObject>();
            return player is not null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}