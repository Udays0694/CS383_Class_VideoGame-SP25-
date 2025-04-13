using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;

[TestFixture]
public class RapidUpgradeTest
{
    private GameObject player;
    private UpgradeSystem upgradeSystem;
    private PlayerScript playerScript;

    // Set up the scene before each test run
    [OneTimeSetUp]
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ZachScene");
    }

    // Set up the player and components before each test
    [SetUp]
    public void SetUp()
    {
        player = new GameObject("Player");
        player.tag = "Player"; // Ensure the player tag is set correctly

        // Add PlayerScript component
        playerScript = player.AddComponent<PlayerScript>();

        // Add UpgradeSystem component and assign the playerScript
        upgradeSystem = player.AddComponent<UpgradeSystem>();
        upgradeSystem.playerScript = playerScript;

        // Optionally reset the player stats to known values before each test
        playerScript.moveSpeed = 10f;
        playerScript.strength = 5;
        playerScript.health = 100f;

        Debug.Log("Setup complete for rapid upgrade test.");
    }

    // Test case to simulate rapid pressing of U to upgrade the player
    [UnityTest]
    public IEnumerator RapidUpgradeTest_UpgradingPlayer()
    {
        Debug.Log("Starting RapidUpgradeTest...");

        // Initial stats of the player
        float initialSpeed = playerScript.moveSpeed;
        int initialStrength = playerScript.strength;
        float initialHealth = playerScript.health;

        // Press 'U' key multiple times rapidly and simulate the upgrades
        for (int i = 0; i < 10; i++) // Simulate 10 presses
        {
            upgradeSystem.AwardUpgrade("Speed"); // Upgrade speed
            upgradeSystem.AwardUpgrade("Strength"); // Upgrade strength
            upgradeSystem.AwardUpgrade("Health"); // Upgrade health
            yield return null; // Wait for the next frame
        }

        // Log stats after upgrades
        Debug.Log($"Player Stats after upgrades: Speed: {playerScript.moveSpeed}, Strength: {playerScript.strength}, Health: {playerScript.health}");

        // Assert that all stats have increased from their initial values
        Assert.Greater(playerScript.moveSpeed, initialSpeed, "Speed should have increased.");
        Assert.Greater(playerScript.strength, initialStrength, "Strength should have increased.");
        Assert.Greater(playerScript.health, initialHealth, "Health should have increased.");

        yield return null;
    }

    // Optionally, you can add other tests to simulate other aspects of upgrades
}
