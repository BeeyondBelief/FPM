
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Animations
{
    public class PopUp: MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private TextMeshProUGUI _mesh;
        [SerializeField] private float _showTime = 1f;

        private Queue<string> _queue = new();
        private bool _animating;

        public void Show(string popUpText)
        {
            _queue.Enqueue(popUpText);
        }

        public void FixedUpdate()
        {
            if (_animating) return;
            
            while (_queue.Count != 0)
            {
                _animating = true;
                _mesh.text = _queue.Dequeue();
                _animator.SetTrigger("Open");
                Invoke(nameof(Hide), _showTime);
            }
        }

        private void Hide()
        {
            _animator.SetTrigger("Close");
            _animating = false;
        }
    }
}