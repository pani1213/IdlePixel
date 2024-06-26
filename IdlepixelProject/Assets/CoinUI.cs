using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public Text coinText;
    int coinID = 1001;
    void Update()
    {
        coinText.text = ItemInfoManager.instance.FindInventoryItemSlot(coinID).count.FormatNumber();

        if (Input.GetKey(KeyCode.A))
        {
            ItemInfoManager.instance.InsertItem(coinID, 1000);
        }
    }
}
