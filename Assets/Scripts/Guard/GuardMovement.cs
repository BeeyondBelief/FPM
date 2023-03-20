using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
// ReSharper disable StringLiteralTypo

namespace Guard
{
    public class GuardMovement : MonoBehaviour
    {
        [Tooltip("Объекты патрулирования.")] public TravelRoute travelRoute;

        [Tooltip("Дистанция, при которой засчитывается достижение цели.")]
        public float travelReachedDistance = 0.5f;

        [Tooltip("Объект преследования.")] public Transform catchTarget;

        [Tooltip("Если объекты преследования находятся в радиусе поиска, они становятся целью.")]
        public float searchRange = 15f;

        [Tooltip("Навигатор")] public NavMeshAgent nav;

        [Tooltip("Дистанция срабатывания захвата объекта.")]
        public float catchDistance = 1.5f;

        [Tooltip("Событие, срабатывает если объект схвачен.")]
        public UnityEvent onTargetCaught;

        public bool Chasing { get; private set; }

        public void RestartThisLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// Установить следующую точку как цель
        /// </summary>
        private void SetNextPoint()
        {
            var point = travelRoute.GetNext();
            if (point is not null)
                nav.destination = point.transform.position;
        }

        private void FixedUpdate()
        {
            var vectorToPlayer = catchTarget.position - transform.position;
            if (vectorToPlayer.magnitude > searchRange)
            {
                Chasing = false;
                if (!nav.pathPending && nav.remainingDistance < travelReachedDistance)
                    SetNextPoint();
                return;
            }

            Chasing = true;
            nav.destination = catchTarget.position;
            if (!nav.pathPending && nav.remainingDistance < catchDistance)
            {
                onTargetCaught?.Invoke();
                nav.destination = transform.position;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, searchRange);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, catchDistance);
            if (!Chasing)
                Gizmos.DrawLine(transform.position, nav.destination);
        }
    }
}
