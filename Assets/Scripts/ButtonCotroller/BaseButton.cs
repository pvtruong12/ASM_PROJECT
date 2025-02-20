using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    private Button button;
    void Start()
    {
        if (button == null)
            button = GetComponent<Button>();
        AddOnClickEvent();

    }
    protected virtual void AddOnClickEvent()
    {
        button.onClick.RemoveAllListeners();
        this.button.onClick.AddListener(this.OnClick);
    }

    protected abstract void OnClick();

}
