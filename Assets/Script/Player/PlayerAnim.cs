using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private PlayerMove playerMove;
    private Animator animator;

    private bool isMovingRight;
    Vector3 newRot = Vector3.zero;

    private const float animAllowance = 0.2f;

    private void Start()
    {
        playerMove = this.GetComponent<PlayerMove>();
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateSpeed();
        UpdateRotation();
    }

    private void UpdateSpeed()
    {
        if (!animator || !playerMove) return;

        animator.SetFloat("playerSpeed", Mathf.Abs(playerMove.CurrSpeed));
    }

    private void UpdateRotation()
    {
        float speed = playerMove.CurrSpeed;
       
        if (speed > 0)
        {
            newRot.y = 0;
        }
        else if (speed < 0)
        {
            newRot.y = 180;
        }

        this.transform.localEulerAngles = newRot;
    }
}
