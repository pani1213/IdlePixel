using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : Singleton<SkillController>
{
    public bool isAuto = false;
    public Text AutoText;
    public SKill[] SkillSlots;
    public void init()
    {
        isAuto = UserDataManager.instance.userdata.isSkillAuto;
        Debug.Log($"이전 Auto 상태{isAuto}");
        Refresh();
    }
    public void Refresh()
    {
        for (int i = 0; i < SkillSlots.Length; i++) SkillSlots[i].init(UserDataManager.instance.userdata.PlayerEquipData.Skillids[i]);
        SetAutoText();
    }
    public void Buttonaction_SpellSkill(int _btnIndex)
    {
        Debug.Log(UserDataManager.instance.userdata.PlayerEquipData.Skillids[_btnIndex]);
    }
    public void Buttonaction_Auto()
    {
        isAuto = !isAuto;
        Debug.Log($"스킬 Auto 상태:{isAuto}");
        SetAutoText();
    }
    public void SetAutoText()
    {
        if (isAuto)
            AutoText.text = "AUTO\nON";
        else
            AutoText.text = "AUTO\nOFF";

    }
}
