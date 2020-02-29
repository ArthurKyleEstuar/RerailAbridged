using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    /// <summary>
    /// Search for component in target through all its parents and children. WARNING! EXPENSIVE FUNCTION, FOR PROTOTYPE ONLY 
    /// </summary>
    /// <typeparam name="T"> Component to get </typeparam>
    /// <param name="target"> Object to search </param>
    /// <returns></returns>
    public static T GetComponentDeep<T>(GameObject target)
    {
        var comp = target.GetComponent<T>();

        if (comp == null)
            comp = target.GetComponentInChildren<T>();

        if (comp == null)
            comp = target.GetComponentInParent<T>();

        if (comp == null)
            Debug.LogErrorFormat("No {0} found in {1}", nameof(T), target.name);

        return comp;
    }

    public static int GetRandomIndex(int listSize)
    {
        return (int)Random.Range(0.0f, listSize - 1);
    }
}
