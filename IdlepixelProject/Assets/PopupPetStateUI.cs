using UnityEngine.UI;
public class PopupPetStateUI : PopupItemCycleUI
{ 
    public Text AttackValueTextUI, AttackSpeedValueTextUI;

    public override void Refresh()
    {
        base.Refresh();

        if (ishave)
        {
            AttackValueTextUI.text = ((int)currentData.atkValue.LevelFormet(invenData.level)).FormatNumber();
            AttackSpeedValueTextUI.text = currentData.atkSpeedValue.LevelFormet(invenData.level).ToString();
        }
        else
        {
            AttackValueTextUI.text = ((int)currentData.atkValue).FormatNumber();
            AttackSpeedValueTextUI.text = currentData.atkSpeedValue.ToString();
        }
        // 보유한 상태에서 스텟이 달라져야함
   
    }

}
