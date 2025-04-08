using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public XP xpSystem;
    public int levelThreshold = 100;
    public int level = 1;

    public int levelCap = 50;

    public PlayerScript playerScript; // Reference to the PlayerScript

    void Start()
    {
        xpSystem = GetComponent<XP>();
        if (xpSystem == null)
        {
            Debug.LogError("XP component not found on the GameObject.");
        }

        // Attempt to find PlayerScript
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (playerScript == null)
        {
            Debug.LogError("PlayerScript not found on the Player.");
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
        // Increase player stats - let's start with just increasing speed for now
        if (playerScript != null)
        {
            float speedIncrease = 1f; // Increase speed by 1 each level up
            playerScript.moveSpeed += speedIncrease; // Increase player speed
            Debug.Log($"Player speed increased to {playerScript.moveSpeed}");
        }
        else
        {
            Debug.LogWarning("PlayerScript component not found on the GameObject.");
        }
    }
}
