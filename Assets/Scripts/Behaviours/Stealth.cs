using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Behaviours
{
    public class Stealth: MonoBehaviour
    {
        public float stealthPower = 10f;
        public float radius = 3f;

        private void Awake()
        {
            var sc = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
            sc.radius = radius;
            sc.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponent<PlayerObject>();
            if (player is null)
            {
                return;
            }
            var contrl = player.GetComponent<CharacterController>();
            var customRadius = radius;
            if (contrl is not null)
            {
                customRadius += contrl.radius;
            }
            player.boons.Add(new StealthBoon(transform.position, customRadius, stealthPower));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}