using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
// ReSharper disable StringLiteralTypo

// по этому мануалу делал
//https://www.red-gate.com/simple-talk/development/dotnet-development/pathfinding-unity-c/

public class GuardMovement : MonoBehaviour
{
    //сюда просто обьекты добавить, и он будет патрулировать их
    public Transform pointsStorageToPatrol;
    //кого ловить
    public Transform targetToCatch;
    //пока что не ворк
    public float fieldOfView = 90;
    //как близко игрок должен находиться, чтоб увидеть его
    public float maxRange = 35;
    //навигация AI
    public NavMeshAgent nav;
    
    //вызывается когда игрок суперблизко к охраннику
    public UnityEvent onTargetCatched;
    
    [Header("Debug")]
    //выводить на экран если видит, ставить метку куда идет
    public bool debug;
    //текст на экран, мол найден охранником
    public TextMeshProUGUI text;
    //телепортирует этот обьект туда, куда идет
    public Transform goesToThatPosition;
    
    //номер обьекта, к которому идет
    private int _destPoint;

    /// <summary>
    /// установить следующую точку как цель
    /// </summary>
    void GoToNextPoint()
    {
        if (pointsStorageToPatrol.childCount == 0)
        {
            return;
        }
        nav.destination = pointsStorageToPatrol.GetChild(_destPoint).position;
        
        if (debug)
            goesToThatPosition.position = nav.destination;
        
        _destPoint = (_destPoint + 1) % pointsStorageToPatrol.childCount;
    }

    public void RestartFirstLevel()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    
    void FixedUpdate()
    {
        //расстояние до игрока
        var vectorToPlayer = targetToCatch.position - this.transform.position;
        if (vectorToPlayer.sqrMagnitude < maxRange * maxRange) { //если до игрока достаточно малое расстояние
            
            nav.destination = targetToCatch.position; //установка цели для преследования

            if (debug)
            {
                text.text = "Вы замечены";
                goesToThatPosition.position = nav.destination;
            }

            if (!nav.pathPending && nav.remainingDistance < 1.5f) //если игрок суперблизко
            {
                //игрок пойман
                if(debug)
                    Debug.Log("player catched");
                onTargetCatched?.Invoke();
            }
            
            return;
        }
        
        //до сюда доходит когда игрок не замечен
        text.text = "";
        
        if (!nav.pathPending && nav.remainingDistance < 0.5f) //если суперблизко к точке патрулирования
            GoToNextPoint();
    }
    
}
