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

        private void Awake()
        {
            FindParentSection();
        }

        private void FindParentSection()
        {
            var trans = transform;
            while (true)
            {
                var parent = trans.parent;
                if (parent is null)
                {
                    break;
                }
                var section = parent.gameObject.GetComponent<MenuSection>();
                if (section is not null)
                {
                    _from = section;
                    break;
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
