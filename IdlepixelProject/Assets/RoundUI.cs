using UnityEngine.UI;

public class RoundUI : Singleton<RoundUI>
{
    public Image Fill_ImageUI;
    public Text StageText;
    public void SetRoundText()
    {

    }
    public void SetRoundBar(int _Round)
    {
        float fill = 0;
        if (_Round == 0)
            fill = 0.2f;
        else
            fill = (_Round + 1) * 0.2f;

        Fill_ImageUI.fillAmount = fill;

        StageText.text = $"{UserDataManager.instance.userdata.playerStageInfo.Chapter} - {UserDataManager.instance.userdata.playerStageInfo.Stage}";
    }
}
