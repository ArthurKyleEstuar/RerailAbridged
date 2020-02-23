using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Scene Components")]
    [SerializeField] private Rigidbody2D    rb;

    [Header("Move Parameters")]
    [SerializeField] private float          playerSpeed     = 10.0f;
    [SerializeField] private float          acceleration    = 5.0f;
    [SerializeField] private float          deccelAllowance = 0.4f;

    [Header("Limit")]    
    [SerializeField] private Vector2        speedCap        = new Vector2(-10, 10);
    
    [Header("Move State")]
    [SerializeField] private bool           canMove         = true;

    private PlayerInputAction inputAction;
    private Vector2 moveInput;
    private float currSpeed;
    private float currAccel;
    private float dir;
	

    public float CurrSpeed { get { return currSpeed; } }

    #region Init
    private void Awake()
    {
        inputAction = new PlayerInputAction();
        inputAction.PlayerControls.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void Start()
    {
        currAccel = acceleration;
    }
    #endregion

    private void FixedUpdate()
    {
        float input = moveInput.x;

        if (input == 0) //deccelerate toward 0
        {
            dir = 0;

            if (currSpeed > deccelAllowance)
                currSpeed -= GetDeccelerate();
            else if (currSpeed < -deccelAllowance)
                currSpeed += GetDeccelerate();
            else
                currSpeed = 0;
        }
        else
        {
            dir = (input > 0) ? 1 : -1;
            currSpeed += GetForce();
        }

        currSpeed = Mathf.Clamp(currSpeed, speedCap.x, speedCap.y);
        if (rb == null) return;

        //Stop moving when disabled
        if (!canMove)
            currSpeed = 0;

        rb.velocity = new Vector2(currSpeed, 0);
    }

    #region Calculations
    private float GetForce()
    {
        return Time.deltaTime * currAccel * dir;
    }

    private float GetDeccelerate()
    {
        return Time.deltaTime * currAccel;
    }
    #endregion

    #region Accel methods
    public void SetAcceleration(float newAccel)
    {
        currAccel = newAccel;
    }

    public void ResetAcceleration()
    {
        currAccel = acceleration;
    }
    #endregion

    #region Move Allow/Deny
    public void StopImmediately()
	{
		currAccel = 0;
		currSpeed = 0;
	}

    public void AllowMove()
    {
        canMove = true;
    }

    public void StopMove(float duration)
	{
        canMove = false;

        if (duration <= 0) return; //Skip the CR when no valid duration set
        StartCoroutine(TempRestrictMoveCR(duration));
	}

    private IEnumerator TempRestrictMoveCR(float duration)
    {
        yield return new WaitForSeconds(duration);
        canMove = true;
    }
    #endregion
}
