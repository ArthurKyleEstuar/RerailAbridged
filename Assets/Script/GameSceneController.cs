using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameSceneController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI    currScoreText;
    [SerializeField] private TextMeshProUGUI    trainsLostText;
    [SerializeField] private int                maxTrainsLost   = 5;

    private int currScore   = 0;
    private int trainsLost  = 0;

    private void Start()
    {
        if (currScoreText != null)
            currScoreText.text = currScore.ToString();

        if (trainsLostText != null)
            trainsLostText.text = trainsLost.ToString() + "/" + maxTrainsLost.ToString();
    }

    public void IncrementScore(int delta = 1)
    {
        currScore += delta;

        if(currScoreText != null)
            currScoreText.text = currScore.ToString();
    }

    public void IncrementLostTrains(int delta = 1)
    {
        trainsLost += delta;

        if (trainsLostText != null)
            trainsLostText.text = trainsLost.ToString() + "/" + maxTrainsLost.ToString();
    }
}
