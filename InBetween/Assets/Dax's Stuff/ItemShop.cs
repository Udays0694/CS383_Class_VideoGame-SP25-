using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public GameObject shopPanel;

    private void Start()
    {
        shopPanel.SetActive(true);
    }

    public void onShopButtonClick()
    {
        shopPanel.SetActive(true);
    }

    public void onCloseButtonClick()
    {
        shopPanel.SetActive(false);
    }
}
