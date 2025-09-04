using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public T Create<T>(string path, Transform parent) where T : Object
    {
        T obj = Load<T>(path);
        if (obj == null)
        {
            Debug.LogError($"Resource not found: {path}");
            return null;
        }
        T go = Instantiate(obj, parent);
        return go;
    }

    public T CreateBarUI<T>(string path, Transform parent, Vector3 position) where T : Object
    {
        T obj = Load<T>(path);
        if (obj == null)
        {
            Debug.LogError($"Resource not found: {path}");
            return null;
        }
        T go = Instantiate(obj, position, Quaternion.identity, parent);
        return go;
    }
}
