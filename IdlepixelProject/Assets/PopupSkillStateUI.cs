using UnityEngine;
using UnityEngine.UI;

public class PopupSkillStateUI : PopupItemCycleUI
{
    public Text skillDescriptText, skillCoolTimeText;
    public override void Refresh()
    {
        base.Refresh();
        if (ishave)
        {
        }
        else
        {
        }
        Debug.Log($"���� id:{currentData.id} ,���� index {currentIndex}");
        skillDescriptText.text = string.Format(currentData.desctript, currentData.atkValue, currentData.atkSpeedValue);
        skillCoolTimeText.text = $"{currentData.coolTime}��";
    }
}
