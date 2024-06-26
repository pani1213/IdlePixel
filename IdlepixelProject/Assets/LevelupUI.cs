using System;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
public class LevelupUI : MonoBehaviour
{
    public SpriteAtlas LevelupUIAtlas;
    public Text[] StatNameTexts;
    public Text[] StatTexts;
    public Text[] PriceTexts;
    public Image[] StatImages;
    public BlockUI[] blockObj;
    public Action buttonAction;
  
    public void init()
    {
        for (int i = 0; i < DataContainer.instance.levelupUIDataContainer.Count; i++)
        {
            StatNameTexts[i].text = DataContainer.instance.levelupUIDataContainer[1001 + i].name;
            StatImages[i].sprite = LevelupUIAtlas.GetSprite(DataContainer.instance.levelupUIDataContainer[1001 + i].ImageFileName);
        }
        StatTexts[0].text = ((int)PlayerFindManager.instance.Player.ATK).FormatNumber();
        StatTexts[1].text = ((int)PlayerFindManager.instance.Player.maxHp).FormatNumber();
        StatTexts[2].text = ((int)PlayerFindManager.instance.Player.hpRegen).FormatNumber();
        StatTexts[3].text = ((int)PlayerFindManager.instance.Player.ATKSpeed).FormatNumber();
        StatTexts[4].text = $"{PlayerFindManager.instance.Player.critical}%";
        StatTexts[5].text = $"{((int)PlayerFindManager.instance.Player.criticalPower).FormatNumber()}%";
        StatTexts[6].text = $"{PlayerFindManager.instance.Player.doubleShot}%";
        StatTexts[7].text = $"{PlayerFindManager.instance.Player.tripleShot}%";
        StatTexts[8].text = $"{((int)PlayerFindManager.instance.Player.highclassATK).FormatNumber()}%";
        StatTexts[9].text = $"{((int)PlayerFindManager.instance.Player.normalAnemyATK).FormatNumber()}%";

        SetPriceText("Atk", PriceTexts[0]);
        SetPriceText("AtkSpeed", PriceTexts[3]);
        SetPriceText("Hp", PriceTexts[1]);
        SetPriceText("HpRegen", PriceTexts[2]);
        SetPriceText("Critical", PriceTexts[4]);
        SetPriceText("CriticalPower", PriceTexts[5]);
        SetPriceText("DoubleShot", PriceTexts[6]);
        SetPriceText("TripleShot", PriceTexts[7]);
        SetPriceText("HighClassAtk", PriceTexts[8]);
        SetPriceText("NomalEnemy", PriceTexts[9]);

        SetBlockText();
        SetBlockObj();
        // 시작할때 레벨 확인후 블락 OBJ 추가 및 삭제
        // 레벨업 누를때 블락 OBJ 삭제
    }
    private void SetBlockText()
    {
        for (int i = 0; i < DataContainer.instance.unlockLevelContainer.Count; i++)
        {
            blockObj[i].blockText.text = string.Format(DataContainer.instance.unlockLevelContainer[1001 + i].descript, DataContainer.instance.unlockLevelContainer[1001 + i].unlockLevel);
        }
    }
    private void SetBlockObj()
    {
        if (UserDataManager.instance.userdata.playerLevelData.AttackSpeedLevel >= DataContainer.instance.unlockLevelContainer[1001].unlockLevel)
            blockObj[0].gameObject.SetActive(false);
        if (UserDataManager.instance.userdata.playerLevelData.DoubleShotPercentLevel >= DataContainer.instance.unlockLevelContainer[1002].unlockLevel)
            blockObj[1].gameObject.SetActive(false);
        if (UserDataManager.instance.userdata.playerLevelData.TripleShotPercentLevel >= DataContainer.instance.unlockLevelContainer[1003].unlockLevel)
            blockObj[2].gameObject.SetActive(false);
        if (UserDataManager.instance.userdata.playerLevelData.HighClassAttackPowerLevel >= DataContainer.instance.unlockLevelContainer[1004].unlockLevel)
            blockObj[3].gameObject.SetActive(false);
    }
    private bool isPress = false;
    private float pressCoolTime = 0, pressTimeLimit = 0.5f;
    private float repeatCoolTime =0, repeatLimit = 0.05f;
   
