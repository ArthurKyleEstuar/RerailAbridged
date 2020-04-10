using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseData
{
    public string ID { get { return id; } }

    [SerializeField] private string id;
}

public class BaseDatabase<T> : ScriptableObject where T : BaseData
{
    public List<T> data;
    public int ListCount { get { return data.Count; } }

    public T GetFile(string id)
    {
        T returnVal = data.Find(obj => obj.ID == id);

        if (returnVal == null)
            Debug.LogErrorFormat("Could not find ID {0} in {1} DB", id, nameof(T));

        return returnVal;
    }

    public T GetRandomFile()
    {
        T returnVal = data[Random.Range(0, data.Count)];

        return returnVal;
    }
}
