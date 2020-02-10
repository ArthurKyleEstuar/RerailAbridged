using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float trainSpeed = 10.0f;

    private Vector3 currPos;
    private bool isBroke = false;

    private void FixedUpdate()
    {
        if (isBroke) return;
        currPos = this.transform.position;
        currPos.x += trainSpeed * Time.deltaTime;
        this.transform.position = currPos;
    }

    public void LaunchTrain()
    {
        Vector2 randomForce = new Vector2(Random.Range(-200, 200), 500);

        this.gameObject.layer = 9;

        if (rb != null)
            rb.AddForce(randomForce);
    }
}
