using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenTrain : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private SpriteRenderer sr;

    [Header("Fall Properties")]
    [SerializeField] private Vector2 rotationForceRange = new Vector2(100.0f, 300.0f);
    [SerializeField] private Vector2 fallSpeedMax       = new Vector2(300.0f, 500.0f);
    [SerializeField] private Vector2 fallSpeedMin       = new Vector2(100.0f, 300.0f);

    private Vector3 currFallSpeed;
    private float   currFallRot;

    private void Start()
    {
        currFallSpeed = new Vector3(Random.Range(fallSpeedMin.x, fallSpeedMax.x)
            , Random.Range(fallSpeedMin.y, fallSpeedMax.y)
            , 0);

        currFallRot = Random.Range(rotationForceRange.x, rotationForceRange.y);
    }

    public void Initialize(Sprite trainSprite)
    {
        if (sr == null) return;

        sr.sprite = trainSprite;
    }

    private void FixedUpdate()
    {
        Vector3 currPos = this.transform.position;
        currPos -= currFallSpeed * Time.deltaTime;
        currPos.z = 1;
        this.transform.position = currPos;

        Vector3 currRot = this.transform.localEulerAngles;
        currRot.z += currFallRot * Time.deltaTime;
        this.transform.localEulerAngles = currRot;
    }

}
