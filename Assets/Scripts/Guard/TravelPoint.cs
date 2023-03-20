using System;
using UnityEngine;

namespace Guard
{
    public class TravelPoint : MonoBehaviour
    {
        public float pointRadius = 0.5f;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position, pointRadius);
        }
    }
}
