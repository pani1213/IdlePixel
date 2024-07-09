using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum UIState { None, Attached,Change,Clear };
public class PopupPetUI : PopupItemSawpUI
{
    public override void Close()
    {
        base.Close();
        action();
    }
    public override void init(Action _action)
    {
        base.init(_action);
        if (changedAction == null)
            changedAction += PetController.instance.PetSlotRefresh;
        Refresh();
    }
    public override void Refresh()
    {
        base.Refresh();
        instantiateSlots();

        ApplyChanged();
        RefreshSlot();
        RefreshImage();
        //TEST_REFRESH_PET_ID();
    }
}
