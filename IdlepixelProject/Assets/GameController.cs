using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { WaitingInput, Spawn, ClearCheck}

public class GameController : Singleton<GameController>
{
    public int CurrentStage;
    public Transform[] spawnPoints;
    //UI
    public LevelupUI LevelupUI;
    public GameObject UICanvas;

    public List<Monster> currentSpawnMonsters = new List<Monster>();
    public GameState gameState = GameState.WaitingInput;

    private void Start()
    {
        //DataContainer.instance.init();
        //JsonPasingManager.instance.init();
        //UserDataManager.instance.init();
        RiseFactorManager.instance.init();
        LevelupUI.init();
        PetController.instance.init();
        DataContainer.instance.GetGachaItemData();

        Debug.Log("GetCurrentServerTimeCoroutine 코루틴 함수 호출");
       // StartCoroutine(DBTimeManager.instance.GetCurrentServerTimeCoroutine(serverTime =>
       // {
       //     UserDataManager.instance.userdata.LoginTime = serverTime.DateTimeToString();
       //     Debug.Log($"서버시간 저장 {serverTime}");
       // }));
    }
    public void NextRound()
    {
        int Round = UserDataManager.instance.userdata.playerStageInfo.Round; 
        int MonsterCount = DataContainer.instance.stageDataContainer[((UserDataManager.instance.userdata.playerStageInfo.Chapter*1000)+(UserDataManager.instance.userdata.playerStageInfo.Stage))].monsterSpawnCount[Round];
        RoundUI.instance.SetRoundBar(Round);
        StartCoroutine(MonsterSpawn(MonsterCount));
    }
    private void Update()
    {
        switch (gameState)
        {
            case GameState.WaitingInput:
                NextRound();
                break;
            case GameState.Spawn:
                break;
            case GameState.ClearCheck:
                ClearCheck();
                break;
        }



        if (Input.GetKeyDown(KeyCode.S))
            UserDataManager.instance.SaveUserData();
        if (Input.GetKeyDown(KeyCode.D))
            PlayerPrefs.DeleteAll();
        if (Input.GetKeyDown(KeyCode.I))
            Debug.Log(UserDataManager.instance.userID);

    }
    private void ClearCheck()
    {
        if (currentSpawnMonsters.Count <= 0)
        {
            SetNextRound();
            // 이동하는 애니메이션 -> GameState = GameState.WaitingInput;
            gameState = GameState.WaitingInput;
            //Debug.Log($"gameState:{GameState.WaitingInput}");
        }
    }
    private IEnumerator MonsterSpawn(int _spawnCount)
    {
        gameState = GameState.Spawn;
        //Debug.Log($"gameState:{GameState.Spawn}");
        int count = 0;
        while (count < _spawnCount)
        {
            int monsterId = 0;
            if (UserDataManager.instance.userdata.playerStageInfo.Round == 4)
                monsterId = DataContainer.instance.stageDataContainer[int.Parse($"{UserDataManager.instance.userdata.playerStageInfo.Chapter}00{UserDataManager.instance.userdata.playerStageInfo.Stage}")].bossId;
            else
                monsterId = DataContainer.instance.stageDataContainer[int.Parse($"{UserDataManager.instance.userdata.playerStageInfo.Chapter}00{UserDataManager.instance.userdata.playerStageInfo.Stage}")].monsterId;

            MonsterData monsterdata = DataContainer.instance.monsterDataContainer[monsterId];

            PoolObject obj = ObjectPooler.instance.GetPoolObject(monsterdata.monsterPrefabName);
            Monster monster = obj.GetComponent<Monster>();
            monster.Monsterinit(monsterdata);
            currentSpawnMonsters.Add(monster);
            obj.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            count++;
            yield return new WaitForSeconds(1f);
        }
        //Debug.Log($"gameState:{GameState.ClearCheck}");
        gameState = GameState.ClearCheck;
    }
    public void ClearStage()
    {
        SetPrevRound();
        gameState = GameState.WaitingInput;
        //Debug.Log($"gameState:{GameState.WaitingInput}");
        for (int i = currentSpawnMonsters.Count-1; i >= 0; i--)
        {
            ObjectPooler.instance.InsertPoolObject(currentSpawnMonsters[i]);
            currentSpawnMonsters.RemoveAt(i);
        }
    }
    private void SetNextRound()
    {
        UserDataManager.instance.userdata.playerStageInfo.Round++;

        if (UserDataManager.instance.userdata.playerStageInfo.Round == 5)
        {
            UserDataManager.instance.userdata.playerStageInfo.Round = 0;

            if (UserDataManager.instance.userdata.playerStageInfo.Stage == 10)
            {
                UserDataManager.instance.userdata.playerStageInfo.Stage = 1;

                if (UserDataManager.instance.userdata.playerStageInfo.Chapter == 10)
                {
                    UserDataManager.instance.userdata.playerStageInfo.Chapter = 1;
                }
                else
                    UserDataManager.instance.userdata.playerStageInfo.Chapter++;
            }
            else
                UserDataManager.instance.userdata.playerStageInfo.Stage++;
            
        }
    }
    public void SetPrevRound() 
    {
        if (UserDataManager.instance.userdata.playerStageInfo.Stage == 1)
        {
            if (UserDataManager.instance.userdata.playerStageInfo.Chapter >= 2)
            { 
                UserDataManager.instance.userdata.playerStageInfo.Chapter--;
                UserDataManager.instance.userdata.playerStageInfo.Stage = 10;
            }
            else
                UserDataManager.instance.userdata.playerStageInfo.Chapter = 1;
        }
        else
        {
            UserDataManager.instance.userdata.playerStageInfo.Stage--;
        }
        UserDataManager.instance.userdata.playerStageInfo.Round = 0;
    }
}
