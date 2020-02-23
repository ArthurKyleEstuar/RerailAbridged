using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : BaseManager<AudioManager>
{
    [SerializeField] private AudioDatabase audioDB;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private List<AudioObject> audioObjects = new List<AudioObject>();

    public AudioMixer Mixer { get { return mixer; } }

    public void PlayAudio(string id)
    {
        if (audioObjects.Exists(obj => obj.TrackId == id))
        {
            AudioObject audioObj = audioObjects.Find(obj => obj.TrackId == id);
            audioObj.Play();

        }
        else
        {
            AudioObject audioObj = this.gameObject.AddComponent<AudioObject>();
            audioObj.Initialize(audioDB.GetFile(id));
            audioObjects.Add(audioObj);
        }
    }

    public void ToggleMixVolume(string mixName, int volState)
    {
        float newVolume = 0.0f;

        if (volState == 1)
            newVolume = 0.0f;
        else if (volState == 0)
            newVolume = -80.0f;

        mixer.SetFloat(mixName, newVolume);
    }
}
