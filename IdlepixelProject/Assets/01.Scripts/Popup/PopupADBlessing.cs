using UnityEngine;
using UnityEngine.UI;

public class PopupADBlessing : Popup
{
    public Text TitleText;
    public void ButtonAaciton_GoldBlessing()
    {
        Debug.Log("gold");
        ADMobManager.instance.LoadRewardedAd();
    }
    public void ButtonAction_AttackBlessing()
    {
        Debug.Log("Attack");
    }
    public void ButtonAction_SkillBlessing()
    {
        Debug.Log("Skill");
    }
}
