using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField] private BGDatabase bgDatabase;

    private PlayerInputAction           inputActions;

    public PlayerInputAction InputActions { get { return inputActions; } }

    public static Action OnInputReset;

    private void Start()
    {
        inputActions = new PlayerInputAction();
        LoadPlayerBindings();
        //AudioManager.Manager.PlayAudio("BGM");
    }

    public Sprite GetRandomBG()
    {
        int randomIndex = Utilities.GetRandomIndex(bgDatabase.data.Count);

        return bgDatabase.data[randomIndex].BGImage;
    }

    #region Player Bindings
    public void SavePlayerBindings()
    {
        BindingWrapperClass bindingList = new BindingWrapperClass();

        foreach (var map in InputActions.asset.actionMaps)
        {
            foreach (var binding in map.bindings)
            {
                if (!string.IsNullOrEmpty(binding.overridePath))
                    bindingList.bindings.Add(new BindingSerializable(binding.id.ToString()
                        , binding.overridePath));
            }
        }

        PlayerPrefs.SetString("ControlOverrides", JsonUtility.ToJson(bindingList));
        PlayerPrefs.Save();
    }

    public void LoadPlayerBindings()
    {
        if (PlayerPrefs.HasKey("ControlOverrides"))
        {
            BindingWrapperClass bindingList = JsonUtility
                .FromJson(PlayerPrefs.GetString("ControlOverrides")
                , typeof(BindingWrapperClass)) as BindingWrapperClass;

            foreach (var map in InputActions.asset.actionMaps)
            {
                var bindings = map.bindings;

                for (var i = 0; i < bindings.Count; ++i)
                {
                    if (!bindingList.bindings
                        .Exists(obj => obj.id == bindings[i].id.ToString()))
                        continue;

                    BindingSerializable overrideBind = bindingList.bindings
                        .Find(obj => obj.id == bindings[i].id.ToString());

                    map.ApplyBindingOverride(i, new InputBinding { overridePath = overrideBind.path });
                }
            }
        }
    }

    public void ResetPlayerBindings()
    {
        foreach(var map in InputActions.asset.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }

        PlayerPrefs.DeleteKey("ControlOverrides");

        if (OnInputReset != null) OnInputReset();
    }
    #endregion
}
