using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class UserData
{
    public string userId;
    public EquipData PlayerEquipData = new EquipData();
    public List<InventoryItemData> userInvenData;
    public List<GachaItemInvenData> userGachaItemInven;

    public StatLevel playerLevelData;
    public StageInfo playerStageInfo;
    public GachaLevel playerGachaData;

    public string LoginTime;
    public string LogoutTime;
    public bool isSkillAuto = false;
    public void PrintGachaInven()
    {
        string temp = "";
        for (int i = 0; i < userGachaItemInven.Count; i++)
            temp += $"ID:{userGachaItemInven[i].id} LV{userGachaItemInven[i].level} Count{userGachaItemInven[i].count} \n";
       Debug.Log(temp);
    }
    public List<int> GetHaveIdList(ItemType _itemType)
    {
        List<int> indexs = new List<int>(); 
        for (int i = 0; i < userGachaItemInven.Count; i++)
        {
            if (DataContainer.instance.allGachaData[userGachaItemInven[i].id].ItemType == _itemType)
            {
                indexs.Add(userGachaItemInven[i].id);
            }
        }
        return indexs;
    }
    public string PrintExpFormet(int id)
    {
        int Index = 0 ;
        for (int i = 0; i < userGachaItemInven.Count; i++)
        {
            if (userGachaItemInven[i].id == id)
                Index = i;
        }
        if (DataContainer.instance.gachaLevelTableDatas.Count <= userGachaItemInven[Index].level)
            return $"{userGachaItemInven[Index].level}/{DataContainer.instance.gachaLevelTableDatas.Count}";
        else
            return $"{userGachaItemInven[Index].level}/{DataContainer.instance.gachaLevelTableDatas[userGachaItemInven[Index].level].count}";
    }
}

[Serializable]
public class EquipData
{
    public int WeaponId;
    public int ArmorId;
    public int invenIndex;
    public int[] ints;
    public List<int> Petids = new List<int>();
    public List<int> Skillids = new List<int>();
}
[Serializable]
public class GachaItemInvenData
{
    public int id;
    public GachaItemData GachaItemData;
    public int count;
    public int level;
}
[Serializable]
public class StageInfo
{
    public int Chapter;
    public int Stage;
    public int Round;
    public bool isClearBoss;
}
[Serializable]
public class StatLevel
{
    public int AttackLevel;
    public int HpLevel;
    public int HpRegenLevel;
    public int AttackSpeedLevel;
    public int CriticalPercentLevel;
    public int CriticalPowerLevel;
    public int DoubleShotPercentLevel;
    public int TripleShotPercentLevel;
    public int HighClassAttackPowerLevel;
    public int NomalEnemyAttackPowerLevel;
}
[Serializable]
public class GachaLevel
{
    public int weaponGachaLevel;
    public int weaponGachaCount;
    public int SkillLevel;
    public int SkillGachaCount;
    public int PetLevel;
    public int PetGachaCount;
}