using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    // Reference to the PlayerScript
    private PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        if (playerScript == null)
        {
            Debug.LogError("PlayerScript not found!");
        }
    }

    // This method will be called to award upgrades
    public void AwardUpgrade(string upgradeType)
    {
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
        playerScript.moveSpeed += 5f; // Increase move speed by 5 (example value)
        Debug.Log("Speed Upgrade Awarded! New Speed: " + playerScript.moveSpeed);
    }

    private void IncreaseStrength()
    {
        // Assuming you have strength implemented
        // playerScript.strength += 10; // Increase strength by 10 (example value)
        Debug.Log("Strength Upgrade Awarded!");
    }

    private void IncreaseHealth()
    {
        playerScript.health += 50f; // Increase health by 50 (example value)
        Debug.Log("Health Upgrade Awarded! New Health: " + playerScript.health);
    }
}
