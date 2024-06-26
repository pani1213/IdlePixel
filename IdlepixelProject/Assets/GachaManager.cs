using System;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : Singleton<GachaManager>
{
    public int GetWeightedRandomIndex(params float[] pers)//확률을 차례대로(합이 1이되는것을 권장) 넣었을 때 해당하는 확률이 걸리면 01234가 차례로 나옴.
    {
        int maxLenth = 0;
        if (pers.Length == 1)//배열길이가 1이면 무조건 그 값이 나와야함으로 0을 리턴한다.
            return 0;
        //1.가장 소숫점이 긴 변수를 찾아낸다.
        int lenth;
        decimal total = 0;
        for (int i = 0; i < pers.Length; i++)
        {
            lenth = pers[i].ToString().Substring(pers[i].ToString().IndexOf('.') + 1).Length;
            total += (decimal)pers[i];
            if (maxLenth < lenth)
                maxLenth = lenth;
        }
        int correction = (int)(total * (decimal)Math.Pow(10, maxLenth)); //-> 곱해주는 수 
        int randomNumber = UnityEngine.Random.Range(1, correction + 1);
        int tempNum = 0;
        int num = 0;
        for (int i = 0; i < pers.Length; i++)
        {
            num = (int)(correction * (decimal)pers[i]);
            if (num + tempNum >= randomNumber)
            {
                //Debug.Log(num + tempNum + ">=" + randomNumber);
                return i;
            }
            tempNum += num;
        }
        return 0;
    }

    public int ActionGacha(ItemType _itemType, int _Level)
    {

        //Dictionary<int, GachaItemData> datas = new Dictionary<int, GachaItemData>();
        switch (_itemType) 
        {
            case ItemType.Weapon: case ItemType.Armor:
                UserDataManager.instance.userdata.playerGachaData.weaponGachaCount++;
                if (UserDataManager.instance.userdata.playerGachaData.weaponGachaCount > DataContainer.instance.gachaLevelDatas[_Level].levelupCount)
                { 
                    UserDataManager.instance.userdata.playerGachaData.weaponGachaCount = 0;
                    _Level++;
                    UserDataManager.instance.userdata.playerGachaData.weaponGachaLevel = _Level;
                    Debug.Log($"level up {UserDataManager.instance.userdata.playerGachaData.weaponGachaLevel}");
                }
                break;
            case ItemType.Skill:
                UserDataManager.instance.userdata.playerGachaData.SkillGachaCount++;
                if (UserDataManager.instance.userdata.playerGachaData.SkillGachaCount > DataContainer.instance.gachaLevelDatas[_Level].levelupCount)
                {
                    UserDataManager.instance.userdata.playerGachaData.SkillGachaCount = 0;
                    _Level++;
                    UserDataManager.instance.userdata.playerGachaData.SkillLevel = _Level;
                    Debug.Log($"level up {UserDataManager.instance.userdata.playerGachaData.SkillLevel}");
                }
                break;
            case ItemType.Pet:
                UserDataManager.instance.userdata.playerGachaData.PetGachaCount++;
                if (UserDataManager.instance.userdata.playerGachaData.PetGachaCount > DataContainer.instance.gachaLevelDatas[_Level].levelupCount)
                {
                    UserDataManager.instance.userdata.playerGachaData.PetGachaCount = 0;
                   _Level++;
                    UserDataManager.instance.userdata.playerGachaData.PetLevel = _Level;
                    Debug.Log($"level up {UserDataManager.instance.userdata.playerGachaData.PetLevel}");
                }
                break;
        }
        float[] percents = new float[7];
        for (int i = 0; i < DataContainer.instance.gachaLevelDatas[_Level].percent.Count; i++)
            percents[i] = (float)DataContainer.instance.gachaLevelDatas[_Level].percent[i]/10;

        int rarity = GetWeightedRandomIndex(percents);


        List<GachaItemData> gachaData = GetRarityDatas(_itemType, (Rarity)rarity);
        int randomIndex = UnityEngine.Random.Range(0, gachaData.Count);
   

        ItemInfoManager.instance.InsertGachaItem(gachaData[randomIndex].id, 1);
        return gachaData[randomIndex].id;
    }

    private  List<GachaItemData> GetRarityDatas(ItemType _itemType,Rarity _rarity)
    {
        switch (_itemType)
        {
            case ItemType.Armor:
            case ItemType.Weapon:
                switch (_rarity)
                {
                    case Rarity.Comon:
                        return DataContainer.instance.gachaItemDatas.comonEuiq;
                    case Rarity.Uncomon:
                        return DataContainer.instance.gachaItemDatas.uncomonEuiq;
                    case Rarity.Rare:
                        return DataContainer.instance.gachaItemDatas.rareEuiq;
                    case Rarity.Epic:
                        return DataContainer.instance.gachaItemDatas.epicEuiq;
                    case Rarity.Legendary:
                        return DataContainer.instance.gachaItemDatas.legendaryEuiq;
                    case Rarity.Mythical:
                        return DataContainer.instance.gachaItemDatas.mythicalEuiq;
                    case Rarity.Transcendental:
                        return DataContainer.instance.gachaItemDatas.transcendentalEuiq;
                    default:
                        return null;
                }
            case ItemType.Skill:
                switch (_rarity)
                {
                    case Rarity.Comon:
                        return DataContainer.instance.gachaItemDatas.comonSkill;
                    case Rarity.Uncomon:
                        return DataContainer.instance.gachaItemDatas.uncomonSkill;
                    case Rarity.Rare:
                        return DataContainer.instance.gachaItemDatas.rareSkill;
                    case Rarity.Epic:
                        return DataContainer.instance.gachaItemDatas.epicSkill;
                    case Rarity.Legendary:
                        return DataContainer.instance.gachaItemDatas.legendarySkill;
                    case Rarity.Mythical:
                        return DataContainer.instance.gachaItemDatas.mythicalSkill;
                    case Rarity.Transcendental:
                        return DataContainer.instance.gachaItemDatas.transcendentalSkill;
                    default:
                        return null;
                }
            case ItemType.Pet:
                switch (_rarity)
                {
                    case Rarity.Comon:
                        return DataContainer.instance.gachaItemDatas.comonPet;
                    case Rarity.Uncomon:
                        return DataContainer.instance.gachaItemDatas.uncomonPet;
                    case Rarity.Rare:
                        return DataContainer.instance.gachaItemDatas.rarePet;
                    case Rarity.Epic:
                        return DataContainer.instance.gachaItemDatas.epicPet;
                    case Rarity.Legendary:
                        return DataContainer.instance.gachaItemDatas.legendaryPet;
                    case Rarity.Mythical:
                        return DataContainer.instance.gachaItemDatas.mythicalPet;
                    case Rarity.Transcendental:
                        return DataContainer.instance.gachaItemDatas.transcendentalPet;
                    default:
                        return null;
                }
            default:
                return null;
        }
    }
   

}