using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Guard
{
    public class GuardMovement : MonoBehaviour
    {
        [Tooltip("Маршрут патрулирования.")]
        [SerializeField]
        private TravelRoute _travelRoute;
        
        [Tooltip("Навигатор")]
        [SerializeField]
        private NavMeshAgent _nav;

        #nullable enable
        [Tooltip("Объект преследования.")]
        [SerializeField]
        private Player.Player? _catchTarget;
        #nullable disable

        [SerializeField]
        private List<GuardSearch> _searches;

        [Tooltip("Дистанция срабатывания захвата объекта.")]
        public float catchDistance = 1.5f;

        [Tooltip("Событие, срабатывает если объект схвачен.")]
        public UnityEvent onTargetCaught;

        public float waitOnControlPoints = 1f;

        private float _currentWait;

        public bool Chasing { get; private set; }

        /// <summary>
        /// Устанавливает следующую точку как цель, если маршрут завершен.
        /// </summary>
        private void MaybeNextPoint()
        {
            if (!_nav.pathPending && _nav.remainingDistance < 0.1f)
            {
                Chasing = false;
                if (waitOnControlPoints - _currentWait > 0)
                {
                    _currentWait += Time.deltaTime;
                }
                else
                {
                    _currentWait = 0;
                    var point = _travelRoute.GetNext();
                    if (point is not null)
                        _nav.destination = point.transform.position;
                }
            }
        }
        
        private void FixedUpdate()
        {
            if (_catchTarget is null)
            {
                MaybeNextPoint();
                return;
            }
            foreach (var search in _searches)
            {
                if (search.Search(this, _catchTarget))
                {
                    if (search.IsCaught(this, _catchTarget))
                    {
                        onTargetCaught?.Invoke();
                        return;
                    }
                    ChaiseTarget();
                    break;
                }
            }
            MaybeNextPoint();
        }

        private void ChaiseTarget()
        {
            if (_catchTarget is null)
            {
                return;
            }
            Chasing = true;
            _nav.destination = _catchTarget.transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, catchDistance);
            Gizmos.DrawLine(transform.position, _nav.destination);
        }
    }
}
