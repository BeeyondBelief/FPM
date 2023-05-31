using System;
using Player;
using UnityEngine;

namespace Behaviours
{
    public interface IBoon
    {
        public string CharImage { get; }
        public void Apply(PlayerObject player);
        public void Tick(PlayerObject player);
        public void Destroy(PlayerObject player);
    }
}