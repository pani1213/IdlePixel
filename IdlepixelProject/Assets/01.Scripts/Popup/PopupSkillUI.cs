using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSkillUI : PopupItemSawpUI
{
    public bool isFirst = false;
    public override void init()
    {
        Refresh();
        isFirst = true;
        if (changedAction == null)
            changedAction = SkillController.instance.Refresh;
    }
    public override void Refresh()
    {
        if (!isFirst)
            instantiateSlots();

        ApplyChanged();
        RefreshSlot();
        RefreshImage();
        //TEST_REFRESH_PET_ID();
    }
}
