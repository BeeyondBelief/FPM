using System.Collections.Generic;
using Animations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactions
{
    public class Inventory : MonoBehaviour
    {
        #nullable enable
        [SerializeField] private PlayerInput? _input;
        [SerializeField] private PopUp? _popUp;
        [SerializeField] private Camera? _camera;    
        
        public float pickDistance = 10f;
        [SerializeField] private List<Item> _inventory = new();
        
        private PlayerReader? _reader;
        
        private void Awake()
        {
            // Если так не сделать, то камера при проверках _camera is null, будет давать
            // False, если камера не установлена.
            _camera = _camera ? _camera : null;
            if (_input is not null)
            {
                _reader = new PlayerReader(_input);
            }
        }

        private void Update()
        {
            if (_reader is null || _camera is null || !_reader.MouseClicked) return;

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