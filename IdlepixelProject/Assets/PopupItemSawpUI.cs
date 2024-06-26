using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum UItype {None, Pet,Skill };

public class PopupItemSawpUI : Popup
{
    public GameObject content;
    //Slot
    public List<GachaItemSlot> Slots;
    public GachaItemSlot slotPrefab;
    public GachaItemSlot selectSlot;
    //UI
    public GameObject[] selectIconObj;
    public Image[] SlotImageUIs;

    public UIState UIState = UIState.None;
    public UItype UItype = UItype.None;
    public GameObject selectViewObject;
    int currentId = 0;

    public Action changedAction = null;
    private List<int> GetEquipIds()
    {
        switch (UItype)
        { 
            case UItype.Pet:
                return UserDataManager.instance.userdata.PlayerEquipData.Petids;
            case UItype.Skill:
                return UserDataManager.instance.userdata.PlayerEquipData.Skillids;    
        }
        return null;
    }
    private Dictionary<int, GachaItemData> GetDatas()
    {
        switch (UItype)
        {
            case UItype.Pet:
                return DataContainer.instance.petDatas;
            case UItype.Skill:
                return DataContainer.instance.skillDatas;
        }
        return null;
    }
    private string GetUITypePopupName()
    {
        switch (UItype)
        {
            case UItype.Pet:
                return "Popup_PetStateUI";
            case UItype.Skill:
                return "Popup_SkillStateUI";
        }
        return null;
    }
    public virtual void RefreshImage()
    {
        for (int i = 0; i < SlotImageUIs.Length; i++)
        {
            if (GetEquipIds()[i] != 0)
                SlotImageUIs[i].sprite = DataContainer.instance.GetItemSprite(GetEquipIds()[i]);
            else
                SlotImageUIs[i].sprite = DataContainer.instance.gachaItemAtlas.GetSprite("Pictoicon_Add");
        }
    }
    // slot
    public virtual void instantiateSlots()
    {
        for (int i = 0; i < GetDatas().Count; i++)
        {
            Slots.Add(Instantiate(slotPrefab, content.transform));
        }
    }
    public virtual void RefreshSlot()
    {
        int slotIndex = 0;
        foreach (var petSlot in GetDatas().Values)
        {
            Slots[slotIndex].gameObject.SetActive(true);
            Slots[slotIndex].init(petSlot.id, PopupState);
            slotIndex++;
        }
    }
    public virtual void BttonActionSelecet(int _buttonIndex)
    {
        if (UIState == UIState.None)
        {
            if (GetEquipIds()[_buttonIndex] != 0)
                PopupState(GetEquipIds()[_buttonIndex]);
        }
        if (UIState == UIState.Change)
        {
            //GetEquipIds()[_buttonIndex] = currentId;
            SetItem(_buttonIndex, currentId);
            Debug.Log($"set petList[{_buttonIndex}] <- {currentId}");
            RefreshImage();
            //IdsTextUIs[_buttonIndex].text = currentId.ToString();
            //TEST_REFRESH_PET_ID();
            ApplyChanged();
        }
    }

    public virtual void PopupState(int id)
    {
        // slot click시 이벤트 발동
        Debug.Log("click");
        // pet와 skill 분류@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        UIPopupManager.instance.PopupUI(GetUITypePopupName()).GetComponent<PopupItemCycleUI>().init(id, ActionAttached);
        currentId = id;
    }
    public virtual void ActionAttached(UIState _isAttached)
    {
        switch (_isAttached)
        {
            case UIState.None:
                break;
            case UIState.Attached:
                Debug.Log(GetEquipIds().Count);
                for (int i = 0; i < GetEquipIds().Count; i++)
                {
                    Debug.Log(GetEquipIds()[i]);
                    if (GetEquipIds()[i] == 0)
                    {
                        //GetEquipIds()[i] = currentId;
                        SetItem(i,currentId);
                        Debug.Log($"IdList {i} <= {currentId}");
                        break;
                    }
                }
                break;
            case UIState.Change:
                UIState = UIState.Change;
                selectSlot.init(currentId, null);
                selectViewObject.SetActive(true);
                OnOffSelectIcons(true);
                break;
            case UIState.Clear:
                // popup UserDataManager userdata index clear
                int index = ItemInfoManager.instance.GetAttached_ID_Index(currentId ,GetEquipIds());
                if (index == -1)
                {
                    Debug.Log($"Null {index}");
                }
                else
                {
                    //GetEquipIds()[index] = 0;
                    SetItem(index, 0);
                    Debug.Log($"player 의 {index} 번째 요소  = 0 ");
                }
                break;
        }
        RefreshImage();
        //TEST_REFRESH_PET_ID();
    }
    protected void SetItem(int _index,int _id)
    {
        GetEquipIds()[_index] = _id;
        if(changedAction != null)
            changedAction();
    }
    protected  void ApplyChanged()
    {
        UIState = UIState.None;
        selectViewObject.SetActive(false);
        OnOffSelectIcons(false);
    }
    protected void OnOffSelectIcons(bool _isOnOff)
    {
        for (int i = 0; i < selectIconObj.Length; i++)
            selectIconObj[i].gameObject.SetActive(_isOnOff);
    }
}
