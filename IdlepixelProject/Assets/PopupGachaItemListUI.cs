using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PopupGachaItemListUI : Popup
{
    public GachaItemListSlotUI[] slots;
    public void init(int[] _ids)
    {
        Refresh(_ids);
    }
    public void Refresh(int[] _ids)
    {
       for (int i = 0; i < slots.Length; i++) 
        {
            if (i < _ids.Length)
            {
                slots[i].gameObject.SetActive(true);
                slots[i].init(_ids[i]);
            }
            else slots[i].gameObject.SetActive(false);

        }
    }

}