    private void Update()
    {
        if (isPress)
        {
            pressCoolTime += Time.deltaTime ;
            if (pressCoolTime > pressTimeLimit)
            {
                repeatCoolTime += Time.deltaTime;
                if (repeatCoolTime > repeatLimit)
                {
                    if (buttonAction != null)
                        buttonAction();
                    repeatCoolTime = 0;
                }

            }
        }
        SetPriceTextColor();
    }
    public void OnClick_Down()
    {
        isPress = true;
    }
    public void Onclick_Up()
    {
        isPress = false;
        pressCoolTime = 0;
    }
    public void Onclick_Exit()
    {
        isPress = false;
        pressCoolTime = 0;
    }
    private int GetLevelupPrice(string _type)
    {
        int value = (int)RiseFactorManager.instance.GetLevelUpPrice(UserDataManager.instance.userdata.playerLevelData, _type);
        return value;
    }
    private void SetPriceText(string _type,Text _text)
    {
        int value = GetLevelupPrice(_type);
        _text.text = value.FormatNumber();
        if (ItemInfoManager.instance.FindInventoryItemSlot(1001).count >= value)
            _text.color = Color.white;
        else
            _text.color = Color.red;
    }
    private void SetPriceTextColor()
    {
        SetPriceText("Atk",PriceTexts[0]);
        SetPriceText("AtkSpeed", PriceTexts[3]);
        SetPriceText("Hp", PriceTexts[1]);
        SetPriceText("HpRegen", PriceTexts[2]);
        SetPriceText("Critical", PriceTexts[4]);
        SetPriceText("CriticalPower", PriceTexts[5]);
        SetPriceText("DoubleShot", PriceTexts[6]);
        SetPriceText("TripleShot", PriceTexts[7]);
        SetPriceText("HighClassAtk", PriceTexts[8]);
        SetPriceText("NomalEnemy", PriceTexts[9]);
    }
    public void ButtonAction_Levelup_AttackUp()
    {
        int price = GetLevelupPrice("Atk");
        if (price > ItemInfoManager.instance.FindInventoryItemSlot(1001).count)
        {
            Debug.Log("돈없음");
            return;
        }
        ItemInfoManager.instance.FindInventoryItemSlot(1001).count -= price;
        UserDataManager.instance.userdata.playerLevelData.AttackLevel++;
        Debug.Log($"공격 업{UserDataManager.instance.userdata.playerLevelData.AttackLevel}");
        RiseFactorManager.instance.ATKIUp(PlayerFindManager.instance.Player, UserDataManager.instance.userdata.playerLevelData);
        StatTexts[0].text = ((int)PlayerFindManager.instance.Player.ATK).FormatNumber();
    }
    public void Onclick_Atk() => buttonAction = ButtonAction_Levelup_AttackUp;
    public void ButtonAction_Levelup_AttackSpeed()
    {
        int price = GetLevelupPrice("AtkSpeed");
        if (GetLevelupPrice("AtkSpeed") > ItemInfoManager.instance.FindInventoryItemSlot(1001).count)
        {
            Debug.Log("돈없음");
            return;
        }
        ItemInfoManager.instance.FindInventoryItemSlot(1001).count -= price;
        UserDataManager.instance.userdata.playerLevelData.AttackSpeedLevel++;
        Debug.Log($"공격 스피드 업 {UserDataManager.instance.userdata.playerLevelData.AttackSpeedLevel}");
        RiseFactorManager.instance.ATKSpeedUp(PlayerFindManager.instance.Player, UserDataManager.instance.userdata.playerLevelData);
        StatTexts[3].text = ((int)PlayerFindManager.instance.Player.ATKSpeed).FormatNumber();

        if (UserDataManager.instance.userdata.playerLevelData.AttackSpeedLevel >= DataContainer.instance.unlockLevelContainer[1001].unlockLevel)
            blockObj[0].gameObject.SetActive(false);
    }
    public void Onclick_AtkSpeed() => buttonAction = ButtonAction_Levelup_AttackSpeed;
    public void ButtonAction_Levelup_HP_UP()
    {
        int price = GetLevelupPrice("Hp");
        if (GetLevelupPrice("Hp") > ItemInfoManager.instance.FindInventoryItemSlot(1001).count)
        {
            Debug.Log("돈없음");
            return;
        }
        ItemInfoManager.instance.FindInventoryItemSlot(1001).count -= price;
        UserDataManager.instance.userdata.playerLevelData.HpLevel++;
        Debug.Log($"hp 업{UserDataManager.instance.userdata.playerLevelData.HpLevel}");
        RiseFactorManager.instance.HPUp(PlayerFindManager.instance.Player, UserDataManager.instance.userdata.playerLevelData);
        StatTexts[1].text = ((int)PlayerFindManager.instance.Player.maxHp).FormatNumber();
    }
    public void Onclick_HP() => buttonAction = ButtonAction_Levelup_HP_UP;
    public void ButtonAction_Levelup_HP_Regen()
    {
        int price = GetLevelupPrice("HpRegen");
        if (GetLevelupPrice("HpRegen") > ItemInfoManager.instance.FindInventoryItemSlot(1001).count)
        {
            Debug.Log("돈없음");
            return;
        }
        ItemInfoManager.instance.FindInventoryItemSlot(1001).count -= price;
        UserDataManager.instance.userdata.playerLevelData.HpRegenLevel++;
        Debug.Log($"hp regen 업{UserDataManager.instance.userdata.playerLevelData.HpRegenLevel}");
        RiseFactorManager.instance.HPregenUp(PlayerFindManager.instance.Player, UserDataManager.instance.userdata.playerLevelData);
        StatTexts[2].text = ((int)PlayerFindManager.instance.Player.hpRegen).FormatNumber();

    }
    public void Onclick_HP_Regen() => buttonAction = ButtonAction_Levelup_HP_Regen;
    
  
    public void ButtonAction_Levelup_Critical_Percentage()
    {
        int price = GetLevelupPrice("Critical");
        if (GetLevelupPrice("Critical") > ItemInfoManager.instance.FindInventoryItemSlot(1001).count)
        {
            Debug.Log("돈없음");
            return;
        }
        ItemInfoManager.instance.FindInventoryItemSlot(1001).count -= price;
        UserDataManager.instance.userdata.playerLevelData.CriticalPercentLevel++;
        Debug.Log($"크리티컬 확률 업{UserDataManager.instance.userdata.playerLevelData.CriticalPercentLevel}");
        RiseFactorManager.instance.CriticalUp(PlayerFindManager.instance.Player, UserDataManager.instance.userdata.playerLevelData);
        StatTexts[4].text = $"{PlayerFindManager.instance.Player.critical}%";

    }
    public void Onclick_Critical() => buttonAction = ButtonAction_Levelup_Critical_Percentage;
    public void ButtonAction_Levelup_Critical_Power()
    {
        int price = GetLevelupPrice("CriticalPower");
        if (GetLevelupPrice("CriticalPower") > ItemInfoManager.instance.FindInventoryItemSlot(1001).count)
        {
            Debug.Log("돈없음");
            return;
        }
        ItemInfoManager.instance.FindInventoryItemSlot(1001).count -= price;
        UserDataManager.instance.userdata.playerLevelData.CriticalPowerLevel++;
        Debug.Log($"크리티컬 파워 업{UserDataManager.instance.userdata.playerLevelData.CriticalPowerLevel}");
        RiseFactorManager.instance.CriticalPowerUp(PlayerFindManager.instance.Player, UserDataManager.instance.userdata.playerLevelData);
        StatTexts[5].text = $"{((int)PlayerFindManager.instance.Player.criticalPower).FormatNumber()}%";
    }
    public void Onclick_CriticalPower() => buttonAction = ButtonAction_Levelup_Critical_Power;
    public void ButtonAction_Levelup_DoubleShot()
    {
        int price = GetLevelupPrice("DoubleShot");
        if (GetLevelupPrice("DoubleShot") > ItemInfoManager.instance.FindInventoryItemSlot(1001).count)
        {
            Debug.Log("돈없음");
            return;
        }
        ItemInfoManager.instance.FindInventoryItemSlot(1001).count -= price;
        UserDataManager.instance.userdata.playerLevelData.DoubleShotPercentLevel++;
        Debug.Log($"더블샷 확률 업 {UserDataManager.instance.userdata.playerLevelData.DoubleShotPercentLevel}");
        RiseFactorManager.instance.DoubleShotUp(PlayerFindManager.instance.Player, UserDataManager.instance.userdata.playerLevelData);
        StatTexts[6].text = $"{PlayerFindManager.instance.Player.doubleShot}%";


        if (UserDataManager.instance.userdata.playerLevelData.DoubleShotPercentLevel >= DataContainer.instance.unlockLevelContainer[1002].unlockLevel)
            blockObj[1].gameObject.SetActive(false);
    }
    public void Onclick_DoubleShot() => buttonAction = ButtonAction_Levelup_DoubleShot;
    public void ButtonAction_Levelup_TripleShot()
    {
        int price = GetLevelupPrice("TripleShot");
        if (GetLevelupPrice("TripleShot") > ItemInfoManager.instance.FindInventoryItemSlot(1001).count)
        {
            Debug.Log("돈없음");
            return;
        }
        ItemInfoManager.instance.FindInventoryItemSlot(1001).count -= price;
        UserDataManager.instance.userdata.playerLevelData.TripleShotPercentLevel++;
        Debug.Log($"트리플샷 확률 업 {UserDataManager.instance.userdata.playerLevelData.TripleShotPercentLevel}");
        RiseFactorManager.instance.TripleShotUp(PlayerFindManager.instance.Player, UserDataManager.instance.userdata.playerLevelData);
        StatTexts[7].text = $"{PlayerFindManager.instance.Player.tripleShot}%";

        if (UserDataManager.instance.userdata.playerLevelData.TripleShotPercentLevel >= DataContainer.instance.unlockLevelContainer[1003].unlockLevel)
            blockObj[2].gameObject.SetActive(false);
   
    }
    public void Onclick_TripleShot() => buttonAction = ButtonAction_Levelup_TripleShot; 
    public void ButtonAction_Levelup_HighClassAttackPower()
    {
        int price = GetLevelupPrice("HighClassAtk");
        if (GetLevelupPrice("HighClassAtk") > ItemInfoManager.instance.FindInventoryItemSlot(1001).count)
        {
            Debug.Log("돈없음");
            return;
        }
        ItemInfoManager.instance.FindInventoryItemSlot(1001).count -= price;
        UserDataManager.instance.userdata.playerLevelData.HighClassAttackPowerLevel++;
        Debug.Log($"상위 공격 업 {UserDataManager.instance.userdata.playerLevelData.HighClassAttackPowerLevel}");
        RiseFactorManager.instance.HighClassATKUp(PlayerFindManager.instance.Player, UserDataManager.instance.userdata.playerLevelData);
        StatTexts[8].text = $"{((int)PlayerFindManager.instance.Player.highclassATK).FormatNumber()}%";

        if (UserDataManager.instance.userdata.playerLevelData.HighClassAttackPowerLevel >= DataContainer.instance.unlockLevelContainer[1004].unlockLevel)
            blockObj[3].gameObject.SetActive(false);
    }
    public void Oncilck_HighClassAtk() => buttonAction = ButtonAction_Levelup_HighClassAttackPower;
    public void ButtonAction_NomalEnemyAttackPower()
    {
        int price = GetLevelupPrice("NomalEnemy");
        if (GetLevelupPrice("NomalEnemy") > ItemInfoManager.instance.FindInventoryItemSlot(1001).count)
        {
            Debug.Log("돈없음");
            return;
        }
        ItemInfoManager.instance.FindInventoryItemSlot(1001).count -= price;
        UserDataManager.instance.userdata.playerLevelData.NomalEnemyAttackPowerLevel++;
        Debug.Log($"노말 적 공격 업 {UserDataManager.instance.userdata.playerLevelData.NomalEnemyAttackPowerLevel}");
        RiseFactorManager.instance.NomalAenemyATKUp(PlayerFindManager.instance.Player, UserDataManager.instance.userdata.playerLevelData);
        StatTexts[9].text = $"{((int)PlayerFindManager.instance.Player.normalAnemyATK).FormatNumber()}%";

    }
    public void Onclick_NomalAtkPower() => buttonAction = ButtonAction_NomalEnemyAttackPower;

}
