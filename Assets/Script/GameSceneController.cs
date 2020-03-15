using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameSceneController : MonoBehaviour
{
    [Header("Text Reference")]
    [SerializeField] private TextMeshProUGUI    currScoreText;
    [SerializeField] private TextMeshProUGUI    trainsLostText;

    [Header("Game Over Parameters")]
    [SerializeField] private int                maxTrainsLost   = 5;
    [SerializeField] private float              gameOverDelay = 1.5f;

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

        if (HasLostGame())
            StartCoroutine(DelayGameOverCR());
    }

    private bool HasLostGame()
    {
        return trainsLost >= maxTrainsLost;
    }

    private IEnumerator DelayGameOverCR()
    {
        yield return new WaitForSeconds(gameOverDelay);

        SceneManage.OpenSingleScene("GameOver");
    }
}
