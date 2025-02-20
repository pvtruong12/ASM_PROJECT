using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnContinue : BaseButton
{
    protected override void OnClick()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetChar();
            GameManager.instance.panelWinOrLose.SetActive(false);
            Time.timeScale = 1;
            GameManager.instance.waypoins.LoadMapLevel(0, new System.Action(delegate() { }));
            return;
        }
        SceneManager.LoadScene(GameManager.listLevel[0]);
    }
}
