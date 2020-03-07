using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TrackManager trackManager;

    [Header("Check intervals")]
    [SerializeField] private float checkInterval = 1.0f;

    [Header("Break Notifs")]
    [SerializeField] private GameObject leftBreakNotif;
    [SerializeField] private GameObject rightBreakNotif;

    private float currInterval;

    private void Start()
    {
        StartCoroutine(SetNotifsCR());
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
