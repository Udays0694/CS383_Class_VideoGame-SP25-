using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public PlayerScript playerScript; // Reference to PlayerScript

    void Start()
    {
        playerScript = GetComponent<PlayerScript>(); // Get PlayerScript component from the same GameObject
        if (playerScript == null)
        {
            Debug.LogError("PlayerScript not found!");
        }
    }

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
        playerScript.moveSpeed += 5f; // Increase speed
        Debug.Log("Speed Upgrade Awarded! New Speed: " + playerScript.moveSpeed);
    }

    private void IncreaseStrength()
    {
        // Assuming you have strength implemented
        Debug.Log("Strength Upgrade Awarded!");
    }

    private void IncreaseHealth()
    {
        playerScript.health += 50f; // Increase health
        Debug.Log("Health Upgrade Awarded! New Health: " + playerScript.health);
    }
}
