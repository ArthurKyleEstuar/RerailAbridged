using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerMove     playerMove;
    [SerializeField] private PlayerAnim     playerAnim;

    [Header("Anim timer")]
    [SerializeField] private float          repairDuration = 0.3f;

    private PlayerInputAction   inputAction;
    private Track               overlappedTrack;

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

        if (overlappedTrack != null)
            overlappedTrack.RepairTrack();
    }
    #endregion
}
