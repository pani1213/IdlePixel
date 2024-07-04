using UnityEngine;
using UnityEngine.UI;

public class Test_CurrentTime : MonoBehaviour
{
    public Text time_text;
    public void Buttonaction_GetCurrentTime()
    {
         StartCoroutine(DBTimeManager.instance.GetCurrentServerTimeCoroutine(tast => { time_text.text = tast.DateTimeToString();

         }));
    }
    public void SaveButton()
    {
        UserDataManager.instance.SaveUserData();
    }
}
