using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactions
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private Camera _camera;
        [SerializeField] private float pickDistance = 10f;

        [SerializeField] private List<Item> _inventory = new();

        private PlayerReader _reader;

        private void Awake()
        {
            _reader = new PlayerReader(input);
        }

        private void Update()
        {
            if (!_reader.MouseClicked)
            {
                return;
            }
            var pos = _reader.MousePos;
            var ray = _camera.ScreenPointToRay(pos);
            if (Physics.Raycast(ray, out var hit, pickDistance))
            {
                var collectable = hit.collider.gameObject.GetComponent<Collectable>();
                if (collectable is null)
                {
                    return;
                }
                AddItem(collectable.Collect());
            }
            
        }

        public void AddItem(Item item)
        {
            _inventory.Add(item);
        }
    }
}