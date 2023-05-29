using System;
using Player;
using UnityEngine;

namespace Behaviours
{
    public interface IBoon
    {
        public void Apply(PlayerObject player);
        public bool Tick(PlayerObject player);
        public void Destroy(PlayerObject player);
    }

    public abstract class Boon : IBoon
    {
        
        public abstract bool Tick(PlayerObject player);
        
        public void Apply(PlayerObject player)
        {
            
        }
        public void Destroy(PlayerObject player)
        {

        }
    }
}