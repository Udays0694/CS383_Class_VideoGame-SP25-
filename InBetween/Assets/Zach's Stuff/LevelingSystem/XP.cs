using UnityEngine;

public class XP : MonoBehaviour
{
    // Step 1: Static variable to hold the instance of the XP class
    private static XP _instance;

    // Step 2: Public property to access the singleton instance
    public static XP Instance
    {
        get
        {
            // If the instance is not yet created, find it in the scene or create a new one
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<XP>();
                if (_instance == null)
                {
                    // If no instance is found, create a new GameObject to hold the XP component
                    GameObject go = new GameObject("XP");
                    _instance = go.AddComponent<XP>();
                }
            }
            return _instance;
        }
    }

    // Existing XP class variables
    private int xp = 0;
    private int level = 1;

    // Step 3: AddXP function (no changes here)
    public void AddXP(int amount)
    {
        xp += amount;
    }

    // Step 4: GetLevel function (no changes here)
    public int GetLevel()
    {
        return level;
    }

    // Step 5: GetXP function (no changes here)
    public int GetXP()
    {
        return xp;
    }

    // Step 6: Private constructor to prevent direct instantiation
    private XP() { }
}
