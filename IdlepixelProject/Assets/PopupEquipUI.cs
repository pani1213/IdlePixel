using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopupEquipUI : Popup
{
    public GameObject content;
    //slot
    public List<EquipSlot> equipSlots;
    public EquipSlot slotPrefab;

    //UI
    public Text TitleTextUI, NameTextUI,RarityTextUI, LevelTextUI, EXPTextUI, PossessValueTextUI, EquipValueTextUI;
    public Image WeaponProfileImageUI, ExpFillImageUI;

    private ItemType UIType;
    public Button AttachmentBtn;
    public int currentId;
    public bool isHave;
    public void init(Action _action ,ItemType _itemType)
    {
        action = _action;
  
        UIType = _itemType;
        instantiateSlots();
        Refresh();
        SetTopUI(UserDataManager.instance.userdata.PlayerEquipData.WeaponId);
    }
    public override void Close()
    {
        base.Close();
        action();
    }
    private void SetTopUI(int _id)
    { 
        if (_id == 0)
        {
            WeaponProfileImageUI.sprite = DataContainer.instance.gachaItemAtlas.GetSprite("empty");
            NameTextUI.text = "";
            RarityTextUI.text = "";
            LevelTextUI.text = "Lv1";
            EXPTextUI.text = "0/3";
            ExpFillImageUI.fillAmount = 0;
            PossessValueTextUI.text = "";
            EquipValueTextUI.text = "";
        }
        else
        {
            GachaItemData data = DataContainer.instance.allGachaData[_id];
            GachaItemInvenData invenData = ItemInfoManager.instance.FindGachaInvenItemSlot(data.id);
            if (invenData == null)
            {
                LevelTextUI.text = "Lv1";
                EXPTextUI.text = "0/3";
                ExpFillImageUI.fillAmount = 0;
            }
            else
            {
                LevelTextUI.text = $"Lv{invenData.level}";
                EXPTextUI.text = UserDataManager.instance.userdata.PrintExpFormet(invenData.id);
                ExpFillImageUI.fillAmount = (float)invenData.count / (float)DataContainer.instance.gachaLevelTableDatas[invenData.level].count;
            }
            WeaponProfileImageUI.sprite = DataContainer.instance.gachaItemAtlas.GetSprite(data.prefabName);
            NameTextUI.text = data.name;
            RarityTextUI.FormetRarityTypeColor(data.rarity.ToString(),data.rarity);
            EquipValueTextUI.text = ((int)data.equipValue).FormatNumber();
            PossessValueTextUI.text = ((int)data.possessionValue).FormatNumber();
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        TitleTextUI.text = UIType.ToString();
        //if (ItemType.Weapon == UIType) TitleTextUI.text = "무기";
        //if (ItemType.Armor == UIType) TitleTextUI.text = "방어구";
        RefreshSlots();
    }
    // 1.datacontainer 의 itemtype slot 생성
    public void instantiateSlots()
    {
        int count = DataContainer.instance.GetItemTypeCount(UIType);
        for (int i = 0; i < count; i++)
        {
            equipSlots.Add(Instantiate(slotPrefab, content.transform));
            equipSlots[i].gameObject.SetActive(true);
        }
    }
    public void RefreshSlots()
    {
        List<int> idList = UserDataManager.instance.userdata.GetHaveIdList(UIType);

        int slotIndex = 0;
        foreach (GachaItemData data in DataContainer.instance.EquipDatas.Values)
        {
            if (data.ItemType != UIType)
                continue;

            equipSlots[slotIndex].init(data.id, SelectAction);
            slotIndex++;
        }
    }
    public void SelectAction(int dataId)
    {
        bool ishave = UserDataManager.instance.userdata.GetHaveIdList(UIType).Contains(dataId);
        AttachmentBtn.enabled = ishave;
        Debug.Log(dataId);
        currentId = dataId;
        isHave = ishave;
        SetTopUI(dataId);
    }
    public void ButtonActionAttachment()
    {
        if (!isHave)
        { 
            Debug.Log("아이템을 가지고있지 않습니다");
        }
        else
        { 
            if(UIType == ItemType.Weapon)
            UserDataManager.instance.userdata.PlayerEquipData.WeaponId = currentId;
            if(UIType == ItemType.Armor)
            UserDataManager.instance.userdata.PlayerEquipData.ArmorId = currentId;
            Debug.Log($"아이템 장착:{currentId}");
        }
    }
}
