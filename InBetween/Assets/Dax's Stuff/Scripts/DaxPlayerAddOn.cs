using UnityEngine;

public class DaxPlayerAddOn : MonoBehaviour, IShopCustomer
{
    public void BoughtItem(Item.ItemType itemType)
    {
        Debug.Log("Bought item: " + itemType);
    }
}
