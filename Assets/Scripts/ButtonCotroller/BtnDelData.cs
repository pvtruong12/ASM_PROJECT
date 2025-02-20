using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnDelData : BaseButton
{
    protected override void OnClick()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.Quit();
    }
}
