using System.Collections.Generic;
using Animations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactions
{
    public class Inventory : MonoBehaviour
    {
        public float pickDistance = 10f;
        [SerializeField] private List<Item> _inventory = new();

        private PlayerReader _reader;
        private PopUp _popUp;
        private Camera _camera;

        private void Awake()
        {
            var input = FindObjectOfType<PlayerInput>();
            if (input is not null)
            {
                _reader = new PlayerReader(input);
            }
            _popUp = FindObjectOfType<PopUp>();
            _camera = FindObjectOfType<Camera>();
        }

        private void Update()
        {
            if (_reader is null || !_reader.MouseClicked || _camera is null) return;
            
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
            ShowPopUp(item);
        }

        private void ShowPopUp(Item item)
        {
            if (_popUp is not null && item.type == ItemType.KeyItem)
            {
                _popUp.Show(item.whenFound);
            }
        }
    }
}