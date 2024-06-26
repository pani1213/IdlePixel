using Firebase.Database;
using Firebase.Extensions;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginSceneController : MonoBehaviour
{
    public Text logText;
    DatabaseReference reference;
    public GameObject[] LogingameBtns;
    bool isFirest = false;
    private void Start()
    {
        DataContainer.instance.init();
        JsonPasingManager.instance.init();
        UserDataManager.instance.init();
        reference = FirebaseDatabase.DefaultInstance.RootReference;  // 파이어베이스 접속

        FileHandler fileHandler = new FileHandler();

        string fileName = "UserID.txt";
        UserDataManager.instance.userID = System.Guid.NewGuid().ToString();//SystemInfo.deviceUniqueIdentifier;
        (bool, string) temp = fileHandler.SaveOrLoadFile(fileName, UserDataManager.instance.userID);
        isFirest = temp.Item1;
        UserDataManager.instance.userID = temp.Item2;

        if (!isFirest)
        {
            for (int i = 0; i < LogingameBtns.Length; i++) LogingameBtns[i].gameObject.SetActive(false);
            GetUserData();
        }
    }
    public void ButtonAction_GuestLogin()
    {
        if (isFirest)
        {
            Debug.Log("첫 로그인 파이어 베이스에 아이디 작성");
            string json = JsonUtility.ToJson(UserDataManager.instance.userdata);
            reference.Child("users").Child(UserDataManager.instance.userID).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task => { });

            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            Debug.Log("파이어 베이스에서 데이터 불러오기");
            GetUserData();
        }
    }
    private void GetUserData()
    {
        reference.Child("users").Child(UserDataManager.instance.userID).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("파이어베이스에서 가져올수없음.");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    string jsonstring = snapshot.GetRawJsonValue();
                    Debug.Log("파이어베이스에서 데이터를 가져옴" + jsonstring);
                    UserDataManager.instance.userdata = JsonUtility.FromJson<UserData>(jsonstring);
                    SceneManager.LoadSceneAsync(1);
                }
                else
                    Debug.Log("파이어베이스에서 해당 유저데이터 찾을수 없음" + UserDataManager.instance.userdata);
            }
        });
    }
    public void ButtonAction_GoogleLogin()
    {
        Debug.Log("구글 로그인");
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        SingIn();
    }
    public void SingIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            UserDataManager.instance.userID = PlayGamesPlatform.Instance.GetUserId();
            string imgurl = PlayGamesPlatform.Instance.GetUserImageUrl();

            FileHandler fileHandler = new FileHandler();
            fileHandler.SaveToFile("UserID.txt", PlayGamesPlatform.Instance.GetUserId());

            UserDataManager.instance.SaveUserData();

            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            Debug.Log("Sign in failed!");
        }
    }
  
}
