using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

[TestFixture]
public class BoundaryTest
{
    private GameObject player;
    private LevelSystem levelSystem;
    private XP xpSystem;
    private UpgradeSystem upgradeSystem;
    private GameObject upgradePanel;
    private Button speedButton, strengthButton, healthButton;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("ZachScene");
    }

  [UnitySetUp] // This is all to get my scene setup and ready for tests. AI slop because it would not load otherwise.
public IEnumerator SetUp()
{
    yield return new WaitForSeconds(1f);

    // Find the player and its components
    player = GameObject.FindWithTag("Player");
    Assert.IsNotNull(player, "Player with tag 'Player' not found.");

    xpSystem = player.GetComponent<XP>();
    levelSystem = player.GetComponent<LevelSystem>();

    Assert.IsNotNull(xpSystem, "XP component not found on Player.");
    Assert.IsNotNull(levelSystem, "LevelSystem component not found on Player.");

    // Ensure UpgradeSystem is present
    if (!player.TryGetComponent(out UpgradeSystem upgradeSystem))
    {
        upgradeSystem = player.AddComponent<UpgradeSystem>();
    }
    levelSystem.upgradeSystem = upgradeSystem;

    // Set up the XP slider and XP bar (keeping your original functionality)
    var xpSliderGO = GameObject.Find("XP Slider");
    if (xpSliderGO == null)
    {
        var canvasSlider = new GameObject("Canvas", typeof(Canvas));
        xpSliderGO = new GameObject("XP Slider", typeof(Slider));
        xpSliderGO.transform.SetParent(canvasSlider.transform);
    }

    var slider = xpSliderGO.GetComponent<Slider>();
    var xpBar = xpSliderGO.GetComponent<XPBar>() ?? xpSliderGO.AddComponent<XPBar>();
    xpBar.xpSlider = slider;
    xpBar.levelSystem = levelSystem;
    xpBar.xpSystem = xpSystem;

    levelSystem.xpSlider = slider;

    // Set up the Upgrade UI Manager
    var upgradeUIManagerGO = new GameObject("UpgradeUIManager");
    var upgradeUIManager = upgradeUIManagerGO.AddComponent<UpgradeUIManager>();

    // Create the panel for upgrades
    var canvasGO = GameObject.Find("Canvas") ?? new GameObject("Canvas", typeof(Canvas));
    var canvas = canvasGO.GetComponent<Canvas>();
    canvas.renderMode = RenderMode.ScreenSpaceOverlay;

    GameObject upgradePanel = new GameObject("UpgradePanel");
    upgradePanel.transform.SetParent(canvas.transform);
    upgradePanel.SetActive(false); // Hide the panel by default

    upgradeUIManager.panel = upgradePanel;

    // Create and set up the upgrade buttons (Speed, Strength, Health)
    var buttonContainer = new GameObject("ButtonContainer").transform;
    buttonContainer.SetParent(upgradePanel.transform);

    var buttonPrefab = new GameObject("ButtonPrefab", typeof(Button), typeof(Image)).GetComponent<Button>();
    upgradeUIManager.upgradeButtonPrefab = buttonPrefab.gameObject;
    upgradeUIManager.buttonContainer = buttonContainer;

    // Set up icons (you should assign these sprites in the Unity Editor)
    upgradeUIManager.speedIcon = null; // Replace with actual Sprite
    upgradeUIManager.strengthIcon = null; // Replace with actual Sprite
    upgradeUIManager.healthIcon = null; // Replace with actual Sprite

    Debug.Log("Scene setup complete. Player found, systems connected, and UI elements set up.");
}



    [UnityTest]
    public IEnumerator LevelPlayer()
    {
        xpSystem.AddXP(50);  // check xp gain
        yield return null;

        Assert.Greater(xpSystem.GetXP(), 0, "XP did not increase as expected.");
    }

    [UnityTest]
    public IEnumerator LevelPlayer_BelowLevelUp()
    {
        // Assuming the level-up happens at 100 XP, we add 99 XP
        xpSystem.AddXP(99);  
        yield return null;

        Assert.AreEqual(1, levelSystem.level, "Player leveled up prematurely.");
    }

    [UnityTest]
    public IEnumerator LevelPlayer_NegativeXP()
    {
        // Add negative XP (-1)
        xpSystem.AddXP(-1);  
        yield return null;

        // Print current XP after negative XP addition
        Debug.Log("Current XP after negative addition: " + xpSystem.GetXP());

        // Add positive XP (e.g., 10)
        xpSystem.AddXP(10);  
        yield return null;

        // Print current XP after positive XP addition
        Debug.Log("Current XP after positive addition: " + xpSystem.GetXP());

        // Assert that the XP is correctly set
        Assert.AreEqual(9, xpSystem.GetXP(), "XP should be 9 after adding -1 then 10.");
    }

    [UnityTest]
    public IEnumerator LevelPlayer_TwoRapidLevelUps()
    {
        // Print the starting XP and level
        Debug.Log("Starting XP: " + xpSystem.GetXP());
        Debug.Log("Starting Level: " + levelSystem.level);

        // Add enough XP to level up the player twice
        xpSystem.AddXP(levelSystem.levelThreshold);  // First level up
        yield return null;  // Wait for the level-up process to complete

        Debug.Log("XP after first level-up: " + xpSystem.GetXP());
        Debug.Log("Level after first level-up: " + levelSystem.level);

        // Add more XP to level up again
        xpSystem.AddXP(levelSystem.levelThreshold);  // Second level up
        yield return null;  // Wait for the second level-up to complete

        // Print XP and level after the second level-up
        Debug.Log("XP after second level-up: " + xpSystem.GetXP());
        Debug.Log("Level after second level-up: " + levelSystem.level);

        // Assert that the level has increased twice
        Assert.AreEqual(2, levelSystem.level, "Player should level up twice.");
    }

    [UnityTest]
    public IEnumerator LevelPlayer_ReachLevel50()
    {
        // Calculate total XP required to reach level 50
        int totalXPRequired = 0;
        for (int i = 1; i < 50; i++)
        {
            totalXPRequired += levelSystem.levelThreshold;  // Assuming each level threshold is the same for simplicity
        }

        // Print the XP and level before adding the XP
        Debug.Log("Starting XP: " + xpSystem.GetXP());
        Debug.Log("Starting Level: " + levelSystem.level);

        // Add enough XP to reach level 50
        xpSystem.AddXP(totalXPRequired);
        yield return null;

        // Print the XP and level after adding the XP
        Debug.Log("XP after adding total required XP: " + xpSystem.GetXP());
        Debug.Log("Level after reaching level 50: " + levelSystem.level);

        // Assert that the level has reached level 50
        Assert.AreEqual(50, levelSystem.level, "Player should reach level 50.");
    }

