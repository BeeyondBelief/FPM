using System;
using Player;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviours
{
    public class BoonsUI: MonoBehaviour
    {
        [SerializeField] private PlayerObject _player;
        [SerializeField] private Transform grid;
        [SerializeField] private TMP_Text effectObject;

        private void FixedUpdate()
        {
            DestroyBoons();
            ShowAllBoons();
        }

        private void ShowAllBoons()
        {
            foreach (var boon in _player.boons.GetBoons())
            {
                var effect = Instantiate(effectObject, grid.transform, true);
                effect.text = boon.CharImage;
                effect.enabled = true;
            }
        }

        private void DestroyBoons()
        {
            foreach (Transform child in grid.transform) {
                Destroy(child.gameObject);
            }
        }
    }
}