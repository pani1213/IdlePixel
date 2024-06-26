using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupItemCycleUI : Popup
{
    public Image profileImageUI,ExpImageUI;
    public Text NameTextUI, TypeTextUI, LevelTextUI, EXPTextUI, ProssasbleValueTextUI;
    public GachaItemData currentData = null;
    public GachaItemInvenData invenData = null;
    public int currentIndex;
    public List<GachaItemData> dataList = new List<GachaItemData>();
    public List<int> haveIdList;
    public bool ishave;
    public Action<UIState> ActionAttached;
    public ItemType UItype;

    public virtual void init(int id, Action<UIState> _action)
    {
        ActionAttached = _action;
        int index = 0;

        haveIdList = UserDataManager.instance.userdata.GetHaveIdList(UItype);
        foreach (GachaItemData data in GetItemData().Values)
        {
            dataList.Add(data);
            if (data.id == id)
            {
                currentIndex = index;
                currentData = data;
                invenData = ItemInfoManager.instance.FindGachaInvenItemSlot(currentData.id);
            }
            index++;
        }
        Refresh();
    }
    public override void Refresh()
    {
        base.Refresh();
        currentData = dataList[currentIndex];
        ishave = haveIdList.Contains(currentData.id);
        profileImageUI.sprite = DataContainer.instance.GetItemSprite(currentData.id);
        NameTextUI.text = currentData.name;

        TypeTextUI.FormetRarityTypeColor(currentData.rarity.ToString(), currentData.rarity);
        Debug.Log(invenData == null);

        if (ishave)// ������ ������������
        {
            LevelTextUI.text = $"Lv{invenData.level}";
            EXPTextUI.text = UserDataManager.instance.userdata.PrintExpFormet(invenData.id);
            ExpImageUI.fillAmount = (float)invenData.count / (float)DataContainer.instance.gachaLevelTableDatas[invenData.level].count;
            ProssasbleValueTextUI.text = $"���ݷ� +{(int)currentData.possessionValue.LevelFormet(invenData.level)}%";
        }
        else //������ ���������� ������
        {
            LevelTextUI.text = "Lv1";
            EXPTextUI.text = "0/3";
            ExpImageUI.fillAmount=0;
            ProssasbleValueTextUI.text = $"���ݷ� +{(int)currentData.possessionValue}%";
        }
    }
    private Dictionary<int, GachaItemData> GetItemData()
    {
        if (UItype == ItemType.Pet)
            return DataContainer.instance.petDatas;
        if (UItype == ItemType.Skill)
            return DataContainer.instance.skillDatas;
        return null;
    }
    public List<int> GetIdList()
    {
        if (UItype == ItemType.Pet)
            return UserDataManager.instance.userdata.PlayerEquipData.Petids;
        if (UItype == ItemType.Skill)
            return UserDataManager.instance.userdata.PlayerEquipData.Skillids;
        return null;
    }
    public void ButtonAction_Right()
    {
        currentIndex++;
        if (dataList.Count <= currentIndex)
            currentIndex = 0;
        Refresh();
    }
    public void ButtonAction_Left()
    {
        currentIndex--;
        if (0 > currentIndex)
            currentIndex = dataList.Count - 1;
        Refresh();
    }
    public void ButtonAction_Attachment()
    {
        if (ishave)
        {
            int emptySlot = ItemInfoManager.instance.InvenIsEmptySlotIndex(GetIdList());
            int index = ItemInfoManager.instance.GetAttached_ID_Index(currentData.id, GetIdList());
            //1. currentPetData �����ѰͿ� ������� Clear
            if (index != -1)
            {
                ActionAttached(UIState.Clear);
            }
            else
            {
                //3. ���������� -1 
                if (emptySlot == -1)
                {
                    ActionAttached(UIState.Change);
                }
                //2. ������� �־�
                else
                {
                    ActionAttached(UIState.Attached);
                }
            }
            Close();
        }
        else
        {
            Debug.Log("None");
            //isAttach(false);
        }
    }
    public void ButtonAction_Upgrade()
    {
        Debug.Log("��ȭ");
    }
}
