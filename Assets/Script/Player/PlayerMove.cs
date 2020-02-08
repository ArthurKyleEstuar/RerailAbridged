using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float acceleration = 5.0f;
    [SerializeField] private float allowance = 0.4f;
    [SerializeField] private Vector2 speedCap = new Vector2(-10, 10);
    [SerializeField] private Rigidbody2D rb;

    private float currSpeed;
    private float currAccel;
    private float dir;

    public float CurrSpeed { get { return currSpeed; } }

    private void Start()
    {
        currAccel = acceleration;
    }

    private void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");
        if (input == 0)
        {
            dir = 0;

            if (currSpeed > allowance)
                currSpeed -= GetDeccelerate();
            else if (currSpeed < -allowance)
                currSpeed += GetDeccelerate();
            else
                currSpeed = 0;
        }
        else if (input > 0)
        {
            dir = 1;
            currSpeed += GetForce();
        }
        else if (input < 0)
        {
            dir = -1;
            currSpeed += GetForce();
        }

        currSpeed = Mathf.Clamp(currSpeed, speedCap.x, speedCap.y);

        if (rb == null) return;
        rb.velocity = new Vector2(currSpeed, 0);
    }

    private float GetForce()
    {
        return Time.deltaTime * currAccel * dir;
    }

    private float GetDeccelerate()
    {
        return Time.deltaTime * currAccel;
    }

    public void SetAcceleration(float newAccel)
    {
        currAccel = newAccel;
    }

    public void ResetAcceleration()
    {
        currAccel = acceleration;
    }
}
