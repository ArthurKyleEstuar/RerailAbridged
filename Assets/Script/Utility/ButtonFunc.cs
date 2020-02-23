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
        Application.Quit();
    }
}
