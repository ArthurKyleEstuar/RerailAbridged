using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSafeZone : MonoBehaviour
{
    [SerializeField] private bool isFirstTrain  = false;
    [SerializeField] private bool isLastTrain   = false;

    public void Initialize(bool isFirst = false, bool isLast = false)
    {
        isFirstTrain = isFirst;
        isLastTrain = isLast;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFirstTrain) return;

        if (collision.gameObject.tag == "Track")
        {
            //Debug.LogError("Prevent break");
        }
       //Prevent track from breaking
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isLastTrain) return;

        if (collision.gameObject.tag == "Track")
        {
            //Debug.LogError("Allow break");
        }
        //Allow track to break
    }
}
