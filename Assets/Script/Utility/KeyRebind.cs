using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public enum GameControls
{
    Repair      = 0,
    Interact    = 1,
    Switch      = 2,
}

[System.Serializable]
public class BindingWrapperClass
{
    public List<BindingSerializable> bindings = new List<BindingSerializable>();
}

[System.Serializable]
public struct BindingSerializable
{
    public string id;
    public string path;

    public BindingSerializable(string bindingId, string bindingPath)
    {
        id = bindingId;
        path = bindingPath;
    }
}

public class KeyRebind : MonoBehaviour
{
    [SerializeField] private GameControls       keyToChange;
    [SerializeField] private TextMeshProUGUI    keyDisplayText;
    [SerializeField] private bool               isComposite;
    [SerializeField] private int                compositeId;
    [SerializeField] private string             compositeName;

    private InputAction         selectedAction;
    private PlayerInputAction   playerAction;

    private void OnEnable()
    {
        GameManager.OnInputReset += UpdateText;
    }

    private void OnDisable()
    {
        if (GameManager.IsShuttingDown) return;

        GameManager.OnInputReset -= UpdateText;
    }

    private void Start()
    {
        playerAction = GameManager.Manager.InputActions;
        compositeId++;

        switch (keyToChange)
        {
            case GameControls.Repair:
                selectedAction = playerAction.PlayerControls.Repair;
                break;

            case GameControls.Interact:
                selectedAction = playerAction.PlayerControls.Pickup;
                break;

            case GameControls.Switch:
                selectedAction = playerAction.PlayerControls.Switch;
                break;
        }

        UpdateText();
    }

    public void ChangeKeyBind()
    {
        if (!isComposite)
        {
            var rebindOperation = selectedAction.PerformInteractiveRebinding()
                .Start()
                .OnComplete(result =>
                {
                    Debug.LogFormat("Rebinded {0} to {1}"
                    , keyToChange.ToString()
                    , selectedAction.GetBindingDisplayString());


                    GameManager.Manager.SavePlayerBindings();
                    UpdateText();
                    result.Dispose();
                });
        }
        else
        {
            var bindingToChange = selectedAction.bindings[compositeId];

            var rebindComposite = selectedAction.PerformInteractiveRebinding()
                .WithTargetBinding(compositeId)
                .Start()
                .OnComplete(result =>
                {
                    Debug.LogFormat("Rebinded {0} to {1}"
                        , bindingToChange.ToDisplayString()
                        , selectedAction.GetBindingDisplayString(bindingIndex: compositeId));

                    GameManager.Manager.SavePlayerBindings();
                    UpdateText();
                    result.Dispose();
                });
        }


        UpdateText("Press any key to rebind");
    }

    private void UpdateText(string displayText = "")
    {
        if (keyDisplayText == null) return;

        if (string.IsNullOrEmpty(displayText))
            keyDisplayText.text = GetKeyDisplayName();
        else
            keyDisplayText.text = displayText;
    }

    private void UpdateText()
    {
        if (keyDisplayText == null) return;

        keyDisplayText.text = GetKeyDisplayName();
    }

    private string GetKeyDisplayName()
    {
        if(isComposite)
            return keyToChange.ToString() + " " + compositeName + ": " + selectedAction.GetBindingDisplayString(bindingIndex: compositeId);
        else
            return keyToChange.ToString() + ": " + selectedAction.GetBindingDisplayString();
    }
}
