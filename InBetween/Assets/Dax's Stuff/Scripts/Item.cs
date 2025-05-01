using UnityEngine;

public class Item 
{
    public enum ItemType
    {
       SwordOne,
       SwordTwo,
       AxeOne,
       AxeTwo,
       DaggerOne,
       PotionOne
    }

    public enum ItemCategory
    {
        Weapon,
        Armor,
        Shield,
        Potion
    }

    public static ItemCategory GetCategory(ItemType itemType)
    {
        switch(itemType)
        {
            //Weapons
            case ItemType.SwordOne:
            case ItemType.AxeOne:
            case ItemType.DaggerOne:
                return ItemCategory.Weapon;

            //Armor
            case ItemType.AxeTwo:
                return ItemCategory.Armor;

            //Shields
            case ItemType.SwordTwo:
                return ItemCategory.Shield;

            //Potions 
            case ItemType.PotionOne:
                return ItemCategory.Potion;

            default:
                return ItemCategory.Weapon;

        }
    }

    public static int GetEffectValue(ItemType itemType) //Holds the attack damage / health changes of each item that can be bought 
    {
        switch(itemType)
        {
            default: return 0;
            case ItemType.SwordOne: return 5; //used to add attack damage for the player
            case ItemType.SwordTwo: return 10;
            case ItemType.AxeOne: return 15;
            case ItemType.AxeTwo: return 20;
            case ItemType.DaggerOne: return 5;
            case ItemType.PotionOne: return 50; //How much health the potion heals for 
        }
    }

    public static string GetDescription(ItemType itemType)
    {
        switch(itemType)
        {
            default: return "Item not found";
            case ItemType.SwordOne: return "Attack Damage: +5";
            case ItemType.SwordTwo: return "Attack Damage: +10";
            case ItemType.AxeOne: return "Attack Damage: +15";
            case ItemType.AxeTwo: return "Attack Damage: +20";
            case ItemType.DaggerOne: return "Attack Damage: +5";
            case ItemType.PotionOne: return "Heals For +50 Health";

        }
    }

    public static int GetCost(ItemType itemType) //Holds cost in coins for each item 
    {
        switch(itemType)
        {
            default:
            case ItemType.SwordOne: return 100; //Number is cost in coins
            case ItemType.SwordTwo: return 50;
            case ItemType.AxeOne: return 75;
            case ItemType.AxeTwo: return 85;
            case ItemType.DaggerOne: return 125;
            case ItemType.PotionOne: return 10;


        }
    }

    public static Sprite GetSprite(ItemType itemType) //holds sprite images for each item in the item shop 
    {
        switch(itemType)
        {
            default:
            case ItemType.SwordOne: return GameAssets.i.s_SwordOne; //Sprite for each itme in the shop 
            case ItemType.SwordTwo: return GameAssets.i.s_SwordTwo;
            case ItemType.AxeOne: return GameAssets.i.s_AxeOne;
            case ItemType.AxeTwo: return GameAssets.i.s_AxeTwo;
            case ItemType.DaggerOne: return GameAssets.i.s_DaggerOne;
            case ItemType.PotionOne: return GameAssets.i.s_PotionOne;

        }
    }
}
