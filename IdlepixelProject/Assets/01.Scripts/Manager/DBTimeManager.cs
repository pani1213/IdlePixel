using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using UnityEngine;

public class DBTimeManager : Singleton<DBTimeManager>
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetServerTimestamp();
        }
    }
  
    public IEnumerator GetCurrentServerTimeCoroutine(Action<DateTime> callback)
    {
        if (FirebaseManager.instance.dataReference != null)
        {
            DatabaseReference timeRef = FirebaseManager.instance.dataReference.Child("server_time");

            // 서버 시간 저장
                
            var saveTask = timeRef.SetValueAsync(ServerValue.Timestamp);
            yield return new WaitUntil(() => saveTask.IsCompleted);

            if (saveTask.Exception == null)
            {
                // 서버 시간 가져오기
                var getTask = timeRef.GetValueAsync();
                yield return new WaitUntil(() => getTask.IsCompleted);

                if (getTask.Exception == null && getTask.Result.Exists)
                {
                    long timestamp = (long)getTask.Result.Value;
                    DateTime serverTime = UnixTimeStampToDateTime(timestamp);
                    callback(serverTime); // 콜백 호출
                }
                else
                    Debug.LogError("Failed to get server time: " + getTask.Exception);
            }
            else
                Debug.LogError("Failed to save server time: " + saveTask.Exception);
        }
        else
            Debug.LogError("Database reference is not initialized.");
        
    }
    public void GetServerTimestamp()
    {
        if (FirebaseManager.instance.dataReference != null)
        {
            DatabaseReference timeRef = FirebaseManager.instance.dataReference.Child("server_time");
            timeRef.GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.Exists)
                    {
                        long timestamp = (long)snapshot.Value;
                        System.DateTime serverTime = UnixTimeStampToDateTime(timestamp);
                        Debug.Log("Server Time: " + serverTime.ToString());
                    }
                }
            });
        }
    }
    private System.DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        dateTime = dateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }
}
