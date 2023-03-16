using UnityEngine;

public static class TransformExtensions
{
    public static Transform[] GetChildTransforms(this Transform transform)
    {
        int count = transform.childCount;
        Transform[] child = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            child[i] = transform.GetChild(i);
        }

        return child;
    }
        
    public static GameObject[] GetChildGameObjects(this Transform transform)
    {
        int count = transform.childCount;
        GameObject[] child = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            child[i] = transform.GetChild(i).gameObject;
        }

        return child;
    }
}
