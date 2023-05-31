using System;
using Player;
using UnityEngine;

namespace Behaviours
{
    public interface IBoon
    {
        public string CharImage { get; }
        public void Apply(PlayerObject player);
        public bool Tick(PlayerObject player);
        public void Destroy(PlayerObject player);
    }

    public abstract class Boon : IBoon
    {
        
        public abstract bool Tick(PlayerObject player);
        public abstract string CharImage { get; }

        
        public void Apply(PlayerObject player)
        {
        }
        public void Destroy(PlayerObject player)
        {
        }
    }
}