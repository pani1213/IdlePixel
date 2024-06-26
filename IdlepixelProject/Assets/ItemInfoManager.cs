using System.Collections.Generic;
using UnityEngine;
public class ItemInfoManager : Singleton<ItemInfoManager>
{
    public int InvenIsEmptySlotIndex(List<int> ids)
    {
        for (int i = 0; i < ids.Count; i++)
        {
            if (ids[i] == 0)
                return i;
        }
        return -1;
    }
    /// <summary>
    /// 반환값 없을때 Return -1
    /// </summary>
    /// <param name="_Id"></param>
    /// <returns></returns>
    public int GetAttached_ID_Index(int _Id ,List<int> ids)
    {
        for (int i = 0; i < ids.Count; i++)
        {
            if (ids[i] == _Id)
            {
                return i;
            }
        }
        return -1;
    }
    public InventoryItemData FindInventoryItemSlot(int _id)
    {
        foreach (InventoryItemData item in UserDataManager.instance.userdata.userInvenData)
        {
            if (_id == item.id)
                return item;
        }
        Debug.Log($"해당 아이템을 찾을수 없음 id :{_id}");
        return null;
    }
    public GachaItemInvenData FindGachaInvenItemSlot(int _id)
    {
        for (int i = 0; i < UserDataManager.instance.userdata.userGachaItemInven.Count; i++)
        {
            if (UserDataManager.instance.userdata.userGachaItemInven[i].id == _id)
                return UserDataManager.instance.userdata.userGachaItemInven[i];
        }
        return null;
    }
    public void InsertGachaItem(int _id, int _count)
    {
        GachaItemInvenData slot = FindGachaInvenItemSlot(_id);
        if (slot == null) //아이템이 있을때
        {
           // Debug.Log($"NEW ID !: {_id}");
            UserDataManager.instance.userdata.userGachaItemInven.Add(new GachaItemInvenData() { id = _id ,count = 0, level = 1 });
            slot = FindGachaInvenItemSlot(_id);
        }
        slot.count += _count;

        if (slot.level >= DataContainer.instance.gachaLevelTableDatas.Count)
        {
            if (DataContainer.instance.gachaLevelTableDatas[DataContainer.instance.gachaLevelTableDatas.Count - 1].count <= slot.count)
            {
                slot.count = 0;
                slot.level++;
            }
           // Debug.Log($"id:{_id} Lv{slot.level}:{slot.count}/15");
        }
        else
        {
            for (int i = 0; i < DataContainer.instance.gachaLevelTableDatas.Count; i++)
            {
                if (DataContainer.instance.gachaLevelTableDatas[slot.level].count <= slot.count)
                {
                    slot.count = 0;
                    slot.level++;
                }
                else
                    break;
               // Debug.Log($"id:{_id} Lv{slot.level}:{slot.count}/{DataContainer.instance.gachaLevelTableDatas[slot.level].count}");
            }
        }
    }
    public void InsertItem(int _id,int _count)
    {
        InventoryItemData slot = FindInventoryItemSlot(_id);
        if (slot == null) //아이템이 있을때
        {
            UserDataManager.instance.userdata.userInvenData.Add(DataContainer.instance.inventoryDataContainer[_id]);
            slot = FindInventoryItemSlot(_id);
        }
        slot.count += _count;
    }
}