using System.Collections.Generic;
using Animations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactions
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Camera _camera;
        [SerializeField] private PopUp _popUp = null;
        [SerializeField] private float pickDistance = 10f;

        [SerializeField] private List<Item> _inventory = new();

        private PlayerReader _reader;

        private void Awake()
        {
            _reader = new PlayerReader(_input);
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
            if (_popUp is not null && item.type == ItemType.KeyItem)
            {
                _popUp.Show(item.whenFound);
            }
        }
    }
}