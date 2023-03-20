using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
// ReSharper disable StringLiteralTypo

// по этому мануалу делал
//https://www.red-gate.com/simple-talk/development/dotnet-development/pathfinding-unity-c/

public class GuardMovement : MonoBehaviour
{
    [Tooltip("Объекты патрулирования.")]
    public Transform travelRoute;
    [Tooltip("Дистанция, при которой засчитывается достижение цели.")]
    public float travelReachedDistance = 0.5f;
    
    [Tooltip("Объект преследования.")]
    public Transform catchTarget;

    [Tooltip("Если объекты преследования находятся в радиусе поиска, они становятся целью.")]
    public float searchRange = 15f;

    [Tooltip("Навигатор")]
    public NavMeshAgent nav;
    
    [Tooltip("Дистанция срабатывания захвата объекта.")]
    public float catchDistance = 1.5f;
    
    [Tooltip("Событие, срабатывает если объект схвачен.")]
    public UnityEvent onTargetCaught;
    
    private int _destinationPoint;
    public bool Chaising { get; private set; }
    
    public void RestartThisLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Установить следующую точку как цель
    /// </summary>
    private void SetNextPoint()
    {
        if (travelRoute.childCount == 0)
        {
            return;
        }
        nav.destination = travelRoute.GetChild(_destinationPoint).position;
        _destinationPoint = (_destinationPoint + 1) % travelRoute.childCount;
    }

    private void FixedUpdate()
    {
        var vectorToPlayer = catchTarget.position - transform.position;
        if (vectorToPlayer.magnitude > searchRange) {
            Chaising = false;
            if (!nav.pathPending && nav.remainingDistance < travelReachedDistance)
                SetNextPoint();
            return;
        }
        Chaising = true;
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
        if (!Chaising)
            Gizmos.DrawLine(transform.position, nav.destination);
    }
}
