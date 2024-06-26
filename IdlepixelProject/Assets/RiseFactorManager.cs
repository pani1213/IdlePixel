using Unity.Mathematics;
using UnityEngine;

public class RiseFactorManager : Singleton<RiseFactorManager>
{
    public void init()
    {
        Player player = PlayerFindManager.instance.Player;
        Debug.Log(UserDataManager.instance.userdata == null);
        StatLevel userData = UserDataManager.instance.userdata.playerLevelData;

        if (userData.AttackLevel > 1) ATKIUp(player, userData);
        if (userData.AttackSpeedLevel > 1) ATKSpeedUp(player, userData);
        if (userData.HpLevel > 1) HPUp(player, userData);
        if (userData.HpRegenLevel > 1) HPregenUp(player, userData);
        if (userData.CriticalPercentLevel > 1) CriticalUp(player, userData);
        if (userData.CriticalPowerLevel > 1) CriticalPowerUp(player, userData);
        if (userData.DoubleShotPercentLevel > 1) DoubleShotUp(player, userData);
        if (userData.TripleShotPercentLevel > 1) TripleShotUp(player, userData);
        if (userData.HighClassAttackPowerLevel > 1) HighClassATKUp(player, userData);
        if (userData.NomalEnemyAttackPowerLevel > 1) NomalAenemyATKUp(player, userData);
    }
    public void ATKIUp(Player player, StatLevel userData) => player.ATK = GetBetweenLevelData(userData.AttackLevel).atkRiseFactor* userData.AttackLevel;
    public void ATKSpeedUp(Player player, StatLevel userData) => player.ATKSpeed = GetBetweenLevelData(userData.AttackSpeedLevel).atkSpeedFactor* userData.AttackSpeedLevel;
    public void HPUp(Player player, StatLevel userData) => player.maxHp = GetBetweenLevelData(userData.HpLevel).hpRiseFactor * userData.HpLevel;
    public void HPregenUp(Player player, StatLevel userData) => player.hpRegen = GetBetweenLevelData(userData.HpRegenLevel).hpRegenFactor * userData.HpRegenLevel;
    public void CriticalUp(Player player, StatLevel userData) => player.critical = GetBetweenLevelData(userData.CriticalPercentLevel).criticalFactor * userData.CriticalPercentLevel;
    public void CriticalPowerUp(Player player, StatLevel userData) => player.criticalPower = GetBetweenLevelData(userData.CriticalPowerLevel).criticalPowerFactor * userData.CriticalPowerLevel;
    public void DoubleShotUp(Player player, StatLevel userData) => player.doubleShot = GetBetweenLevelData(userData.DoubleShotPercentLevel).doubleShotFactor * userData.DoubleShotPercentLevel;
    public void TripleShotUp(Player player, StatLevel userData) => player.tripleShot = GetBetweenLevelData(userData.TripleShotPercentLevel).tripleShotFactor * userData.TripleShotPercentLevel;
    public void HighClassATKUp(Player player, StatLevel userData) => player.highclassATK = GetBetweenLevelData(userData.HighClassAttackPowerLevel).highClassAtkFactor * userData.HighClassAttackPowerLevel;
    public void NomalAenemyATKUp(Player player, StatLevel userData) => player.normalAnemyATK = GetBetweenLevelData(userData.NomalEnemyAttackPowerLevel).nomalEnemyFactor * userData.NomalEnemyAttackPowerLevel;
    /// <summary>
    /// -1 == null
    /// </summary>
    /// <param name="userData"></param>
    /// <param name="_type"></param>
    /// <returns></returns>
    public float GetLevelUpPrice(StatLevel userData,string _type)
    {
        switch (_type)
        {
            case "Atk":
                return GetBetweenLevelData(userData.AttackLevel).atkRisePrice * userData.AttackLevel;
            case "AtkSpeed":
                return GetBetweenLevelData(userData.AttackSpeedLevel).atkSpeedPrice* userData.AttackSpeedLevel;
            case "Hp":
                return GetBetweenLevelData(userData.HpLevel).hpRisePrice * userData.HpLevel;
            case "HpRegen":
                return GetBetweenLevelData(userData.HpRegenLevel).hpRegenPrice * userData.HpRegenLevel;
            case "Critical":
                return GetBetweenLevelData(userData.CriticalPercentLevel).criticalPrice * userData.CriticalPercentLevel;
            case "CriticalPower":
                return GetBetweenLevelData(userData.CriticalPowerLevel).criticalPowerPrice * userData.CriticalPowerLevel;
            case "DoubleShot":
                return GetBetweenLevelData(userData.DoubleShotPercentLevel).doubleShotPrice * userData.DoubleShotPercentLevel;
            case "TripleShot":
                return GetBetweenLevelData(userData.TripleShotPercentLevel).tripleShotPrice * userData.TripleShotPercentLevel;
            case "HighClassAtk":
                return GetBetweenLevelData(userData.HighClassAttackPowerLevel).highClassAtkPrice * userData.HighClassAttackPowerLevel;
            case "NomalEnemy":
                return GetBetweenLevelData(userData.NomalEnemyAttackPowerLevel).nomalEnemyPrice * userData.NomalEnemyAttackPowerLevel;
            default:
                return -1;
        }
    }
    private LevelData GetBetweenLevelData(int _level)
    {
        for (int i = 0; i < DataContainer.instance.levelDataContainer.Count; i++) 
        {
            if (_level.Between(DataContainer.instance.levelDataContainer[1001+i].startLevel, DataContainer.instance.levelDataContainer[1001+i].endLevel))
            {
                return DataContainer.instance.levelDataContainer[1001+i];
            }
        }
        Debug.Log($"데이터에 맞는 레벨이 아닙니다 레벨: {_level}");
        Debug.Log($"탐색한 levelData의 Count : {DataContainer.instance.levelDataContainer.Count}");
        return null;
    }
}
