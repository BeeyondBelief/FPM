
using Player;
using UnityEngine;

namespace Behaviours
{
    public delegate void Activate(PlayerObject player);
    public delegate void Deactivate(PlayerObject player);
    
    public abstract class BoonActivator: MonoBehaviour
    {
        public abstract event Activate ShouldActivate;
        public abstract event Deactivate ShouldDeactivate;
    }
}