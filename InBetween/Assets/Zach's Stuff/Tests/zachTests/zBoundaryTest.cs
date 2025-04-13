/*boundary test 1 puts the player at max level and attempts to bypass the max level
boundary test 2 puts the player at level -1 and tries to gain xp

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class BoundaryTest
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoomGenTest");
    }

    private GameObject player;
    private LevelSystem levelSystem;
    private XP xpSystem; 

    [SetUp]
    public void SetUp()
    {
        player = new GameObject("Player");

        xpSystem = player.AddComponent<XP>();
        levelSystem = player.AddComponent<LevelSystem>();

        levelSystem.xpSystem = xpSystem;

        levelSystem.level = 1;
        xpSystem.AddXP(0);

        if (levelSystem == null)
        {
            Debug.LogError("LevelSystem component is missing after setup!");
        }
        else
        {
            Debug.Log("LevelSystem initialized properly.");
        }
    }

    [UnityTest]
    public IEnumerator PlayerCannotExceedLevelCap()
    {
        Debug.Log("Starting PlayerCannotExceedLevelCap test...");

        levelSystem.level = 50;

        levelSystem.GainXP(100);

        Debug.Log($"Player Level after GainXP: {levelSystem.level}");

        Assert.LessOrEqual(levelSystem.level, levelSystem.levelCap, "Player exceeded the level cap.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerCannotHaveNegativeLevel()
    {
        Debug.Log("Starting PlayerCannotHaveNegativeLevel test...");

        levelSystem.level = -1;

        Debug.Log("Player level set to -1");

        levelSystem.GainXP(100);

        Debug.Log($"Player Level after GainXP: {levelSystem.level}");

        Assert.GreaterOrEqual(levelSystem.level, 1, "Player has a negative level.");

        yield return null;
    }
}
*/
