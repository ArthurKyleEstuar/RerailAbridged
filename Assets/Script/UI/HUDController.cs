﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject         player;
    [SerializeField] private TrackManager       trackManager;

    [Header("Check intervals")]
    [SerializeField] private float              checkInterval   = 1.0f;

    [Header("Break Notifs")]
    [SerializeField] private GameObject         leftBreakNotif;
    [SerializeField] private GameObject         rightBreakNotif;

    [Header("Popups")]
    [SerializeField] private GameObject         invalidToolPopup;

    [Header("Score Text")]
    [SerializeField] private TextMeshProUGUI    currScoreText;
    [SerializeField] private TextMeshProUGUI    trainsLostText;
    
    [Header("Tool Data")]
    [SerializeField] private Image              toolImage;
    [SerializeField] private Image              toolBoxImage;

    private void Awake()
    {
        GameSceneController.OnScoreUpdated  += SetScoreDisplay;
        GameSceneController.OnTrainLost     += SetTrainsLost;
        PlayerItemController.OnToolSwitched += SetToolDisplay;
        PlayerItemController.OnPickupToolbox += SetHasToolboxDisplay;
        TrackManager.OnInvalidToolUsed += SetInvalidToolPopup;
    }

    private void OnDisable()
    {
        GameSceneController.OnScoreUpdated -= SetScoreDisplay;
        GameSceneController.OnTrainLost -= SetTrainsLost;
        PlayerItemController.OnToolSwitched -= SetToolDisplay;
        PlayerItemController.OnPickupToolbox -= SetHasToolboxDisplay;
        TrackManager.OnInvalidToolUsed -= SetInvalidToolPopup;
    }

    private void Start()
    {
        StartCoroutine(SetNotifsCR());

        SetInvalidToolPopup(false);
    }

    public void SetInvalidToolPopup(bool isActive)
    {
        if (invalidToolPopup == null) return;

        invalidToolPopup.SetActive(isActive);

        if (isActive)
            StartCoroutine(DelayHideWrongToolCR());
    }

    private IEnumerator DelayHideWrongToolCR()
    {
        yield return new WaitForSeconds(3);

        SetInvalidToolPopup(false);
    }

    private void SetScoreDisplay(int score)
    {
        if (currScoreText == null) return;

        currScoreText.text = score.ToString();
    }

    private void SetTrainsLost(string trainLostString)
    {
        if (trainsLostText == null) return;

        trainsLostText.text = trainLostString;
    }

    private void SetToolDisplay(ItemData currTool)
    {
        if (toolImage == null) return;

        toolImage.sprite = currTool.ItemSprite;
    }

    private void SetHasToolboxDisplay(bool hasToolbox)
    {
        if (toolBoxImage == null) return;

        toolBoxImage.gameObject.SetActive(hasToolbox);
    }

    //Do this by interval to reduce load on Update
    private IEnumerator SetNotifsCR()
    {
        bool leftBroken = false;
        bool rightBroken = false;

        List<Track> tracks = trackManager.DamagedTracks;

        yield return new WaitForSeconds(checkInterval);

        foreach (Track track in tracks)
        {
            //Dont need to show notif if already rendered on screen
            if (track.IsRendered) continue;

            if (track.transform.position.x > player.transform.position.x)
            {
                rightBroken = true;
            }
            else if (track.transform.position.x < player.transform.position.x)
            {
                leftBroken = true;
            }
        }

        if(leftBreakNotif != null)
            leftBreakNotif.SetActive(leftBroken);

        if(rightBreakNotif != null)
            rightBreakNotif.SetActive(rightBroken);

        StartCoroutine(SetNotifsCR());
    }
}
