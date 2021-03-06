﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerMove             playerMove;
    [SerializeField] private PlayerAnim             playerAnim;
    [SerializeField] private PlayerItemController   playerItem;

    [Header("Anim timer")]
    [SerializeField] private float repairDuration = 0.3f;

    private PlayerInputAction   inputAction;
    private Track               overlappedTrack;

    private bool                isRepairing = false;

    public static System.Action<ItemData> OnRepairAction;

    private void Awake()
    {
        inputAction = GameManager.Manager.InputActions;
        inputAction.PlayerControls.Repair.performed += StartRepair;
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.PlayerControls.Repair.performed -= StartRepair;

        inputAction.Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Track")
        {
            Track track = collision.gameObject.GetComponent<Track>();

            overlappedTrack = track;
        }
    }

    public void SetOverlappingTrack(Track track)
    {
        overlappedTrack = track;
    }

    #region Repair Logic
    private void StartRepair(InputAction.CallbackContext obj)
    {
        if (!obj.performed || playerMove == null || isRepairing) return;

        isRepairing = true;

        playerMove.StopMove(repairDuration);
        StartCoroutine(RepairActionCR());
    }

    private IEnumerator RepairActionCR()
    {
        if (playerAnim == null) yield break;

        playerAnim.SetAnimBool("isRepair", true, repairDuration);

        yield return new WaitForSeconds(repairDuration);

        isRepairing = false;
    }

    //Called via Animation Event in player_repair
    public void RepairCallback()
    {
        if (playerItem != null && OnRepairAction != null)
            OnRepairAction(playerItem.CurrItem);
    }
    #endregion
}
