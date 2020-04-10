using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class KeyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI    keyRebindText;
    [SerializeField] private TextMeshProUGUI    keyActionText;
    
    [SerializeField] private GameControls       currControl;
    [SerializeField] private bool               isComposite;
    [SerializeField] private int                compositeId;

    private void Start()
    {
        PlayerInputAction actions = GameManager.Manager.InputActions;
        InputAction inputAction = null;

        if (keyActionText != null)
            keyActionText.text = currControl.ToString();

        switch (currControl)
        {
            case GameControls.Interact:
                inputAction = actions.PlayerControls.Pickup;
                break;

            case GameControls.Repair:
                inputAction = actions.PlayerControls.Repair;
                break;

            case GameControls.Switch:
                inputAction = actions.PlayerControls.Switch;
                break;
        }

        if (keyRebindText == null) return;

        if(isComposite)
            keyRebindText.text = inputAction.GetBindingDisplayString(bindingIndex: compositeId + 1);
        else
            keyRebindText.text = inputAction.GetBindingDisplayString();
    }

}
