using UnityEngine;
using UnityEngine.UI;

public class TestID : MonoBehaviour
{
    public Text Text;
    private void Start()
    {
        Text = gameObject.GetComponent<Text>();
        Debug.Log(Text == null);
        Text.text = UserDataManager.instance.userID;
    }

}
