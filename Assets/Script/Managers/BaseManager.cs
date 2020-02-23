using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool isShuttingDown = false;
    private static T instance;
    
    public static T Manager
    {
        get
        {
            if(isShuttingDown)
            {
                Debug.LogErrorFormat("{0} is shutting down! Returning null", nameof(T));
                return null;
            }

            if (instance == null)
                instance = (T)FindObjectOfType(typeof(T));

            if(instance == null)
            {
                Debug.LogErrorFormat("{0} cannot be found! Returning null", nameof(T));
            }

            return instance;
        }
    }

    public static bool IsShuttingDown { get { return isShuttingDown; } }

    protected void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    protected void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    protected void OnDestroy()
    {
        isShuttingDown = true;
    }
}
