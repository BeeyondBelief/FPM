using System.Collections.Generic;
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
    }
}
