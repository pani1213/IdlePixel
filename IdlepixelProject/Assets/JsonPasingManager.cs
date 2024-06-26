using System;
using System.Collections.Generic;
using UnityEngine;

public class JsonPasingManager : Singleton<JsonPasingManager>
{
    public TextAsset StageDataTextAsset, MonsterDataTextAsset, LevelupDataTextAsset, InventoryItemDataTextAsset, LevelDataTextAsset, UnlockDataTextAsset,
        GachaItemDataTextAsset,GachaLevelDataTextAsset, GachaLevelTableDataTextAsset;
    StageDatas stageDatas;
    MonsterDatas monsterDatas;
    LevelupUIDatas LevelupUIDatas;
    InventoryItemDatas inventoryItemDatas;
    LevelDatas levelDatas;
    UnLockLevelDatas unLockLevelDatas;
    GachaItemDatas gachaItemDatas;
    GachaLevelDatas gachaLevelDatas;
    GachaItemLevelTables gachaLevelTableDatas;
    public void init()
    {
     
        stageDatas = JsonUtility.FromJson<StageDatas>(StageDataTextAsset.text);
        foreach (StageData it in stageDatas.StageData)
            DataContainer.instance.stageDataContainer.Add(it.id, it);
        monsterDatas = JsonUtility.FromJson<MonsterDatas>(MonsterDataTextAsset.text);
        foreach (MonsterData it in monsterDatas.MonsterData)
            DataContainer.instance.monsterDataContainer.Add(it.id, it);
        LevelupUIDatas = JsonUtility.FromJson<LevelupUIDatas>(LevelupDataTextAsset.text);
        foreach (LevelupUIData it in LevelupUIDatas.LevelupUIData)
            DataContainer.instance.levelupUIDataContainer.Add(it.id, it);
        inventoryItemDatas = JsonUtility.FromJson<InventoryItemDatas>(InventoryItemDataTextAsset.text);
        foreach (InventoryItemData it in inventoryItemDatas.InventoryItemData)
            DataContainer.instance.inventoryDataContainer.Add(it.id, it);
        levelDatas = JsonUtility.FromJson<LevelDatas>(LevelDataTextAsset.text);
        foreach (LevelData it in levelDatas.LevelData)
            DataContainer.instance.levelDataContainer.Add(it.id, it);
        unLockLevelDatas = JsonUtility.FromJson<UnLockLevelDatas>(UnlockDataTextAsset.text);
        foreach (UnLockLevelData it in unLockLevelDatas.UnLockLevelData)
            DataContainer.instance.unlockLevelContainer.Add(it.id, it);
        gachaItemDatas = JsonUtility.FromJson<GachaItemDatas>(GachaItemDataTextAsset.text);
        foreach (GachaItemData it in gachaItemDatas.GachaItemData)
        {
            DataContainer.instance.allGachaData.Add(it.id, it);
          
            switch (it.ItemType)
            {
                case ItemType.Weapon: case ItemType.Armor:
                    DataContainer.instance.EquipDatas.Add(it.id, it);
                    break;
                case ItemType.Skill:
                    DataContainer.instance.skillDatas.Add(it.id, it);
                    break;
                case ItemType.Pet:
                    DataContainer.instance.petDatas.Add(it.id, it);
                    break;
            }
        }
        gachaLevelDatas = JsonUtility.FromJson<GachaLevelDatas>(GachaLevelDataTextAsset.text);
        foreach (GachaLevelData it in gachaLevelDatas.GachaLevelData)
            DataContainer.instance.gachaLevelDatas.Add(it.id, it);

        gachaLevelTableDatas = JsonUtility.FromJson<GachaItemLevelTables>(GachaLevelTableDataTextAsset.text);
        foreach (GachaItemLevelTable it in gachaLevelTableDatas.GachaItemLevelTable)
            DataContainer.instance.gachaLevelTableDatas.Add(it.id, it);
    }
}
[Serializable]
public class StageDatas
{
    public List<StageData> StageData;
}

[Serializable]
public class StageData
{
    public int id;
    public int monsterId;
    public int bossId;
    public int[] monsterSpawnCount;
    public string backgroundSpriteName;
    public string soundName;
}
[Serializable]
public class MonsterDatas
{
    public List<MonsterData> MonsterData;
}
[Serializable]
public class MonsterData
{
    public int id;
    public float hp;
    public float atk;
    public int rewardId;
    public int rewardCount;
    public float speed;
    public string monsterPrefabName;
}
[Serializable]
public class LevelupUIDatas
{
    public List<LevelupUIData> LevelupUIData;
}
[Serializable]
public class LevelupUIData
{
    public int id;
    public string name;
    public string ImageFileName;
}
[Serializable]
public class InventoryItemDatas
{
    public List<InventoryItemData> InventoryItemData;
}
[Serializable]
public class InventoryItemData
{
    public int id;
    public string name;
    public Rarity rarity;
    public string descript;
    public int count;
}
[Serializable]
public class LevelDatas
{
    public List<LevelData> LevelData;
}
[Serializable]
public class LevelData
{
    public int id;
    public int startLevel;
    public int endLevel;
    public float atkRiseFactor;
    public float atkRisePrice;
    public float hpRiseFactor;
    public float hpRisePrice;
    public float hpRegenFactor;
    public float hpRegenPrice;
    public float atkSpeedFactor;
    public float atkSpeedPrice;
    public float criticalFactor;
    public float criticalPrice;
    public float criticalPowerFactor;
    public float criticalPowerPrice;
    public float doubleShotFactor;
    public float doubleShotPrice;
    public float tripleShotFactor;
    public float tripleShotPrice;
    public float highClassAtkFactor;
    public float highClassAtkPrice;
    public float nomalEnemyFactor;
    public float nomalEnemyPrice;
}
public enum unlockType { atkSpeed,doubleShot,tripleShot,highClassAtk }
[Serializable]
public class UnLockLevelDatas
{
    public List<UnLockLevelData> UnLockLevelData;
}
[Serializable]
public class UnLockLevelData
{
    public int id;
    public unlockType unlockType;
    public int unlockLevel;
    public string descript;
}
[Serializable]
public class GachaItemDatas
{
    public List<GachaItemData> GachaItemData;
}
[Serializable]
public class GachaItemData
{
    public int id;
    public string name;
    public Rarity rarity;
    public ItemType ItemType;
    public float possessionValue;
    public float equipValue;
    public float atkValue;
    public float atkSpeedValue;
    public string prefabName;
    public int coolTime;
    public string desctript;
}
[Serializable]
public class GachaLevelDatas
{
    public List<GachaLevelData> GachaLevelData;
}
[Serializable]
public class GachaLevelData
{
    public int id;
    public List<double> percent;
    public int levelupCount;
}
[Serializable]
public class GachaItemLevelTables
{
    public List<GachaItemLevelTable> GachaItemLevelTable;
}
[Serializable]
public class GachaItemLevelTable
{
    public int id;
    public int count;
}