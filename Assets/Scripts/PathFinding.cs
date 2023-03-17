using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

//https://www.red-gate.com/simple-talk/development/dotnet-development/pathfinding-unity-c/

public class PathFinding : MonoBehaviour
{
    public Transform targetToCatch;
    public float fieldOfView = 90;
    public float maxRange = 35;
    public Transform pointsStorage;
    //public Transform[] points;
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
        if (pointsStorage.childCount == 0)
        {
            return;
        }
        nav.destination = pointsStorage.GetChild(destPoint).position;
        destPoint = (destPoint + 1) % pointsStorage.childCount;
    }
    
    void FixedUpdate()
    {
        //расстояние до игрока
        var heading = targetToCatch.position - this.transform.position;
        if (heading.sqrMagnitude < maxRange * maxRange) { //если до игрока достаточно малое расстояние
            // Target is within range.
            nav.destination = targetToCatch.position; //установка цели для преследования

            if (!nav.pathPending && nav.remainingDistance < 1.5f) //если игрок суперблизко
            {
                //игрок пойман
                Debug.Log("player catched");
                SceneManager.LoadScene("FirstLevel");
            }
            
            return;
        }
        
        //до сюда доходит когда игрока рядом нет
        if (!nav.pathPending && nav.remainingDistance < 0.5f) //если суперблизко к точке патрулирования
            GoToNextPoint();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
