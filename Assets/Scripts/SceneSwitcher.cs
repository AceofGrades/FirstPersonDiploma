using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public bool stirlingMode = false;

    public void GotoGameScene()
    {
        if (stirlingMode == false)
        {
            SceneManager.LoadScene(1);
        }
        if (stirlingMode == true)
        {
            SceneManager.LoadScene(2);
        }

    }

    public void GotoTitleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GotoStirlingScene()
    {
        SceneManager.LoadScene(2);
        stirlingMode = true;
    }
}
