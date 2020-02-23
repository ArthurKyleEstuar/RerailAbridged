using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioObject : MonoBehaviour
{
    private AudioSource aSource;
    private AudioData aFile;
    public string TrackId { get { return aFile.ID; } }

    public void Initialize(AudioData fileToLoad)
    {
        if (fileToLoad == null)
        {
            Debug.LogError("NO file to load! AudioFile may not be in DB " + fileToLoad.ID);
            return;
        }
        aFile = fileToLoad;

        aSource = this.gameObject.AddComponent<AudioSource>();
        aSource.clip = aFile.AudioClip;
        aSource.outputAudioMixerGroup = AudioManager.Manager.Mixer.FindMatchingGroups(aFile.MixGroup)[0];
        aSource.volume = aFile.Volume;
        aSource.loop = aFile.IsLooping;
        aSource.Play();
    }

    public void Play()
    {
        aSource.Play();
    }
}
