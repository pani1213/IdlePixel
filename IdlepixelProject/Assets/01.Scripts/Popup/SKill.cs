using UnityEngine;
using UnityEngine.UI;

public class SKill : MonoBehaviour
{
    public Image skillImage, SkillCoolTimeImage;
    public void init(int _id)
    {
        if (_id == 0)
        {
            skillImage.sprite = DataContainer.instance.gachaItemAtlas.GetSprite("Pictoicon_Add");
        }
        else
        {
            GachaItemData data = DataContainer.instance.allGachaData[_id];
            skillImage.sprite = DataContainer.instance.gachaItemAtlas.GetSprite(data.prefabName);
        }
    }
}