using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnContinue : BaseButton
{
    protected override void OnClick()
    {
        SoundManages.instance.Play("click");
        LoginHandles.instance.LoadSceneLoginCharFromLoginScr();
    }
}
