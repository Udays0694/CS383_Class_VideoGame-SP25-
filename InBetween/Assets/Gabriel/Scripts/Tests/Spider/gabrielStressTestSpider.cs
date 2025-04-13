/* Rapidly gains XP to stress test the limits of the leveling system */

using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class SpiderStressTest
{
    public GameObject SpiderPrefab;
    public GameObject Spider;

    [OneTimeSetUp]
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoomGenTest");
    }

    [SetUp]
    public void SetUp()
    {
        SpiderPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Gabriel/Prefabs/Spider.prefab");
    }

    [TearDown]
    public void TearDown()
    {
        if (Spider != null)
        {
            Object.Destroy(Spider);
        }
    }

    [UnityTest]
    public IEnumerator SpiderQuantityStressTest()
    {
        int maxSpiders = 500;
        int currentSpiders = 0;
        float fps = 0;

        // Variable to track if FPS ever drops below 10
        bool fpsDroppedBelow10 = false;

        while (currentSpiders < maxSpiders)
        {
            Spider = Object.Instantiate(SpiderPrefab, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);
            currentSpiders++;

            fps = 1f / Time.deltaTime;

            if (fps < 10)
            {
                fpsDroppedBelow10 = true;
                break;
            }

            yield return null;
        }

        if (fpsDroppedBelow10)
        {
            Assert.Fail("FPS dropped below 10 FPS.");
        }

        Assert.Pass("FPS remained above 10 FPS throughout the test.");
    }
}
