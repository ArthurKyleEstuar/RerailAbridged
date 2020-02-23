using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSafeZone : MonoBehaviour
{
    private bool isFirstTrain = false;
    private bool isLastTrain = false;

    public void Initialize(bool isFirst = false, bool isLast = false)
    {
        isFirstTrain = isFirst;
        isLastTrain = isLast;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFirstTrain) return;
       //Prevent track from breaking
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isLastTrain) return;
        //Allow track to break
    }
}
