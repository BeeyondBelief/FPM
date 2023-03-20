using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Guard
{
    public class TravelRoute : MonoBehaviour
    {
        
        public List<TravelPoint> points;
        private int _currentPoint;
        
        #nullable enable
        public TravelPoint? GetNext()
        {
            if (points.Count == 0)
            {
                return null;
            }
            var point = points[_currentPoint];
            _currentPoint = (_currentPoint + 1) % points.Count;
            return point;
        }

        private void OnDrawGizmos()
        {
            for (var i = 0; i < points.Count; i++)
            {
                var point = points[i];
                var next = points[(i + 1) % points.Count];
                var pos = point.transform.position;
                Gizmos.DrawLine(pos, next.transform.position);
                pos.y += point.pointRadius*2;
                Handles.Label(pos, i.ToString());
            }
        }
    }
}
