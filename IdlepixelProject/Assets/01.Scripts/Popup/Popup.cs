using System;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public Action action;
    public PopupType PopupType;
    public virtual void init(Action _action) { action = _action; }
    public virtual void init() { }
    public virtual void Refresh() { }
    public virtual void Apply() { }
    public virtual void Close()
    {
        UIPopupManager.instance.PopupClose();
    }

}
