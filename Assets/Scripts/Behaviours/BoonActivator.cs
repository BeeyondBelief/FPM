
using Player;
using UnityEngine;

namespace Behaviours
{
    public abstract class BoonActivator: MonoBehaviour
    {
        public delegate void Activate(PlayerObject player);
        public abstract event Activate ShouldActivate;
    }
    
    public abstract class BoonDeactivator: MonoBehaviour
    {
        public delegate void Deactivate(PlayerObject player);
        public abstract event Deactivate ShouldDeactivate;
    }
}