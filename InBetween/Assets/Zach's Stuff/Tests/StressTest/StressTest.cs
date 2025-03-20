/*Rapidly gains XP to stress test the limits of the leveling system*/

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class LevelSystemStressTest
{
    private GameObject player;
    private LevelSystem levelSystem;
    private XP xpSystem;

    [OneTimeSetUp]
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoomGenTest");
    }

    [SetUp]
    public void SetUp()
    {
        if (player != null)
        {
            GameObject.Destroy(player);
        }

        player = new GameObject("Player");
        xpSystem = player.AddComponent<XP>();
        levelSystem = player.AddComponent<LevelSystem>();
        levelSystem.xpSystem = xpSystem;
        Object.DontDestroyOnLoad(player);
    }

    [TearDown]
    public void TearDown()
    {
        if (player != null)
        {
            GameObject.Destroy(player);
        }
    }

    [UnityTest]
    public IEnumerator RapidLevelingWithExcessiveXP()
    {
        int maxXP = 1000000;
        int totalXP = 0;

        while (totalXP < maxXP)
        {
            levelSystem.GainXP(100);
            totalXP += 1000;
            yield return null;

            Assert.LessOrEqual(levelSystem.level, levelSystem.levelCap, "Player exceeded the level cap.");
        }

        Assert.AreEqual(levelSystem.level, 50, "Player did not reach the max level.");
    }
}
