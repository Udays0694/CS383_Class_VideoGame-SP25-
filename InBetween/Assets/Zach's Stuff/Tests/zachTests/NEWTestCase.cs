using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[TestFixture]
public class BoundaryTest
{
    private GameObject player;
    private LevelSystem levelSystem;
    private XP xpSystem;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("ZachScene");
    }

    [UnitySetUp] // this is all to get my scene setup and ready for tests. AI slop because it would not load otherwise
    public IEnumerator SetUp()
    {
        yield return new WaitForSeconds(1f); 

        player = GameObject.FindWithTag("Player");
        Assert.IsNotNull(player, "Player with tag 'Player' not found.");

        xpSystem = player.GetComponent<XP>();
        levelSystem = player.GetComponent<LevelSystem>();

        Assert.IsNotNull(xpSystem, "XP component not found on Player.");
        Assert.IsNotNull(levelSystem, "LevelSystem component not found on Player.");

        if (!player.TryGetComponent(out UpgradeSystem upgradeSystem))
        {
            upgradeSystem = player.AddComponent<UpgradeSystem>();
        }
        levelSystem.upgradeSystem = upgradeSystem;

        
        var xpSliderGO = GameObject.Find("XP Slider");
        if (xpSliderGO == null)
        {
            var canvas = new GameObject("Canvas", typeof(Canvas));
            xpSliderGO = new GameObject("XP Slider", typeof(Slider));
            xpSliderGO.transform.SetParent(canvas.transform);
        }

        var slider = xpSliderGO.GetComponent<Slider>();
        var xpBar = xpSliderGO.GetComponent<XPBar>() ?? xpSliderGO.AddComponent<XPBar>();
        xpBar.xpSlider = slider;
        xpBar.levelSystem = levelSystem;
        xpBar.xpSystem = xpSystem;

        levelSystem.xpSlider = slider;

        Debug.Log("Scene setup complete. Player found and systems connected.");
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
    public IEnumerator LevelPlayer_ZeroXP()
    {
        xpSystem.AddXP(-1);  // Add zero XP
        yield return null;

        Assert.AreEqual(-1, xpSystem.GetXP(), "XP should not increase when adding zero.");
    }

}
