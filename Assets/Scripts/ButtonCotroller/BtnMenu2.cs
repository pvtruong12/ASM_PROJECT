using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMenu2 : BaseButton
{
    protected override void OnClick()
    {
        SoundManages.instance.Play("click");
        LoginHandles.instance.LoadSceneMenuFromLoginScr();
    }
}
