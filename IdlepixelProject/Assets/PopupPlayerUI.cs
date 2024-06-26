using UnityEngine;
using UnityEngine.UI;

public class PopupPlayerUI : Popup
{
    public Image[] equipIcons;
    public Text[] equipLevels;
    public void ButtonAction_WeaponPopup()
    {
        UIPopupManager.instance.PopupUI("Popup_EquipUI").GetComponent<PopupEquipUI>().init(Refresh, ItemType.Weapon);
    }
    public void ButtonAction_ArmorPopup()
    {
        UIPopupManager.instance.PopupUI("Popup_EquipUI").GetComponent<PopupEquipUI>().init(Refresh,ItemType.Armor);
    }
    public override void init()
    {
        Refresh();
    }
    public override void Refresh()
    {
        base.Refresh();
        if (UserDataManager.instance.userdata.PlayerEquipData.WeaponId != 0)
        {
            equipIcons[0].sprite = DataContainer.instance.GetItemSprite(UserDataManager.instance.userdata.PlayerEquipData.WeaponId);
            equipLevels[0].text = $"LV{ItemInfoManager.instance.FindGachaInvenItemSlot(UserDataManager.instance.userdata.PlayerEquipData.WeaponId).level}";
        }
        else
        {
            equipIcons[0].sprite = DataContainer.instance.gachaItemAtlas.GetSprite("Pictoicon_Add");
            equipLevels[0].text = "";
        }
        if (UserDataManager.instance.userdata.PlayerEquipData.ArmorId != 0)
        {
            equipIcons[1].sprite = DataContainer.instance.GetItemSprite(UserDataManager.instance.userdata.PlayerEquipData.ArmorId);
            equipLevels[1].text = $"LV{ItemInfoManager.instance.FindGachaInvenItemSlot(UserDataManager.instance.userdata.PlayerEquipData.ArmorId).level}";
        }
        else
        {
            equipIcons[1].sprite = DataContainer.instance.gachaItemAtlas.GetSprite("Pictoicon_Add");
            equipLevels[1].text = "";
        }
    }
 
}

