using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIPopupManager : Singleton<UIPopupManager>
{
    public GameObject UILayerObj, NoticeLayerObj;
    // popup 을 컨트롤할 popup
    public List<Popup> PopupList = new List<Popup>();
    // 참조를 가져올 Addressable Asset List
    [SerializeField] List<AssetReference> popupReferences;
    // 실질적으로 매니저에서 꺼내쓸 Dictionary
    Dictionary<string, Popup> PopupObjs = new Dictionary<string, Popup>();

    private void Start()
    {
        init();
    }
    public void init()
    {
        foreach (AssetReference assetReference in popupReferences) 
        {
            assetReference.LoadAssetAsync<GameObject>().Completed +=
                (AsyncOperationHandle<GameObject> obj) =>
                {
                    Popup popup = obj.Result.GetComponent<Popup>();
                    PopupObjs.Add(popup.name, popup);
                };
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PopupClose();
        }
    }
    public Popup PopupUI(string _prefabName)
    {
        Popup popup = Instantiate(PopupObjs[_prefabName], GetPopupLayer(PopupObjs[_prefabName].PopupType).transform);
        PopupList.Add(popup);
        return popup;
    }
    public void PopupClose()
    {
        if (PopupList.Count > 0)
        {
            Popup popup = PopupList[PopupList.Count - 1];
            PopupList.RemoveAt(PopupList.Count - 1);
            if (popup.action != null)
                popup.action();
            Destroy(popup.gameObject);
        }
    }
    public void PopupClose(Popup _popup)
    {
        for (int i = PopupList.Count-1; i >= 0; i--)
        {
            if (PopupList[i] == _popup)
                PopupList.RemoveAt(i);
        }
        Destroy(_popup.gameObject);
    }
    public GameObject GetPopupLayer(PopupType _popupType)
    {
        switch (_popupType)
        {
            case PopupType.UI:
                return UILayerObj;
            case PopupType.Notice:
                return NoticeLayerObj;
            default:
                return null;
        }
    }
}
