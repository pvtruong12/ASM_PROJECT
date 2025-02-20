using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnChoiMoi : BaseButton
{
    public static LoginHandles instance;
    protected override void OnClick()
    {
        instance.LoadCreatChar();
    }
}
