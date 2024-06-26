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
    public void ATKIUp(Player player, StatLevel userData) => player.ATK = math.pow(GetBetweenLevelData(userData.AttackLevel).atkRiseFactor, userData.AttackLevel);
    public void ATKSpeedUp(Player player, StatLevel userData) => player.ATKSpeed = math.pow(GetBetweenLevelData(userData.AttackSpeedLevel).atkSpeedFactor, userData.AttackSpeedLevel);
    public void HPUp(Player player, StatLevel userData) => player.maxHp = math.pow(GetBetweenLevelData(userData.HpLevel).hpRiseFactor, userData.HpLevel);
    public void HPregenUp(Player player, StatLevel userData) => player.hpRegen = math.pow(GetBetweenLevelData(userData.HpRegenLevel).hpRegenFactor, userData.HpRegenLevel);
    public void CriticalUp(Player player, StatLevel userData) => player.critical = math.pow(GetBetweenLevelData(userData.CriticalPercentLevel).criticalFactor, userData.CriticalPercentLevel);
    public void CriticalPowerUp(Player player, StatLevel userData) => player.criticalPower = math.pow(GetBetweenLevelData(userData.CriticalPowerLevel).criticalPowerFactor, userData.CriticalPowerLevel);
    public void DoubleShotUp(Player player, StatLevel userData) => player.doubleShot = math.pow(GetBetweenLevelData(userData.DoubleShotPercentLevel).doubleShotFactor, userData.DoubleShotPercentLevel);
    public void TripleShotUp(Player player, StatLevel userData) => player.tripleShot = math.pow(GetBetweenLevelData(userData.TripleShotPercentLevel).tripleShotFactor, userData.TripleShotPercentLevel);
    public void HighClassATKUp(Player player, StatLevel userData) => player.highclassATK = math.pow(GetBetweenLevelData(userData.HighClassAttackPowerLevel).highClassAtkFactor, userData.HighClassAttackPowerLevel);
    public void NomalAenemyATKUp(Player player, StatLevel userData) => player.normalAnemyATK = math.pow(GetBetweenLevelData(userData.NomalEnemyAttackPowerLevel).nomalEnemyFactor, userData.NomalEnemyAttackPowerLevel);
  
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
