using UnityEngine;

public class MainBottombuttonsUI : MonoBehaviour
{
    public enum PopupState {None ,Player, Pet, Dungeon, Lab, Shop };
    public PopupState popupState = PopupState.None;
    Popup CurrentPopup = null;
    
    public void ButtonAction_Player() => PopupAction("Popup_PlayerUI", PopupState.Player); 
    public void ButtonAction_Pet() => PopupAction("Popup_PetUI", PopupState.Pet);
    public void ButtonAction_Dungeon() => PopupAction("Popup_DungeonUI", PopupState.Dungeon);
    public void ButtonAction_Lab() { }
    public void ButtonAction_Shop() => PopupAction("Popup_GachaShop", PopupState.Shop);
    public void SetPopupState()
    {
        popupState = PopupState.None;
    }
    private void PopupAction(string _popupName,PopupState _popupState)
    {
        if (popupState == _popupState)
        {
            CurrentPopup.Close();
            return;
        }
        if (popupState != PopupState.None) CurrentPopup.Close();

        CurrentPopup = UIPopupManager.instance.PopupUI(_popupName);
        CurrentPopup.init(SetPopupState);
        popupState = _popupState;

    }
}
