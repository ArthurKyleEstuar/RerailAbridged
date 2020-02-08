using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerAnim playerAnim;
    [SerializeField] private float repairDuration = 0.3f;

    private PlayerInputAction inputAction;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
        inputAction.PlayerControls.Repair.performed += Repair;
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void Repair(InputAction.CallbackContext obj)
    {
        if (!obj.performed || playerMove == null) return;

        playerMove.StopMove(repairDuration);
        StartCoroutine(RepairActionCR());
    }

    private IEnumerator RepairActionCR()
    {
        if (playerAnim == null) yield break;

        playerAnim.SetAnimBool("isRepair", true, repairDuration);
        yield return new WaitForSeconds(repairDuration / 2);
        //TODO Get overlapped track and fix it
        Debug.Log("Repair Track");
    }
}
