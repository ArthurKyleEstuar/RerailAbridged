using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField] private GameSceneController    gameController;
    [SerializeField] private ItemDatabase           toolDB;

    [Header("Prefab")]
    [SerializeField] private GameObject             trackPrefab;
    [SerializeField] private GameObject             trackEndPrefab;

    [Header("Spawn Parameters")]
    [SerializeField] private float                  trackSize       = 3.2f;
    [SerializeField] private float                  endSize         = 8.0f;
    [SerializeField] private float                  heightOffset    = -0.275f;
    [SerializeField] private int                    trackCount      = 30;

    [Header("Break Parameters")]
    [SerializeField] private float                  breakInterval   = 5.0f;

    private List<Track> tracks          = new List<Track>();
    private List<Track> damagedTracks   = new List<Track>();

    public List<Track> DamagedTracks    { get { return damagedTracks; } }
    public ItemDatabase ToolDB          { get { return toolDB; } }

    public static System.Action<bool> OnInvalidToolUsed;

    private void Start()
    {
        SpawnTracks();

        StartCoroutine(RandomBreakCR());
    }

    [ContextMenu("Spawn Track")]
    private void SpawnTracks()
    {
        if (trackPrefab == null || trackEndPrefab == null)
        {
            Debug.LogError("Track prefab missing!");
            return;
        }

        //Spawn the initial track piece
        Vector3 initPos = this.transform.position;
        initPos.y += heightOffset;
        GameObject initTrack = Instantiate(trackEndPrefab, initPos, Quaternion.identity);
        initTrack.transform.parent = this.transform;

        //Spawn the middle tracks
        for (int i = 0; i < trackCount; i++)
        {
            Vector3 spawnPos = this.transform.position;
            spawnPos.x = spawnPos.x + endSize + (i * trackSize);

            GameObject go = Instantiate(trackPrefab, spawnPos, Quaternion.identity);
            go.transform.parent = this.transform;

            Track track = go.GetComponent<Track>();

            if (track == null) continue;

            tracks.Add(track);
            track.Initialize(this);
        }

        //Spawn the end track piece
        Vector3 endPos = initPos;
        endPos.x += endSize + ((trackCount + 1) * trackSize);

        GameObject endTrack = Instantiate(trackEndPrefab, endPos, Quaternion.identity);
        endTrack.transform.localEulerAngles = new Vector3(0, 180, 0);
        endTrack.transform.parent = this.transform;

        TrackEnd trackEnd = Utilities.GetComponentDeep<TrackEnd>(endTrack);

        if (trackEnd != null)
            trackEnd.Initialize(true, gameController);
    }

    private IEnumerator RandomBreakCR()
    {
        yield return new WaitForSeconds(breakInterval);

        int randomIndex = Random.Range(0, tracks.Count);

        tracks[randomIndex].DamageTrack();

        StartCoroutine(RandomBreakCR());
    }

    public void AddDamaged(Track track)
    {
        damagedTracks.Add(track);
    }

    public void RemoveDamaged(Track track)
    {
        damagedTracks.Remove(track);
    }
    
    public void InvalidToolUsed()
    {
        if (OnInvalidToolUsed != null) OnInvalidToolUsed(true);
    }
  
}
