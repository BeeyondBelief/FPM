using System;
using Player;
using UnityEngine;

namespace Behaviours
{
    public class Smoke: MonoBehaviour
    {
        public float stealthPower = 10f;
        [SerializeField] private BoonActivator _activator;

        private SmokeBoon _boon;

        private void Awake()
        {
            _activator.ShouldActivate += Activate;
            _activator.ShouldDeactivate += Deactivate;
        }

        private void OnDestroy()
        {
            _activator.ShouldActivate -= Activate;
            _activator.ShouldDeactivate -= Deactivate;
        }

        public void Activate(PlayerObject player)
        {
            _boon = new SmokeBoon(stealthPower);
            player.boons.Add(_boon);
        }

        public void Deactivate(PlayerObject player)
        {
            player.boons.Remove(_boon);
        }
    }
}