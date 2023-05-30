using Player;
using UnityEngine;

namespace Behaviours
{
    public abstract class RadiusActivator : MonoBehaviour, IBoonActivator
    {
        [SerializeField] private float _radius = 3f;
        public float Radius { get; private set; }
        
        private void Awake()
        {
            var sc = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
            sc.radius = _radius;
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
            if (contrl is not null)
            {
                Radius = contrl.radius + _radius;
            }
            else
            {
                Radius = _radius;
            }
            Activate(player);
        }

        public abstract void Activate(PlayerObject player);
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}