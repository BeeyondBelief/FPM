using System;
using UnityEngine;

namespace Menu
{
    public class SectionTravel : MonoBehaviour
    {
        [SerializeField] private MenuSection _to;
        #nullable enable
        private MenuSection? _from;
        #nullable disable
        
        private void OnValidate()
        {
            var trans = transform;
            while (true)
            {
                var parent = trans.parent;
                if (parent is null)
                {
                    return;
                }
                var section = parent.gameObject.GetComponent<MenuSection>();
                if (section is not null)
                {
                    _from = section;
                    return;
                }
                trans = parent.transform;
            }
        }

        public void Travel()
        {
            if (_from is not null)
            {
                _from.gameObject.SetActive(false);
            }

            _to.gameObject.SetActive(true);
        }
    }
}
