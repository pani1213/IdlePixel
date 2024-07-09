using System;
using UnityEngine;
public enum DungeonType { None, Gem, Gold }
public class PopupDungeonUI : Popup
{
    public override void Close()
    {
        base.Close();
        action();
    }
    public override void init(Action _action)
    {
        base.init(_action);
   
    }
    public void ButtonActionInDungeon(int _dungeonType)
    {
        Debug.Log((DungeonType)_dungeonType);
        UIPopupManager.instance.PopupUI("Popup_DungeonStageUI").GetComponent<PopupDungeonStageUI>().init((DungeonType)_dungeonType);


    }
}
