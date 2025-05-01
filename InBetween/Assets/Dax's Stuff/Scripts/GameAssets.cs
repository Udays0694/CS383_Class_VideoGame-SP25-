using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    public Sprite s_Sword;
    public Sprite s_CoolSword;
    public Sprite s_Axe;
    public Sprite s_CoolAxe;
    public Sprite s_Dagger;
    public Sprite s_PotionOne;
    public Sprite s_BronzeArmor;
    public Sprite s_SilverArmor;
    public Sprite s_GoldArmor;
    public Sprite s_DiamondArmor;
    public Sprite s_BronzeShield;
    public Sprite s_SilverShield;
    public Sprite s_GoldShield;
    public Sprite s_DiamondShield;
    public Sprite s_Spear;
    public Sprite s_Sickle;
    public Sprite s_BattleAxe;
    public Sprite s_BigAxe;
    public Sprite s_Pickaxe;
    public Sprite s_BestSword;
    public Sprite s_LongSword;
    public Sprite s_Katana;

}