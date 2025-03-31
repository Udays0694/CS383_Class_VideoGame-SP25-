using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public XP xpSystem;
    public int levelThreshold = 100;
    public int level = 1;

    public int levelCap = 50;

    void Start()
    {
        xpSystem = GetComponent<XP>();
        if (xpSystem == null)
        {
            Debug.LogError("XP component not found on the GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GainXP(10); // On 'X' input give XP for testing
        }
    }

    public void GainXP(int amount)
    {
        xpSystem.AddXP(amount);
        if (xpSystem.GetXP() >= levelThreshold)
        {
            LevelUp();
        }
    }

private void LevelUp()
{
    // Check if the player is at the maximum level (50 in this case)
    if (level >= 50)
    {
        Debug.LogWarning("Player has reached the maximum level! Leveling up is not possible.");
        return; // Prevent leveling up beyond 50
    }

    level++; // Only increment if not at max level
    xpSystem.AddXP(-levelThreshold);
    levelThreshold += 50; // Increase level up threshold

    IncreasePlayerStats();
    Debug.LogWarning($"Player Leveled Up! Current Level: {level}");
}


    private void IncreasePlayerStats()
    {
        PlayerBuffs playerStats = GetComponent<PlayerBuffs>();
        if (playerStats != null)
        {
            playerStats.IncreaseStats(10f, 2, 2); // Increment player stats
            //Debug.LogWarning($"Stats Increased! Health: {health}, Strength: {Strength}, Agility: {Agility} ")
        }
        else
        {
            Debug.LogWarning("PlayerBuffs component not found on the GameObject.");
        }
    }
}
