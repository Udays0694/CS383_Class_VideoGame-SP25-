/* Rapidly gains XP to stress test the limits of the leveling system */

using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class ZombieStressTest
{
    public GameObject ZombiePrefab;
    public GameObject Zombie;

    [OneTimeSetUp]
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoomGenTest");
    }

    [SetUp]
    public void SetUp()
    {
        ZombiePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Gabriel/Prefabs/Zombie.prefab");
    }

    [TearDown]
    public void TearDown()
    {
        if (Zombie != null)
        {
            Object.Destroy(Zombie);
        }
    }

    [UnityTest]
    public IEnumerator ZombieQuantityStressTest()
    {
        int maxZombies = 500;
        int currentZombies = 0;
        float fps = 0;

        // Variable to track if FPS ever drops below 10
        bool fpsDroppedBelow10 = false;

        while (currentZombies < maxZombies)
        {
            Zombie = Object.Instantiate(ZombiePrefab, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);
            currentZombies++;

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
