using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnChoiMoi : BaseButton
{
    protected override void OnClick()
    {
        SoundManages.instance.Play("click");
        LoginHandles.instance.LoadSceneCreatCharFromLoginScr();
    }
}
