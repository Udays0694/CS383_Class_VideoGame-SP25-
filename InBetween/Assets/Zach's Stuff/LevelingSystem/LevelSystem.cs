using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public XP xpSystem;
    public int levelThreshold = 100;
    public int level = 1;
    public int levelCap = 50;
    public Slider xpSlider; // Reference to the XP Slider
    private UpgradeSystem upgradeSystem; // Reference to UpgradeSystem

    void Start()
    {
        if (xpSystem == null)
        {
            xpSystem = GetComponent<XP>();
            if (xpSystem == null)
            {
                Debug.LogError("XP component not found on the GameObject.");
            }
        }

        upgradeSystem = GetComponent<UpgradeSystem>(); // Initialize UpgradeSystem
        if (upgradeSystem == null)
        {
            Debug.LogError("UpgradeSystem component not found on the GameObject.");
        }

        if (xpSlider != null)
        {
            xpSlider.maxValue = levelThreshold;
        }
        else
        {
            Debug.LogError("XP Slider is not assigned.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GainXP(10); // On 'X' input, give XP for testing
        }
    }

    public void GainXP(int amount)
    {
        xpSystem.AddXP(amount);
        if (xpSystem.GetXP() >= levelThreshold)
        {
            LevelUp();
        }

        if (xpSlider != null)
        {
            xpSlider.value = xpSystem.GetXP();
        }
    }

    private void LevelUp()
    {
        if (level >= levelCap)
        {
            Debug.LogWarning("Player has reached the maximum level! Leveling up is not possible.");
            return;
        }

        level++; // Only increment if not at max level
        xpSystem.AddXP(-levelThreshold); // Subtract XP equal to the threshold (so it resets)
        levelThreshold += 50; // Increase level-up threshold (for the next level)

        if (xpSlider != null)
        {
            xpSlider.maxValue = levelThreshold;
        }

        IncreasePlayerStats();

        string upgradeGiven = AwardRandomUpgrade();
        Debug.LogWarning($"Player Leveled Up! Current Level: {level}. Upgrade given: {upgradeGiven}");
    }

    private void IncreasePlayerStats()
    {
        PlayerBuffs playerStats = GetComponent<PlayerBuffs>();
        if (playerStats != null)
        {
            playerStats.IncreaseStats(10f, 2, 2);
        }
        else
        {
            Debug.LogWarning("PlayerBuffs component not found on the GameObject.");
        }
    }

    private string AwardRandomUpgrade()
    {
        string[] upgradeTypes = { "Speed", "Strength", "Health" };
        string randomUpgrade = upgradeTypes[Random.Range(0, upgradeTypes.Length)];
        upgradeSystem.AwardUpgrade(randomUpgrade);
        return randomUpgrade;
    }
}
