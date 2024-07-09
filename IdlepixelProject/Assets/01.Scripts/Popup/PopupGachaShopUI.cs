using System;
using UnityEngine;

public class PopupGachaShopUI : Popup
{
    public GachaSlot[] GachaSlots;
    public override void init(Action _action)
    {
        base.init(_action);
        foreach (GachaSlot slot in GachaSlots) slot.init();
    }
    public override void Close()
    {
        action();
        base.Close();
    }
}


