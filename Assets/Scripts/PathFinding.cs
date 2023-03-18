using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

//https://www.red-gate.com/simple-talk/development/dotnet-development/pathfinding-unity-c/

public class PathFinding : MonoBehaviour
{
    public Transform pointsStorageToPatrol;
    public Transform targetToCatch;
    public float fieldOfView = 90;
    public float maxRange = 35;
    
    public UnityEvent onTargetCatched;
    
    [Header("Debug")]
    public bool debug = false;
    public TextMeshProUGUI text;
    public Transform goesToThatPosition;
    
    private NavMeshAgent nav;
    private int destPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    //установить следующую точку как цель
    void GoToNextPoint()
    {
        if (pointsStorageToPatrol.childCount == 0)
        {
            return;
        }
        nav.destination = pointsStorageToPatrol.GetChild(destPoint).position;
        
        if (debug)
            goesToThatPosition.position = nav.destination;
        
        destPoint = (destPoint + 1) % pointsStorageToPatrol.childCount;
    }

    public void RestartFirstLevel()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    
    void FixedUpdate()
    {
        //расстояние до игрока
        var heading = targetToCatch.position - this.transform.position;
        if (heading.sqrMagnitude < maxRange * maxRange) { //если до игрока достаточно малое расстояние
            if(debug)
                text.text = "Вы замечены";
            // Target is within range.
            nav.destination = targetToCatch.position; //установка цели для преследования
            if (debug)
                goesToThatPosition.position = nav.destination;
            
            if (!nav.pathPending && nav.remainingDistance < 1.5f) //если игрок суперблизко
            {
                //игрок пойман
                if(debug)
                    Debug.Log("player catched");
                onTargetCatched?.Invoke();
            }
            
            return;
        }
        
        text.text = "";
        //до сюда доходит когда игрока рядом нет
        if (!nav.pathPending && nav.remainingDistance < 0.5f) //если суперблизко к точке патрулирования
            GoToNextPoint();
    }
    
}
