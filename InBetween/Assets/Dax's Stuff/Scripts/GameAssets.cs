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

    public Sprite s_SwordOne;
    public Sprite s_SwordTwo;
    public Sprite s_AxeOne;
    public Sprite s_AxeTwo;
    public Sprite s_DaggerOne;
    public Sprite s_PotionOne;

}