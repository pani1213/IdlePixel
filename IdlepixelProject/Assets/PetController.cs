using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PetController : Singleton<PetController>
{
    public SpriteLibraryAsset[] petSpriteAssets;
    public Dictionary<string, SpriteLibraryAsset> petSpriteContainer = new Dictionary<string, SpriteLibraryAsset>();
    public Pet[] petSlots;

    public void init()
    {
        for (int i = 0; i < petSpriteAssets.Length; i++)
            petSpriteContainer.Add(petSpriteAssets[i].name, petSpriteAssets[i]);
        PetSlotRefresh();
    }
    public void PetSlotRefresh()
    {
        Debug.Log("�� �������� ����");
        SetPetSlot();
    }
    // userdata �� petids ������ �°� pet ����
    public void SetPetSlot()
    {
        for (int i = 0; i < UserDataManager.instance.userdata.PlayerEquipData.Petids.Count; i++) petSlots[i].init(UserDataManager.instance.userdata.PlayerEquipData.Petids[i]);
    }
}
