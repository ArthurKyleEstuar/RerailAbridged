using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioData : BaseData
{
    public AudioClip    AudioClip   { get { return audioClip; } }
    public bool         IsLooping   { get { return isLooping; } }
    public string       MixGroup    { get { return mixGroup; } }
    public float        Volume      { get { return volume; } }

    [SerializeField] private AudioClip  audioClip;
    [SerializeField] private bool       isLooping;
    [SerializeField] private string     mixGroup;
    [SerializeField] private float      volume;
}

[CreateAssetMenu(fileName = "AudioDatabase", menuName = "Database/AudioDatabase")]
public class AudioDatabase : BaseDatabase<AudioData>
{
    
}
