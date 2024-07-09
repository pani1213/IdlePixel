using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseManager : Singleton<FirebaseManager>
{
    public DatabaseReference dataReference;

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                dataReference = FirebaseDatabase.DefaultInstance.RootReference;

                LoginSceneController.instance.init();

            }
        });
    }
  

}
