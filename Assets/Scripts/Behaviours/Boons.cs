using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Player;
using UnityEngine;

namespace Behaviours
{
    public class Boons: MonoBehaviour
    {
        [SerializeField] private PlayerObject _player;
        private List<IBoon> _boons = new();
        private List<IBoon> _toDestroy = new();

        public void Add(IBoon boon)
        {
            boon.Apply(_player);
            RemoveSameBoon(boon);
            _boons.Add(boon);
        }

        private void RemoveSameBoon(IBoon boon)
        {
            var t = boon.GetType();
            for (var i = 0; i < _boons.Count; i++)
            {
                if (_boons[i].GetType() == t)
                {
                    _boons.RemoveAt(i);
                    return;
                }
            }
        }

        #nullable enable
        public T? GetBoon<T>() where T: Boon
        {
            var t = typeof(T);
            foreach (var boon in _boons)
            {
                if (boon.GetType() == t)
                {
                    return boon as T;
                }
            }
            return null;
        }
        #nullable disable

        public IBoon[] GetBoons()
        {
            return _boons.ToArray();
        }

        private void FixedUpdate()
        {
            foreach (var boon in _boons)
            {
                if (boon.Tick(_player))
                {
                    _toDestroy.Add(boon);
                }
            }
            _toDestroy.ForEach(boon =>
            {
                boon.Destroy(_player);
                _boons.Remove(boon);
            });
            _toDestroy.Clear();
        }
    }
}