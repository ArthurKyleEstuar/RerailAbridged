using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackEnd : MonoBehaviour
{
    [SerializeField] private Collider2D trackCollider;

    private GameSceneController gameController;
    private bool isLastTrack = false;

    public void Initialize(bool lastTrack, GameSceneController gc)
    {
        if (trackCollider != null)
            trackCollider.isTrigger = lastTrack;

        gameController = gc;

        isLastTrack = lastTrack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Train" && isLastTrack)
        {
            Train train = collision.gameObject.GetComponent<Train>();

            if (train != null)
                train.DeleteTrain();

            if(gameController != null)
                gameController.IncrementScore();
        }
           
    }
}
