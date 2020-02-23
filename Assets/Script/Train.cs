using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D    rb;

    [Header("Movement")]
    [SerializeField] private float          trainSpeed      = 10.0f;

    [Header("Launch Properties")]
    [SerializeField] private Vector2        launchRotRange  = new Vector2(-180, 180);
    [SerializeField] private Vector2        minLaunchForce  = new Vector2(-200, 500);
    [SerializeField] private Vector2        maxLaunchForce  = new Vector2(200, 1500);
    private Vector3 currPos;
    private float   rotRate;
    private bool    isBroke = false;

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

    public void LaunchTrain()
    {
        isBroke = true;

        float xLaunchForce = Random.Range(minLaunchForce.x, maxLaunchForce.x);
        float yLaunchForce = Random.Range(minLaunchForce.y, maxLaunchForce.y);

        Vector2 randomForce = new Vector2(xLaunchForce, yLaunchForce);
        rotRate = Random.Range(launchRotRange.x, launchRotRange.y);

        this.gameObject.layer = 9;

        if (rb != null)
            rb.AddForce(randomForce);
    }
}
