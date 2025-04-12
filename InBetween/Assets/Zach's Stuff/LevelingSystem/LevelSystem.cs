using UnityEngine;
using UnityEngine.UI;

public class LevelSystemBase : MonoBehaviour // Superclass for dynamic binding
{
    // Virtual method that can be overridden in subclasses
    public virtual void GainXP(int amount)
    {
        Debug.Log("Base GainXP called with: " + amount); // Virtual method in the base class
    }
}

public class LevelSystem : LevelSystemBase // Subclass for static binding
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
        // Demonstrate dynamic binding
        LevelSystemBase baseRef = this;

        if (Input.GetKeyDown(KeyCode.X))
        {
            baseRef.GainXP(10); // Dynamically bound to the overridden method in LevelSystem
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            LevelUp(); // Manual level-up
        }

        // To demonstrate static binding, you can call the base GainXP method directly like this:
        if (Input.GetKeyDown(KeyCode.Y))
        {
            CallBaseGainXP(10); // This calls the base method, demonstrating static binding
        }
    }

    public override void GainXP(int amount) // Overridden method for dynamic binding
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

        Debug.Log("Overridden GainXP called with: " + amount);
    }

    // Static binding example: calls the base class's GainXP method directly
    public void CallBaseGainXP(int amount)
    {
        base.GainXP(amount); // Static binding - calls the base class method directly
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