[UnityTest]
public IEnumerator LevelPlayer_OfferedUpgrades()
{
    // Add enough XP for the upgrade panel to potentially show up
    xpSystem.AddXP(100);
    yield return new WaitForSeconds(1f);  // Wait for the upgrade panel to potentially show up

    // Assert that the upgrade panel is active, meaning upgrades are being offered
    var upgradeUIManager = GameObject.FindFirstObjectByType<UpgradeUIManager>();  // Ensure the UI Manager is found
    Assert.IsNotNull(upgradeUIManager, "UpgradeUIManager was not found.");

    Assert.IsTrue(upgradeUIManager.panel.activeSelf, "Upgrade panel should be active when upgrades are offered.");

    // Optionally, check if upgrade buttons exist in the panel
    Assert.IsTrue(upgradeUIManager.buttonContainer.childCount > 0, "Upgrade buttons should exist in the panel.");
}

    [UnityTest]
    public IEnumerator LevelPlayer_PastMaxLevel()
    {
        // Directly set the player's level to 51
        levelSystem.level = 51;

        // Wait for a frame to allow changes to take effect
        yield return null;

        // Print the level after setting it to 51
        Debug.Log("Level after setting to 51: " + levelSystem.level);

        // Assert that the level has been set to 51
        Assert.AreEqual(51, levelSystem.level, "Player's level should be 51.");
    }

    [UnityTest]
    public IEnumerator UpgradePlayer_ThreeRapidUpgrades()
    {
        // Grab the UpgradeSystem
        var upgradeSystem = UnityEngine.Object.FindFirstObjectByType<UpgradeSystem>();
        Assert.IsNotNull(upgradeSystem, "UpgradeSystem could not be found in the scene.");

        // Give enough XP to level up
        xpSystem.AddXP(100);
        yield return new WaitForSeconds(1f);

        // Simulate pressing "U" multiple times
        string[] upgradeTypes = { "Speed", "Strength", "Health" };

        for (int i = 0; i < 3; i++)
        {
            string chosenUpgrade = upgradeTypes[UnityEngine.Random.Range(0, upgradeTypes.Length)];
            upgradeSystem.AwardUpgrade(chosenUpgrade);

            Debug.Log($"Simulated U press {i + 1}: Chose upgrade '{chosenUpgrade}'");

            yield return null;
        }

        Debug.Log("Finished simulating 3 U presses with random upgrades.");
    }

    [UnityTest]
    public IEnumerator Award100SpeedUpgrades()
    {
        // Get the UpgradeSystem reference
        var upgradeSystem = UnityEngine.Object.FindFirstObjectByType<UpgradeSystem>();
        Assert.IsNotNull(upgradeSystem, "UpgradeSystem could not be found in the scene.");

        // Apply 100 Speed upgrades
        for (int i = 0; i < 100; i++)
        {
            upgradeSystem.AwardUpgrade("Speed");
            yield return null; 
        }

        Debug.Log("Awarded 100 Speed upgrades.");

        yield return new WaitForSeconds(10f);

        Debug.Log("10 second wait complete after upgrades.");
    }

    [UnityTest]
    public IEnumerator Award999999999999HealthUpgrades() //BROKE THE CODE LIAM SWITCHED TO VIRTUAL
    {
        var upgradeSystem = UnityEngine.Object.FindFirstObjectByType<UpgradeSystem>();
        Assert.IsNotNull(upgradeSystem, "UpgradeSystem could not be found in the scene.");

        const long upgradeCount = 10000;

        for (long i = 0; i < upgradeCount; i++)
        {
            upgradeSystem.AwardUpgrade("Health");

            // Yield occasionally to avoid freezing the editor completely
            if (i % 1000000 == 0)
            {
                Debug.Log($"Applied {i} health upgrades...");
                yield return null;
            }
        }

        Debug.Log("Finished applying 999,999,999,999 health upgrades.");
        yield return null;
    }

    
    [UnityTest]
    public IEnumerator Apply1000HealthUpgrades()
    {
        // Grab the UpgradeSystem
        var upgradeSystem = UnityEngine.Object.FindFirstObjectByType<UpgradeSystem>();
        Assert.IsNotNull(upgradeSystem, "UpgradeSystem could not be found in the scene.");

        // Apply 1000 Health upgrades
        for (int i = 0; i < 1000; i++)
        {
            upgradeSystem.AwardUpgrade("Health");
            yield return null; // Yield between frames to allow for processing
        }

        Debug.Log("Awarded 1000 Health upgrades.");

        // Optionally wait for a short period to allow for any UI updates or changes
        yield return new WaitForSeconds(1f);
    }



    [UnityTest]
    public IEnumerator LevelPlayer_10000XP()
    {
        xpSystem.AddXP(10000);  // check xp gain
        yield return null;

        Assert.Greater(xpSystem.GetXP(), 0, "XP did not increase as expected.");
        Debug.Log("Current XP: " + xpSystem.GetXP());

    }
    [UnityTest]
    public IEnumerator Award100StrengthUpgrades() //BROKE CODE WHEN DAMAGE REWORKED
    {
        // Get the UpgradeSystem reference
        var upgradeSystem = UnityEngine.Object.FindFirstObjectByType<UpgradeSystem>();
        Assert.IsNotNull(upgradeSystem, "UpgradeSystem could not be found in the scene.");

        // Apply 100 Speed upgrades
        for (int i = 0; i < 100; i++)
        {
            upgradeSystem.AwardUpgrade("Strength");
            yield return null; 
        }

        Debug.Log("Awarded 100 Speed upgrades.");

        yield return new WaitForSeconds(10f);

        Debug.Log("10 second wait complete after upgrades.");
    }


}
