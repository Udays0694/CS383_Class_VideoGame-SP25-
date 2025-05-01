using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public Image weaponSlot;
    public Image armorSlot;
    public Image shieldSlot;

    public void UpdateInventorySlot(Item.ItemCategory category, Sprite sprite)
    {
        switch(category)
        {
            case Item.ItemCategory.Weapon:
                weaponSlot.sprite = sprite;
                break;

            case Item.ItemCategory.Armor:
                armorSlot.sprite = sprite;
                break;

            case Item.ItemCategory.Shield:
                shieldSlot.sprite = sprite;
                break;
        }
    }
}
