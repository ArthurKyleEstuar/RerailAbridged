using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunc : MonoBehaviour
{
    public void OpenSceneSingle(string name)
    {
        SceneManage.OpenSingleScene(name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }

    public void ClearPlayerBindings()
    {
        GameManager.Manager.ResetPlayerBindings();
    }

    public void SavePlayerBindings()
    {
        GameManager.Manager.SavePlayerBindings();
    }
}
