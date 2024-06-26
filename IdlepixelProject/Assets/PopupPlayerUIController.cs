using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PopupPlayerUIController : Popup
{
    public enum PlayerTapType { None, Player, Skill, Relics, Mastery, Characterristic }
    public PlayerTapType popupState = PlayerTapType.None;
    public GameObject[] plyerUIs;
    public GameObject CurrentPlayerUI;
    public void ButtonAction_Player() => SetActiveAction(PlayerTapType.Player);
    public void ButtonAction_Skill() => SetActiveAction(PlayerTapType.Skill);
    public void ButtonAction_Relics() { }
    public void ButtonAction_Mastery() { }
    public void ButtonAction_Characterristic() => SetActiveAction(PlayerTapType.Characterristic);
    private void SetActiveAction(PlayerTapType _popupState)
    {
        if (popupState == _popupState) return;
        if (popupState != PlayerTapType.None) CurrentPlayerUI.gameObject.SetActive(false);
        popupState = _popupState;
        CurrentPlayerUI = plyerUIs[((int)popupState) - 1];
        CurrentPlayerUI.SetActive(true);
        CurrentPlayerUI.GetComponent<Popup>().init();
    }
    public override void Close()
    {
        base.Close();
    }


}
