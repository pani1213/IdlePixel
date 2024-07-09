using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GachaSlot : MonoBehaviour
{
    public Image EXP_Fil_ImageUI;
    public Text EXP_TextUI, LvTextUI,ItemTypeTextUI, BlockTextUI;
    public GameObject BlockObj;
   
    public ItemType ItemType;
    private int gachaLevel = 1;
    public void init()
    {
        switch (ItemType) 
        {
            case ItemType.Weapon:case ItemType.Armor:
                ItemTypeTextUI.text = "장비";
                gachaLevel = UserDataManager.instance.userdata.playerGachaData.weaponGachaLevel;
                break;
            case ItemType.Skill:
                ItemTypeTextUI.text = "스킬";
                gachaLevel = UserDataManager.instance.userdata.playerGachaData.SkillLevel;
                break;
            case ItemType.Pet:
                ItemTypeTextUI.text = "동료";
                gachaLevel = UserDataManager.instance.userdata.playerGachaData.PetLevel;
                break;
        }
        LvTextUI.text = $"Lv{gachaLevel}";
        SetLevel();
    }
   
    public void ButtonAction_11Gacha()
    {
        int[] indexs =new int[12];
        for (int i = 0; i <= 11; i++) indexs[i] = GachaManager.instance.ActionGacha(ItemType, gachaLevel);

        UIPopupManager.instance.PopupUI("Popup_GachaItemList").GetComponent<PopupGachaItemListUI>().init(indexs);
        SetLevel();
        UserDataManager.instance.userdata.PrintGachaInven();
    }
    public void ButtonAction_35Gacha()
    {
        int[] indexs = new int[36];
        for (int i = 0; i <= 35; i++) indexs[i] = GachaManager.instance.ActionGacha(ItemType, gachaLevel);
     
        UIPopupManager.instance.PopupUI("Popup_GachaItemList").GetComponent<PopupGachaItemListUI>().init(indexs);
        SetLevel();
        UserDataManager.instance.userdata.PrintGachaInven();
    }
    public void ButtonAction_ADGacha()
    {
        int[] indexs = new int[16];
        for (int i = 0; i <= 15; i++) indexs[i] = GachaManager.instance.ActionGacha(ItemType, gachaLevel);
        UIPopupManager.instance.PopupUI("Popup_GachaItemList").GetComponent<PopupGachaItemListUI>().init(indexs);
        SetLevel();
        UserDataManager.instance.userdata.PrintGachaInven();
    }
    private void SetLevel()
    {
        switch (ItemType)
        {
            case ItemType.Weapon:case ItemType.Armor:
                LvTextUI.text = $"Lv {UserDataManager.instance.userdata.playerGachaData.weaponGachaLevel}";
                gachaLevel = UserDataManager.instance.userdata.playerGachaData.weaponGachaLevel;
                EXP_TextUI.text = $"{UserDataManager.instance.userdata.playerGachaData.weaponGachaCount}/{DataContainer.instance.gachaLevelDatas[gachaLevel].levelupCount}" ;
                EXP_Fil_ImageUI.fillAmount = (float)UserDataManager.instance.userdata.playerGachaData.weaponGachaCount / (float)DataContainer.instance.gachaLevelDatas[gachaLevel].levelupCount;
                break;
            case ItemType.Skill:
                LvTextUI.text = $"Lv {UserDataManager.instance.userdata.playerGachaData.SkillLevel}";
                gachaLevel = UserDataManager.instance.userdata.playerGachaData.SkillLevel;
                EXP_TextUI.text = $"{UserDataManager.instance.userdata.playerGachaData.SkillGachaCount}/{DataContainer.instance.gachaLevelDatas[gachaLevel].levelupCount}" ;
                EXP_Fil_ImageUI.fillAmount = (float)UserDataManager.instance.userdata.playerGachaData.SkillGachaCount / (float)DataContainer.instance.gachaLevelDatas[gachaLevel].levelupCount;
                break;
            case ItemType.Pet:
                LvTextUI.text = $"Lv {UserDataManager.instance.userdata.playerGachaData.PetLevel}";
                gachaLevel = UserDataManager.instance.userdata.playerGachaData.PetLevel;
                EXP_TextUI.text = $"{UserDataManager.instance.userdata.playerGachaData.PetGachaCount}/{DataContainer.instance.gachaLevelDatas[gachaLevel].levelupCount}" ;
                EXP_Fil_ImageUI.fillAmount = (float)UserDataManager.instance.userdata.playerGachaData.PetGachaCount / (float)DataContainer.instance.gachaLevelDatas[gachaLevel].levelupCount;
                break;
        }
    }
}
