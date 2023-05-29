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

        private void OnTriggerStay(Collider other)
        {
            var player = other.gameObject.GetComponent<PlayerObject>();
            if (player is null)
            {
                return;
            }
            player.boons.Add(new StealthBoon(transform.position, radius, stealthPower));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}