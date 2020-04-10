using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemController : MonoBehaviour
{
    [Header("Spawn Details")]
    [SerializeField] private GameObject     toolboxPrefab;
    [SerializeField] private float          toolboxHeight;

    [Header("Database")]
    [SerializeField] private ItemDatabase   toolDB;

    private PlayerInputAction.PlayerControlsActions     inputAction;

    private InputAction switchAction;
    private bool                                        hasToolbox;
    private bool                                        canDoToolActions;
    private int                                         currItemIndex       = 0;
    private ItemData                                    currItem;
    private List<ItemData>                              availableTools      = new List<ItemData>();
    
    public ItemData                         CurrItem        { get { return currItem; } }
    public static System.Action             OnPickupToolbox;
    public static System.Action<ItemData>   OnToolSwitched;

    private void Awake()
    {
        inputAction = GameManager.Manager.InputActions.PlayerControls;
        availableTools = toolDB.data;

        currItem = availableTools[0];

        inputAction.Pickup.performed += InteractToolbox;
        inputAction.Switch.performed += HandleToolSwitch;
        inputAction.Switch.canceled += HandleToolSwitch;
    }

    private void OnEnable()
    {
        inputAction.Enable();
        Toolbox.OnOverlap += SetInteractable;
    }

    private void OnDisable()
    {
        inputAction.Disable();
        Toolbox.OnOverlap -= SetInteractable;
    }

    private void Start()
    {
        if(OnToolSwitched != null) OnToolSwitched(currItem);
    }

    private void SetInteractable(bool isInteractable)
    {
        canDoToolActions = isInteractable;
    }

    private void HandleToolSwitch(InputAction.CallbackContext obj)
    {
        if (!obj.performed || !canDoToolActions) return;
        
        float toolSwitchState = obj.ReadValue<float>();
            
        if(toolSwitchState >= 1)
        {
            currItemIndex++;

            if (currItemIndex > availableTools.Count - 1)
                currItemIndex = 0;
        }
        else
        {
            currItemIndex--;

            if (currItemIndex < 0)
                currItemIndex = availableTools.Count - 1;
        }

        currItem = availableTools[currItemIndex];

        if (OnToolSwitched != null) OnToolSwitched(currItem);
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

            GameObject go = Instantiate(toolboxPrefab
                , toolboxSpawnPos
                , Quaternion.identity);

            hasToolbox = false;
        }
    }
}
