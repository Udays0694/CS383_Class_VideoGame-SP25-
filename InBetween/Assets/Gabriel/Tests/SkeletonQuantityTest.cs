using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[TestFixture]
public class SkeletonQuantityTest
{
    private GameObject skeletonPrefab;

    [OneTimeSetUp]
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoomGenTest");
    }

    [SetUp]
    public void SetUp()
    {
        skeletonPrefab = Resources.Load<GameObject>("Assets/Gabriel/Prefabs/Skeleton");
    }

    [TearDown]
    public void TearDown()
    {
        if (skeletonPrefab != null)
        {
            GameObject.Destroy(skeletonPrefab);
        }
    }

    [UnityTest]
    public IEnumerator SkeletonQuantityStressTest()
    {
        int maxSkeletons = 1000000;
        int currentSkeletons = 0;
        int counter = 0;
        float fps = 0;

        while (currentSkeletons < maxSkeletons)
        {
            GameObject skeleton = Object.Instantiate(skeletonPrefab, Vector3.zero, Quaternion.identity);
            yield return null;
            counter++;
            Debug.Log(counter);
            fps = 1f / Time.deltaTime;
            Assert.GreaterOrEqual(fps, 10, "Took " + counter + " to get below 10 fps");
        }
        Assert.LessOrEqual(fps, 10, "Didn't drop below 10 fps");
    }
}
