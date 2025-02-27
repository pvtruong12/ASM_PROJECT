using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnDelData : BaseButton
{
    protected override void OnClick()
    {
        SoundManages.instance.Play("click");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.Quit();
    }
}
