using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject trackPrefab;
    [SerializeField] private float trackSize = 3.2f;
    [SerializeField] private int trackCount = 30;

    private void Start()
    {
        if(trackPrefab == null)
        {
            Debug.LogError("Track prefab missing!");
            return;
        }
        
        for(int i = 0; i < 30; i++)
        {
            Vector3 spawnPos = this.transform.position;
            spawnPos.x = spawnPos.x + (i * trackSize);

            GameObject go = Instantiate(trackPrefab, spawnPos, Quaternion.identity);
            Track track = go.GetComponent<Track>();

            if (track != null && i > 20) 
                track.ToggleTrack(true);
        }
    }
}
