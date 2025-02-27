using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : BaseButton
{
    protected override void OnClick()
    { 
            SoundManages.instance.Play("click");
        Application.Quit();
    }
}
