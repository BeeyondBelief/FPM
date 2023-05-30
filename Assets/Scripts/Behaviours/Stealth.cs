using Player;
using UnityEngine;

namespace Behaviours
{
    public class Stealth: RadiusActivator
    {
        public float stealthPower = 10f;

        public override void Activate(PlayerObject player)
        {
            player.boons.Add(new StealthBoon(transform.position, Radius, stealthPower));
        }
    }
}