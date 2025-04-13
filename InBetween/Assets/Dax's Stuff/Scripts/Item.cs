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

    public static int GetCost(ItemType itemType)
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

    public static Sprite GetSprite(ItemType itemType)
    {
        switch(itemType)
        {
            default:
            case ItemType.SwordOne: return GameAssets.i.s_SwordOne;
            case ItemType.SwordTwo: return GameAssets.i.s_SwordTwo;
            case ItemType.AxeOne: return GameAssets.i.s_AxeOne;
            case ItemType.AxeTwo: return GameAssets.i.s_AxeTwo;
            case ItemType.DaggerOne: return GameAssets.i.s_DaggerOne;
            case ItemType.PotionOne: return GameAssets.i.s_PotionOne;

        }
    }
}
