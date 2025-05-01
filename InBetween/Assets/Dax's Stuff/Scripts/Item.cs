using UnityEngine;

public class Item 
{
    public enum ItemType
    {
       Sword,
       CoolSword,
       Axe,
       CoolAxe,
       Dagger,
       Spear,
       Hammer,
       Sickle,
       PickAxe,
       BattleAxe,
       BigAxe,
       BestSword,
       LongSword,
       Katana,
       PotionOne,
       BronzeArmor,
       SilverArmor,
       GoldArmor,
       DiamondArmor,
       BronzeShield,
       SilverShield,
       GoldShield,
       DiamondShield

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
            case ItemType.Sword:
            case ItemType.Axe:
            case ItemType.Dagger:
            case ItemType.CoolAxe:
            case ItemType.CoolSword:
            case ItemType.Spear:
            case ItemType.Sickle:
            case ItemType.PickAxe:
            case ItemType.BattleAxe:
            case ItemType.BigAxe:
            case ItemType.BestSword:
            case ItemType.LongSword:
            case ItemType.Katana:
                return ItemCategory.Weapon;

            //Armor
            case ItemType.BronzeArmor:
            case ItemType.SilverArmor:
            case ItemType.GoldArmor:
            case ItemType.DiamondArmor:
                return ItemCategory.Armor;

            //Shields
            case ItemType.BronzeShield:
            case ItemType.SilverShield:
            case ItemType.GoldShield:
            case ItemType.DiamondShield:
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
            case ItemType.Sword: return 5; //used to add attack damage for the player
            case ItemType.CoolSword: return 10;
            case ItemType.Axe: return 15;
            case ItemType.CoolAxe: return 20;
            case ItemType.Dagger: return 5;
            case ItemType.PotionOne: return 25; //How much health the potion heals for
            case ItemType.BronzeArmor: return 25; //Additional Health
            case ItemType.SilverArmor: return 50;
            case ItemType.GoldArmor: return 75;
            case ItemType.DiamondArmor: return 100;
            case ItemType.BronzeShield: return 10;
            case ItemType.SilverShield: return 20;
            case ItemType.GoldShield: return 30;
            case ItemType.DiamondShield: return 50;
            case ItemType.Spear: return 10;
            case ItemType.Sickle: return 15;
            case ItemType.PickAxe: return 15;
            case ItemType.BattleAxe: return 30;
            case ItemType.BigAxe: return 25;
            case ItemType.BestSword: return 50;
            case ItemType.LongSword: return 25;
            case ItemType.Katana: return 40;

        }
    }

    public static string GetDescription(ItemType itemType)
    {
        switch(itemType)
        {
            default: return "Item not found";
            case ItemType.Sword: return "Attack Damage: +5";
            case ItemType.CoolSword: return "Attack Damage: +10";
            case ItemType.Axe: return "Attack Damage: +15";
            case ItemType.CoolAxe: return "Attack Damage: +20";
            case ItemType.Dagger: return "Attack Damage: +5";
            case ItemType.PotionOne: return "Heals For +50 Health";
            case ItemType.BronzeArmor: return "+25 Max Health";
            case ItemType.SilverArmor: return "+50 Max Health";
            case ItemType.GoldArmor: return "+75 Max Health";
            case ItemType.DiamondArmor: return "+100 Max Health";
            case ItemType.BronzeShield: return "+10 Max Health";
            case ItemType.SilverShield: return "+20 Max Health";
            case ItemType.GoldShield: return "+30 Max Health";
            case ItemType.DiamondShield: return "+50 Max Health";
            case ItemType.Spear: return "Attack Damage: +10";
            case ItemType.Sickle: return "Attack Damage: +15";
            case ItemType.PickAxe: return "Attack Damage: +15";
            case ItemType.BattleAxe: return "Attack Damage: +30";
            case ItemType.BigAxe: return "Attack Damage: +25";
            case ItemType.BestSword: return "Attack Damage: +50";
            case ItemType.LongSword: return "Attack Damage: +25";
            case ItemType.Katana: return "Attack Damage: +40";

        }
    }

    public static int GetCost(ItemType itemType) //Holds cost in coins for each item 
    {
        switch(itemType)
        {
            default:
            case ItemType.Sword: return 20; //Number is cost in coins
            case ItemType.CoolSword: return 35;
            case ItemType.Axe: return 50;
            case ItemType.CoolAxe: return 80;
            case ItemType.Dagger: return 15;
            case ItemType.PotionOne: return 10;
            case ItemType.BronzeArmor: return 50;
            case ItemType.SilverArmor: return 100;
            case ItemType.GoldArmor: return 150;
            case ItemType.DiamondArmor: return 250;
            case ItemType.BronzeShield: return 25;
            case ItemType.SilverShield: return 50;
            case ItemType.GoldShield: return 100;
            case ItemType.DiamondShield: return 150;
            case ItemType.Spear: return 30;
            case ItemType.Sickle: return 25;
            case ItemType.PickAxe: return 50;
            case ItemType.BattleAxe: return 100;
            case ItemType.BigAxe: return 85;
            case ItemType.BestSword: return 200;
            case ItemType.LongSword: return 125;
            case ItemType.Katana: return 150;


        }
    }

    public static Sprite GetSprite(ItemType itemType) //holds sprite images for each item in the item shop 
    {
        switch(itemType)
        {
            default:
            case ItemType.Sword: return GameAssets.i.s_Sword; //Sprite for each itme in the shop 
            case ItemType.CoolSword: return GameAssets.i.s_CoolSword;
            case ItemType.Axe: return GameAssets.i.s_Axe;
            case ItemType.CoolAxe: return GameAssets.i.s_CoolAxe;
            case ItemType.Dagger: return GameAssets.i.s_Dagger;
            case ItemType.PotionOne: return GameAssets.i.s_PotionOne;
            case ItemType.BronzeArmor: return GameAssets.i.s_BronzeArmor;
            case ItemType.SilverArmor: return GameAssets.i.s_SilverArmor;
            case ItemType.GoldArmor: return GameAssets.i.s_GoldArmor;
            case ItemType.DiamondArmor: return GameAssets.i.s_DiamondArmor;
            case ItemType.BronzeShield: return GameAssets.i.s_BronzeShield;
            case ItemType.SilverShield: return GameAssets.i.s_SilverShield;
            case ItemType.GoldShield: return GameAssets.i.s_GoldShield;
            case ItemType.DiamondShield: return GameAssets.i.s_DiamondShield;
            case ItemType.Spear: return GameAssets.i.s_Spear;
            case ItemType.Sickle: return GameAssets.i.s_Sickle;
            case ItemType.PickAxe: return GameAssets.i.s_Pickaxe;
            case ItemType.BattleAxe: return GameAssets.i.s_BattleAxe;
            case ItemType.BigAxe: return GameAssets.i.s_BigAxe;
            case ItemType.BestSword: return GameAssets.i.s_BestSword;
            case ItemType.LongSword: return GameAssets.i.s_LongSword;
            case ItemType.Katana: return GameAssets.i.s_Katana;

        }
    }
}
