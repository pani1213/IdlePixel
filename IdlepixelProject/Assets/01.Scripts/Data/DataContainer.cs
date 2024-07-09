using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class DataContainer : Singleton<DataContainer>
{
    public Dictionary<int, StageData> stageDataContainer;
    public Dictionary<int, MonsterData> monsterDataContainer;
    public Dictionary<int, InventoryItemData> inventoryDataContainer;
    public Dictionary<int, LevelData> levelDataContainer;
    //UI
    public Dictionary<int, LevelupUIData> levelupUIDataContainer;
    public Dictionary<int, UnLockLevelData> unlockLevelContainer;
    //GachaData
    public Dictionary<int, GachaItemData> EquipDatas;
    public Dictionary<int, GachaItemData> skillDatas;
    public Dictionary<int, GachaItemData> petDatas;
    public Dictionary<int, GachaLevelData> gachaLevelDatas;
    public Dictionary<int, GachaItemLevelTable> gachaLevelTableDatas;
    public Dictionary<int, GachaItemData> allGachaData;

    public GachaItemRarityDatas gachaItemDatas;
    private bool isGachainit = false;

    public SpriteAtlas gachaItemAtlas;
    public void init()
    {
        stageDataContainer = new Dictionary<int, StageData>();
        monsterDataContainer = new Dictionary<int, MonsterData>();
        levelupUIDataContainer = new Dictionary<int, LevelupUIData>();
        inventoryDataContainer = new Dictionary<int, InventoryItemData>();
        levelDataContainer = new Dictionary<int, LevelData>();
        unlockLevelContainer = new Dictionary<int, UnLockLevelData>();

        EquipDatas = new Dictionary<int, GachaItemData>();
        skillDatas = new Dictionary<int, GachaItemData>();
        petDatas = new Dictionary<int, GachaItemData>();

        gachaLevelDatas = new Dictionary<int, GachaLevelData>();
        gachaLevelTableDatas = new Dictionary<int, GachaItemLevelTable>();
        allGachaData= new Dictionary<int, GachaItemData>();

    }
    public int GetItemTypeCount(ItemType _itemType)
    {
        int count = 0;
        foreach (GachaItemData data in EquipDatas.Values) if (data.ItemType == _itemType) count++;
        return count;
    }
  
    public Sprite GetItemSprite(int _id)
    {
      
            return gachaItemAtlas.GetSprite(allGachaData[_id].prefabName);
    }
    public GachaItemRarityDatas GetGachaItemData()
    {
        if (isGachainit)
            return null;
        gachaItemDatas = new GachaItemRarityDatas();

        gachaItemDatas.comonEuiq = new List<GachaItemData>();
        gachaItemDatas.uncomonEuiq = new List<GachaItemData>();
        gachaItemDatas.rareEuiq = new List<GachaItemData>();
        gachaItemDatas.epicEuiq = new List<GachaItemData>();
        gachaItemDatas.legendaryEuiq = new List<GachaItemData>();
        gachaItemDatas.mythicalEuiq = new List<GachaItemData>();
        gachaItemDatas.transcendentalEuiq = new List<GachaItemData>();

        gachaItemDatas.comonSkill = new List<GachaItemData>();
        gachaItemDatas.uncomonSkill = new List<GachaItemData>();
        gachaItemDatas.rareSkill = new List<GachaItemData>();
        gachaItemDatas.epicSkill = new List<GachaItemData>();
        gachaItemDatas.legendarySkill = new List<GachaItemData>();
        gachaItemDatas.mythicalSkill = new List<GachaItemData>();
        gachaItemDatas.transcendentalSkill = new List<GachaItemData>();

        gachaItemDatas.comonPet = new List<GachaItemData>();
        gachaItemDatas.uncomonPet = new List<GachaItemData>();
        gachaItemDatas.rarePet = new List<GachaItemData>();
        gachaItemDatas.epicPet = new List<GachaItemData>();
        gachaItemDatas.legendaryPet = new List<GachaItemData>();
        gachaItemDatas.mythicalPet = new List<GachaItemData>();
        gachaItemDatas.transcendentalPet = new List<GachaItemData>();

        foreach (GachaItemData data in EquipDatas.Values)
        {
            switch (data.rarity)
            {
                case Rarity.Comon:
                    gachaItemDatas.comonEuiq.Add(data);
                    break;
                case Rarity.Uncomon:
                    gachaItemDatas.uncomonEuiq.Add(data );
                    break;
                case Rarity.Rare:
                    gachaItemDatas.rareEuiq.Add(data    );
                    break;
                case Rarity.Epic:
                    gachaItemDatas.epicEuiq.Add(data    );
                    break;
                case Rarity.Legendary:
                    gachaItemDatas.legendaryEuiq.Add(data   );
                    break;
                case Rarity.Mythical:
                    gachaItemDatas.mythicalEuiq.Add(data);
                    break;
                case Rarity.Transcendental:
                    gachaItemDatas.transcendentalEuiq.Add(data);
                    break;
            }
        }
        foreach (GachaItemData data in skillDatas.Values)
        {
            switch (data.rarity)
            {
                case Rarity.Comon:
                    gachaItemDatas.comonSkill.Add(data);
                    break;
                case Rarity.Uncomon:
                    gachaItemDatas.uncomonSkill.Add(data);
                    break;
                case Rarity.Rare:
                    gachaItemDatas.rareSkill.Add(data);
                    break;
                case Rarity.Epic:
                    gachaItemDatas.epicSkill.Add(data);
                    break;
                case Rarity.Legendary:
                    gachaItemDatas.legendarySkill.Add(data);
                    break;
                case Rarity.Mythical:
                    gachaItemDatas.mythicalSkill.Add(data);
                    break;
                case Rarity.Transcendental:
                    gachaItemDatas.transcendentalSkill.Add(data);
                    break;
            }
        }
        foreach (GachaItemData data in petDatas.Values)
        {
            switch (data.rarity)
            {
                case Rarity.Comon:
                    gachaItemDatas.comonPet.Add(data);
                    break;
                case Rarity.Uncomon:
                    gachaItemDatas.uncomonPet.Add(data);
                    break;
                case Rarity.Rare:
                    gachaItemDatas.rarePet.Add(data);
                    break;
                case Rarity.Epic:
                    gachaItemDatas.epicPet.Add(data );
                    break;
                case Rarity.Legendary:
                    gachaItemDatas.legendaryPet.Add(data);
                    break;
                case Rarity.Mythical:
                    gachaItemDatas.mythicalPet.Add(data);
                    break;
                case Rarity.Transcendental:
                    gachaItemDatas.transcendentalPet.Add(data);
                    break;
            }
        }
        isGachainit = true;
        return gachaItemDatas;
    }
}

public class GachaItemRarityDatas
{
    public List<GachaItemData> comonEuiq;
    public List<GachaItemData> uncomonEuiq;
    public List<GachaItemData> rareEuiq;
    public List<GachaItemData> epicEuiq;
    public List<GachaItemData> legendaryEuiq;
    public List<GachaItemData> mythicalEuiq;
    public List<GachaItemData> transcendentalEuiq;

    public List<GachaItemData> comonSkill;
    public List<GachaItemData> uncomonSkill;
    public List<GachaItemData> rareSkill;
    public List<GachaItemData> epicSkill;
    public List<GachaItemData> legendarySkill;
    public List<GachaItemData> mythicalSkill;
    public List<GachaItemData> transcendentalSkill;
            
    public List<GachaItemData> comonPet;
    public List<GachaItemData> uncomonPet;
    public List<GachaItemData> rarePet;
    public List<GachaItemData> epicPet;
    public List<GachaItemData> legendaryPet;
    public List<GachaItemData> mythicalPet;
    public List<GachaItemData> transcendentalPet;
}


