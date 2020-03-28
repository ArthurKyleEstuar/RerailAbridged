using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D    rb;
    [SerializeField] private TrainSafeZone  trainSafeZone;
    [SerializeField] private SpriteRenderer sr;

    [Header("Movement")]
    [SerializeField] private float          trainSpeed      = 10.0f;

    [Header("Launch Properties")]
    [SerializeField] private Vector2        launchRotRange  = new Vector2(-180, 180);
    [SerializeField] private Vector2        minLaunchForce  = new Vector2(-200, 500);
    [SerializeField] private Vector2        maxLaunchForce  = new Vector2(200, 1500);

    [Header("Offscreen Launch Properties")]
    [SerializeField] private float          offScreenChance = 20.0f;
    [SerializeField] private float          yOffscreenForce = 1900.0f;

    private Vector3                         currPos;
    private float                           rotRate;
    private bool                            isBroke         = false;
    private GameSceneController             gameController;

    private void FixedUpdate()
    {
        //Move forward
        currPos = this.transform.position;
        currPos.x += trainSpeed * Time.deltaTime;
        this.transform.position = currPos;

        //Flip
        if (isBroke)
        {
            Vector3 newRot = this.transform.localEulerAngles;
            newRot.z += rotRate * Time.deltaTime;

            this.transform.localEulerAngles = newRot;
        }
    }

    public void Initialize(GameSceneController gc, bool isFirstTrain = false, bool isLastTrain = false)
    {
        if (trainSafeZone == null) return;

        gameController = gc;

        trainSafeZone.Initialize(isFirstTrain, isLastTrain);
    }

    public void LaunchTrain()
    {
        isBroke = true;

        float offScreenRoll = Random.Range(0, 100);
        bool isOffscreenLaunch = offScreenRoll <= offScreenChance;

        if (gameController != null && sr != null)
            gameController.HandleLostTrain(sr.sprite
                , isOffscreenLaunch
                , this.transform.position.x);

        Vector2 randomForce = GetLaunchForce(isOffscreenLaunch);

        rotRate = Random.Range(launchRotRange.x, launchRotRange.y);

        this.gameObject.layer = 9;

        if (rb != null)
            rb.AddForce(randomForce);
    }

    public void DeleteTrain(float destroyDelay = 2.0f)
    {
        Destroy(this.gameObject, destroyDelay);
    }

    private Vector2 GetLaunchForce(bool isOffScreen)
    {
        float xForce = 0;
        float yForce = 0;

        if (isOffScreen)
            yForce = yOffscreenForce;
        else
            yForce = Random.Range(minLaunchForce.y, maxLaunchForce.y);

        return new Vector2(xForce, yForce);
    }
}
