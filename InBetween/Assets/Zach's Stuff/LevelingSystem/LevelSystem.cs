using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public XP xpSystem;
    public int levelThreshold = 100;
    public int level = 1;
    public int levelCap = 50;
    public Slider xpSlider;
    private UpgradeSystem upgradeSystem;
    private PlayerScript playerScript;

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

        upgradeSystem = GetComponent<UpgradeSystem>();
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
            GainXP(10); // Give player 10 XP
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            LevelUp(); // Simulate level-up manually
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

        level++;
        xpSystem.AddXP(-levelThreshold);
        levelThreshold += 50;

        if (xpSlider != null)
        {
            xpSlider.maxValue = levelThreshold;
        }

        ApplyStatIncreases();

        UpgradeUIManager uiManager = FindFirstObjectByType<UpgradeUIManager>();
        if (uiManager != null)
        {
            uiManager.ShowUpgradeOptions();
        }
        else
        {
            Debug.LogWarning("Upgrade UI Manager not found in scene.");
        }
    }

    private void ApplyStatIncreases()
    {
        PlayerScript player = FindFirstObjectByType<PlayerScript>();
        if (player != null)
        {
            player.moveSpeed += 0.2f;
            player.strength += 1;
            player.playerScript.setHealth(-2f); // Heal for 2

            Debug.Log($"Passive stat increases applied: Speed={player.moveSpeed}, Strength={player.strength}, Health={player.playerScript.getHealth()}");
        }
        else
        {
            Debug.LogWarning("PlayerScript not found when applying stat increases.");
        }
    }
}
