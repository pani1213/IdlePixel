using System;
using UnityEngine;

public class IdleRewardManager : Singleton<IdleRewardManager>
{

    DateTime LastLogin_dateTime, currentLogin_dateTime;

    private void Start()
    {
        LastLogin_dateTime = new DateTime(2024, 7, 9, 1, 00, 00, 00);
        currentLogin_dateTime = DateTime.Now;

        TimeSpan timeSpan = LastLogin_dateTime - currentLogin_dateTime;
        Debug.Log($"지난 시간 = {timeSpan}, 초로 환산 = {timeSpan.TotalSeconds}");
    }
    public void Buttonaction_PopupIdleReward()
    {
        UIPopupManager.instance.PopupUI("Popup_IdleReward").init();
    }

}
