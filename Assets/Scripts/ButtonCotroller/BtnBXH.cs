using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BtnBXH : BaseButton
{
    protected override void OnClick()
    {
        GameManager.instance.listButtonWinLose.RemoveAt(0);
        GameManager.instance.Top3();

    }
}
