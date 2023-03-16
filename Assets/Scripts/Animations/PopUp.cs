
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Animations
{
    public class PopUp: MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private TMP_Text _mesh;

        private Queue<string> _queue = new();
        private bool _animating;
        private AnimatorStateInfo _info;

        private void Awake()
        {
            _info = _animator.GetCurrentAnimatorStateInfo(0);
        }

        public void Show(string popUpText)
        {
            _queue.Enqueue(popUpText);
        }

        public void FixedUpdate()
        {
            if (!_info.IsName("Closed")) return;
            
            while (_queue.Count != 0)
            {
                _mesh.text = _queue.Dequeue();
                _animator.SetTrigger("Trigger");
            }
        }
    }
}