using UnityEngine; 

public class XP : MonoBehaviour
{
    // 
    private class XPData
    {
        public int xp = 0;
        public int level = 1;
    }

    // Step 2: Static variable to hold the instance of the XP class
    private static XP _instance;

    // Step 3: Public property to access the singleton instance
    public static XP Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<XP>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("XP");
                    _instance = go.AddComponent<XP>();
                }
            }
            return _instance;
        }
    }

    // Step 4: Create an instance of XPData to hold the internal state
    private XPData _xpData;

    // Step 5: Constructor initializes the internal data
    private XP()
    {
        _xpData = new XPData();
    }

    // Step 6: AddXP function to modify xp
    public void AddXP(int amount)
    {
        _xpData.xp += amount;
    }

    // Step 7: GetLevel function to access level
    public int GetLevel()
    {
        return _xpData.level;
    }

    // Step 8: GetXP function to access xp
    public int GetXP()
    {
        return _xpData.xp;
    }

}
//Pattern implementations,
//Private class data and singleton
//p - encapsulate internal data better, reducing risk of manipulation by other code
//s- ensure only 1 instance of xp occurs while used in many things like xpbar and player etc, easily accessible through instance

//private class data is perfect for encapsulation, and singleton mostly implemented for requirements, strategy could be good as well to make extending functionality easy eg. modifiers.
//bad time to use these patterns are when the data should be easily and readily accessible and changable to those other than me. as im the only one working with xp, keep it modular and easily modifiable is perfect for me.
//+------------------+
//|     XP           |         <<Singleton>>
//+------------------+
//| - instance: XP                   // static
//| - data: XPData                   // private class data
//+------------------+
//| + Instance: XP                  // public static property
//| + AddXP(amount: int): void
//| + GetLevel(): int
//| + GetXP(): int
//| + GetXPToNextLevel(): int
//| + LevelUp(): void
//+------------------+
//          |
//          |
//          v
//+------------------+
//|   XPData         |         <<Private Class Data>>
//+------------------+
//| - level: int
//| - experience: int
//| - xpToNextLevel: int
//+------------------+
//| + GetLevel(): int
//| + AddXP(amount: int): void
//| + GetXP(): int
//| + GetXPToNextLevel(): int
//| + Reset(): void
//+------------------+
