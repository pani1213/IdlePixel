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
        reference = FirebaseDatabase.DefaultInstance.RootReference;  // ���̾�̽� ����

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
            Debug.Log("ù �α��� ���̾� ���̽��� ���̵� �ۼ�");
            string json = JsonUtility.ToJson(UserDataManager.instance.userdata);
            reference.Child("users").Child(UserDataManager.instance.userID).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task => { });

            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            Debug.Log("���̾� ���̽����� ������ �ҷ�����");
            GetUserData();
        }
    }
    private void GetUserData()
    {
        reference.Child("users").Child(UserDataManager.instance.userID).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("���̾�̽����� �����ü�����.");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    string jsonstring = snapshot.GetRawJsonValue();
                    Debug.Log("���̾�̽����� �����͸� ������" + jsonstring);
                    UserDataManager.instance.userdata = JsonUtility.FromJson<UserData>(jsonstring);
                    SceneManager.LoadSceneAsync(1);
                }
                else
                    Debug.Log("���̾�̽����� �ش� ���������� ã���� ����" + UserDataManager.instance.userdata);
            }
        });
    }
    public void ButtonAction_GoogleLogin()
    {
        Debug.Log("���� �α���");
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
