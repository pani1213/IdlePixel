using UnityEngine;
using UnityEngine.UI;

public class GachaItemListSlotUI : MonoBehaviour
{
    public Image itemImageUI, itemTypeImageUI;

    public void init(int _id)
    {

        GachaItemData data = DataContainer.instance.allGachaData[_id];
        itemImageUI.sprite = DataContainer.instance.GetItemSprite(_id);
        if (data.ItemType == ItemType.Weapon)
        {
            itemTypeImageUI.gameObject.SetActive(true);
            itemTypeImageUI.sprite = DataContainer.instance.gachaItemAtlas.GetSprite("Icon_Sword");
        }
        else if (data.ItemType == ItemType.Armor)
        { 
            itemTypeImageUI.gameObject.SetActive(true);
            itemTypeImageUI.sprite = DataContainer.instance.gachaItemAtlas.GetSprite("Icon_Shield");
        }
        else
            itemTypeImageUI.gameObject.SetActive(false);
    }
}
