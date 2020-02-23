using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : BaseManager<SceneManage>
{
   public static void OpenSingleScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
