using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameSceneController : MonoBehaviour
{
    [SerializeField] private HUDController      hud;

    [Header("Game Over Parameters")]
    [SerializeField] private int                maxTrainsLost   = 5;
    [SerializeField] private float              gameOverDelay   = 1.5f;

    [Header("Offscreen Train")]
    [SerializeField] private GameObject offScreenPrefab;
    [SerializeField] private float      offScreenSpawnHeight    = 10.0f;
    [SerializeField] private float      offScreenSpawnDelay     = 1.0f;

    private int currScore   = 0;
    private int trainsLost  = 0;

    public static System.Action<int> OnScoreUpdated;
    public static System.Action<string> OnTrainLost;

    private void Start()
    {
        if (OnScoreUpdated != null)
            OnScoreUpdated(currScore);

        if (OnTrainLost != null)
            OnTrainLost(trainsLost.ToString() + "/" + maxTrainsLost.ToString());
    }

    public void IncrementScore(int delta = 1)
    {
        currScore += delta;

        if (OnScoreUpdated != null)
            OnScoreUpdated(currScore);
    }

    public void HandleLostTrain(Sprite sprite, bool isOffscreen, float xPos, int delta = 1)
    {
        trainsLost += delta;

        if (OnTrainLost != null)
            OnTrainLost(trainsLost.ToString() + "/" + maxTrainsLost.ToString());

        if (HasLostGame())
            StartCoroutine(DelayGameOverCR());

        if (isOffscreen)
            StartCoroutine(DelayedSpawnCR(sprite, xPos));
    }

    private IEnumerator DelayedSpawnCR(Sprite sprite, float xPos)
    {
        yield return new WaitForSeconds(offScreenSpawnDelay);
        SpawnOffscreen(sprite, xPos);
    }

    private void SpawnOffscreen(Sprite sprite, float xPos)
    {
        GameObject go = Instantiate(offScreenPrefab
            , new Vector3(xPos, offScreenSpawnHeight, 1)
            , Quaternion.identity);

        OffscreenTrain ot = go.GetComponent<OffscreenTrain>();

        if (ot == null) return;

        ot.Initialize(sprite);
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
