using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupPlayerUI : Popup
{
    public Image[] equipIcons;
    public Text[] equipLevels;
    public Text[] PlayerVelue;
    public void ButtonAction_WeaponPopup()
    {
        UIPopupManager.instance.PopupUI("Popup_EquipUI").GetComponent<PopupEquipUI>().init(Refresh, ItemType.Weapon);
    }
    public void ButtonAction_ArmorPopup()
    {
        UIPopupManager.instance.PopupUI("Popup_EquipUI").GetComponent<PopupEquipUI>().init(Refresh,ItemType.Armor);
    }
    public override void init(Action _action)
    {
        base.init(_action);
        Refresh();
    }
    public override void Refresh()
    {
        base.Refresh();
        RefeshPlayerState();
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

    public void RefeshPlayerState()
    {
        PlayerVelue[0].text = ((int)PlayerFindManager.instance.Player.ATK).FormatNumber();
        PlayerVelue[1].text = ((int)PlayerFindManager.instance.Player.maxHp).FormatNumber();
        PlayerVelue[2].text = PlayerFindManager.instance.Player.ATKSpeed.ToString();
        PlayerVelue[3].text = ((int)PlayerFindManager.instance.Player.hpRegen).FormatNumber();
        PlayerVelue[4].text = $"{PlayerFindManager.instance.Player.critical}%";
        PlayerVelue[5].text = $"{((int)PlayerFindManager.instance.Player.criticalPower).FormatNumber()}%";
        PlayerVelue[6].text = $"{PlayerFindManager.instance.Player.doubleShot}%";
        PlayerVelue[7].text = $"{PlayerFindManager.instance.Player.tripleShot}%";
        PlayerVelue[8].text = $"{((int)PlayerFindManager.instance.Player.normalAnemyATK).FormatNumber()}%";
    }
}

