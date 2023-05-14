using System.Collections.Generic;
using Player;
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
        private PlayerMovement? _catchTarget;
        #nullable disable

        [SerializeField]
        private List<SearchTactic> _tactics;

        [Tooltip("Дистанция срабатывания захвата объекта.")]
        public float catchDistance = 1.5f;

        [Tooltip("Событие, срабатывает если объект схвачен.")]
        public UnityEvent onTargetCaught;

        public float waitOnControlPoints = 1f;

        private SearchTactic _workedTactic;
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
            if (_catchTarget is not null)
            {
                foreach (var tactic in _tactics)
                {
                    if (tactic.Work(this, _catchTarget))
                    {
                        _workedTactic = tactic;
                        ChaiseTarget();
                        break;
                    }
                }
                MaybeCaught();
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

        private void MaybeCaught()
        {
            if (!Chasing || _catchTarget is null)
            {
                return;
            }
            var vectorToPlayer = _catchTarget.transform.position - transform.position;

            if (!_nav.pathPending && vectorToPlayer.magnitude < catchDistance)
            {
                onTargetCaught?.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, catchDistance);
            Gizmos.DrawLine(transform.position, _nav.destination);
        }
    }
}
