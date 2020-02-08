using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Animator animator;

    private bool isMovingRight;
    Vector3 newRot = Vector3.zero;

    private const float animAllowance = 0.2f;

    private void FixedUpdate()
    {
        UpdateSpeed();
        UpdateRotation();
    }

    private void UpdateSpeed()
    {
        if (animator == null || playerMove == null) return;

        animator.SetFloat("playerSpeed", Mathf.Abs(playerMove.CurrSpeed));
    }

    private void UpdateRotation()
    {
        float speed = playerMove.CurrSpeed;
       
        if (speed > 0)
            newRot.y = 0;
        else if (speed < 0)
            newRot.y = 180;

        this.transform.localEulerAngles = newRot;
    }

    #region Animator accessors
    public void SetAnimBool(string name, bool val)
    {
        if (animator == null) return;

        animator.SetBool(name, val);
    }

    public void SetAnimBool(string name, bool val, float duration)
    {
        StartCoroutine(AnimBoolActionCR(name, val, duration));
    }

    private IEnumerator AnimBoolActionCR(string name, bool val, float duration)
    {
        SetAnimBool(name, val);

        yield return new WaitForSeconds(duration);

        SetAnimBool(name, !val);
    }
    #endregion
}
