using System;
using UnityEngine;
using UnityEngine.UI;

public class GachaItemSlot : MonoBehaviour
{
    public Text LevelTextUI, ExpTextUI;
    public Image ItemImageUI, ExpBarImageUI;
    public int id;
    public GachaItemInvenData currentSlot = null;
    public GameObject BlockObject;
    protected bool isHave;
    public Action<int> selectAction;
    public void init(int _id, Action<int> _action)
    {
        id = _id;
        currentSlot = ItemInfoManager.instance.FindGachaInvenItemSlot(id);
        isHave = (currentSlot != null);
        BlockObject.SetActive(!isHave);
        ItemImageUI.sprite = DataContainer.instance.GetItemSprite(_id);
        if (isHave)
        {
            LevelTextUI.text = $"Lv.{currentSlot.level}";
            ExpTextUI.text = UserDataManager.instance.userdata.PrintExpFormet(id);
        }
        else
        {
            LevelTextUI.text = "Lv.1";
            ExpTextUI.text = "0/3";
        }
        selectAction = _action;
    }
    public void ButtonAction_SelectAction()
    {
        Debug.Log(selectAction == null);
        if (selectAction != null)
            selectAction(id);
    }
}
