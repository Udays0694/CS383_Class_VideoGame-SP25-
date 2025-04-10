using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public PlayerScript playerScript; // Reference to PlayerScript

    void Start()
    {
        // Find the player GameObject and get the PlayerScript component
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();

        if (playerScript == null)
        {
            Debug.LogError("PlayerScript not found! Ensure PlayerScript is attached to the player object.");
        }
    }

    public void AwardUpgrade(string upgradeType)
    {
        if (playerScript == null)
        {
            Debug.LogError("PlayerScript is not initialized, can't award upgrades.");
            return; // Exit if PlayerScript is not found
        }

        switch (upgradeType)
        {
            case "Speed":
                IncreaseSpeed();
                break;
            case "Strength":
                IncreaseStrength();
                break;
            case "Health":
                IncreaseHealth();
                break;
            default:
                Debug.LogWarning("Unknown upgrade type");
                break;
        }
    }

    private void IncreaseSpeed()
    {
        playerScript.moveSpeed += 5f; // Increase speed
        Debug.Log("Speed Upgrade Awarded! New Speed: " + playerScript.moveSpeed);
    }

    private void IncreaseStrength()
    {
        playerScript.strength += 2; // Increase strength
        Debug.Log("Strength Upgrade Awarded! New Strength: " + playerScript.strength);
    }

    private void IncreaseHealth()
    {
        playerScript.health += 50f; // Increase health
        Debug.Log("Health Upgrade Awarded! New Health: " + playerScript.health);
    }
}
