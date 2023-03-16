using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[ExecuteAlways]
public class StairsGenerator : MonoBehaviour
{
    private uint _stairsCountOld;
    public uint stairsCount;
    public uint maxStairsCount = 500;
    
    public Transform stairsStorage;
    public GameObject stairPrefab;

    /// <summary>
    /// вызывается при изменении public полей
    /// </summary>
    public void OnValidate()
    {
        if (stairsCount > maxStairsCount)
        {
            stairsCount = maxStairsCount;
        }

        if (stairsCount != _stairsCountOld)
        {
            OnStairsCountEdited();
        }
    }

    async void DestroyGameObjectsWithDelay(GameObject[] gameObjectsToDestroy)
    {
        try
        {
            await UniTask.Delay(300); //надо некоторое время подождать, удалять только в следующих кадрах можно

            if (runInEditMode) //если в едиторе
            {
                foreach (var gameObjectToDestroy in gameObjectsToDestroy)
                {
                    DestroyImmediate(gameObjectToDestroy); //метод для едитора
                }
            }
            else //если в игре
            {
                foreach (var gameObjectToDestroy in gameObjectsToDestroy)
                {
                    Destroy(gameObjectToDestroy); //метод для игры
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void OnStairsCountEdited()
    {
        //удалить дочерние ступеньки если они есть
        if (stairsStorage.childCount > 0)
        {
            GameObject[] gameObjectsToDestroy = stairsStorage.GetChildGameObjects();
            DestroyGameObjectsWithDelay(gameObjectsToDestroy); //асинхронно выполнится позже (обычно после создания новых ступенек). ошибки сюда не выкидывает
        }

        var parentPosition = stairsStorage.position;
        
        for (int i = 0; i < stairsCount; i++)
        {
            Instantiate(stairPrefab, new Vector3(parentPosition.x,  parentPosition.y + i*0.5f, parentPosition.z + i*0.5f), Quaternion.Euler(0, 0, 0), stairsStorage);
        }

        _stairsCountOld = stairsCount;
    }
}