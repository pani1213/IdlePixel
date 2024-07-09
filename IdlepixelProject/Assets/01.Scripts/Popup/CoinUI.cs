using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public Text coinText;
    public int coinID = 1001;
    void Update()
    {
        coinText.text = ItemInfoManager.instance.FindInventoryItemSlot(coinID).count.FormatNumber();
        if (Input.GetKeyDown(KeyCode.Space))
            ItemInfoManager.instance.InsertItem(1001,500);
    }
}
