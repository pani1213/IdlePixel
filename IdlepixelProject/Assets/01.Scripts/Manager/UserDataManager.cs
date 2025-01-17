using UnityEngine;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using Firebase;

public class UserDataManager : Singleton<UserDataManager>
{
    public UserData userdata = new UserData();
    public string userID;
    private int coinID = 1001, JemID = 1002;

    public void init()
    {
        UserDataInIt();
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                FirebaseManager.instance.dataReference = FirebaseDatabase.DefaultInstance.RootReference;
            }
        });
    }

    public void UserDataInIt()
    {
        userdata.playerStageInfo.Chapter = 1;
        userdata.playerStageInfo.Stage = 1;
        userdata.playerStageInfo.Round = 0;
        userdata.playerStageInfo.isClearBoss = false;

        userdata.playerLevelData.AttackLevel = 1;
        userdata.playerLevelData.HpLevel = 1;
        userdata.playerLevelData.HpRegenLevel = 1;
        userdata.playerLevelData.AttackSpeedLevel = 1;
        userdata.playerLevelData.CriticalPercentLevel = 1;
        userdata.playerLevelData.CriticalPowerLevel = 1;
        userdata.playerLevelData.DoubleShotPercentLevel = 1;
        userdata.playerLevelData.TripleShotPercentLevel = 1;
        userdata.playerLevelData.HighClassAttackPowerLevel = 1;
        userdata.playerLevelData.NomalEnemyAttackPowerLevel = 1;

        userdata.playerGachaData.weaponGachaLevel = 1;
        userdata.playerGachaData.weaponGachaCount = 0;
        userdata.playerGachaData.SkillLevel = 1;
        userdata.playerGachaData.SkillGachaCount = 0;
        userdata.playerGachaData.PetLevel = 1;
        userdata.playerGachaData.PetGachaCount = 0;

        userdata.userInvenData = new List<InventoryItemData>();

        ItemInfoManager.instance.InsertItem(coinID,0);
        ItemInfoManager.instance.InsertItem(JemID,0);


        userdata.PlayerEquipData.Petids =new List<int>();
        for (int i = 0; i < 5; i++)
            userdata.PlayerEquipData.Petids.Add(0);
        userdata.PlayerEquipData.Skillids =new List<int>();
        for (int i = 0; i < 6; i++)
            userdata.PlayerEquipData.Skillids.Add(0);
    }
    public void SaveUserData()
    {
        string userkey = "users/" + userID;
        Debug.Log("저장호출");
        StartCoroutine(DBTimeManager.instance.GetCurrentServerTimeCoroutine(time =>
        {
            if (userdata.LogoutTime == "")
                userdata.LogoutTime = time.DateTimeToString();
            string jsonText = JsonUtility.ToJson(userdata, true);

            FirebaseManager.instance.dataReference.Child(userkey).SetRawJsonValueAsync(jsonText).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                    Debug.Log("해당경로에 저장성공");
                else
                    Debug.Log("에러발생 에러코드:" + task.Exception);
            });
        }));
    
    }
}
