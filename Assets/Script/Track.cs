using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private Sprite unbrokenSprite;
    [SerializeField] private Sprite brokenSprite;

    private bool isBroken;

    private void Start()
    {
        
    }
    
    public void ToggleTrack(bool newVal)
    {
        isBroken = newVal;
        if (render != null)
            render.sprite = (isBroken) ? brokenSprite : unbrokenSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Train" && isBroken)
        {
            Train train = collision.gameObject.GetComponent<Train>();

            if (train == null) return;

            train.LaunchTrain();
        }
    }
}

