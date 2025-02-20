using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOK : BaseButton
{
    protected override void OnClick()
    {
        BtnChoiMoi.instance.CheckName();
    }
}
