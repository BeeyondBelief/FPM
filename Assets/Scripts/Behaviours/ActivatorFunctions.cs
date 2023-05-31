using UnityEngine;
using Player;

namespace Behaviours
{
    public static class RadiusFunctions
    {
        public static void AddSphereTrigger(GameObject gameObject, float radius)
        {
            var sc = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
            sc.radius = radius;
            sc.isTrigger = true;
        }
        public static bool HasPlayer(Component other, out PlayerObject player)
        {
            player = other.gameObject.GetComponent<PlayerObject>();
            return player is not null;
        }
        
    }
}