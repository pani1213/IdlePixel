using UnityEngine;

public class ADBlessingController : MonoBehaviour
{
    public void ButtonAction_PopupAD()
    {
        Debug.Log("hi");
        UIPopupManager.instance.PopupUI("PopupADBlessing");
    }
}
