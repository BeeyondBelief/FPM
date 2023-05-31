using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;

namespace Behaviours
{
    public class Boons: MonoBehaviour
    {
        [SerializeField] private PlayerObject _player;
        private List<IBoon> _boons = new();

        public void Add(IBoon boon)
        {
            _boons.Add(boon);
            boon.Apply(_player);
        }

#nullable enable
        public T? GetBoon<T>() where T: IBoon
        {
            var t = typeof(T);
            foreach (var boon in _boons.Where(boon => boon.GetType() == t))
            {
                return (T)boon;
            }
            return default;
        }
#nullable disable

        public void Remove(IBoon boon)
        {
            boon.Destroy(_player);
            _boons.Remove(boon);
        }

        public IBoon[] GetBoons()
        {
            return _boons.ToArray();
        }

        private void FixedUpdate()
        {
            foreach (var boon in _boons)
            {
                boon.Tick(_player);
            }
        }
    }
}