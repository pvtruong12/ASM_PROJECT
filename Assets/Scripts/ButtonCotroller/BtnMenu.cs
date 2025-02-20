using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnMenu : BaseButton
{
    protected override void OnClick()
    {
        ResetGame();
    }
    void ResetGame()
    {
        GameObject[] dontDestroyObjects = FindObjectsOfType<GameObject>();
        foreach (var obj in dontDestroyObjects)
        {
            if (obj.scene.name == "DontDestroyOnLoad")
            {
                Destroy(obj);
            }
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("LoginScr");
    }

}
