using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            UserDataManager.instance.SaveUserData();
        }
    }
    private void OnApplicationQuit()
    {
        UserDataManager.instance.SaveUserData();
    }
}
