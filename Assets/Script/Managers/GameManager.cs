using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField] private BGDatabase bgDatabase;

    private void Start()
    {
        AudioManager.Manager.PlayAudio("BGM");
    }

    public Sprite GetRandomBG()
    {
        int randomIndex = Utilities.GetRandomIndex(bgDatabase.data.Count);

        return bgDatabase.data[randomIndex].BGImage;
    }
}
