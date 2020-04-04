using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemController : MonoBehaviour
{
    [SerializeField] private GameObject toolboxPrefab;
    [SerializeField] private float      toolboxHeight;

    private PlayerInputAction inputAction;
    private bool hasToolbox;

    public static System.Action OnPickupToolbox;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
        inputAction.PlayerControls.Pickup.performed += InteractToolbox;
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void InteractToolbox(InputAction.CallbackContext obj)
    {
        if (!obj.performed) return;

        if (!hasToolbox)
        {
            if (OnPickupToolbox != null) OnPickupToolbox();
            hasToolbox = true;
        }
        else
        {
            Vector3 currPos = this.transform.position;
            Vector3 toolboxSpawnPos = new Vector3(currPos.x, toolboxHeight, 0);
            GameObject go = Instantiate(toolboxPrefab, toolboxSpawnPos, Quaternion.identity);
            hasToolbox = false;
        }
    }
}
