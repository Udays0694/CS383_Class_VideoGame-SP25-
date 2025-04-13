using UnityEngine;

public interface IShopCustomer 
{
    void BoughtItem(Item.ItemType itemType);

    bool TrySpendCoins(int coins);
}
