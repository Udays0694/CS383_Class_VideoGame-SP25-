/* Rapidly gains XP to stress test the limits of the leveling system */

using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class SkeletonStressTest
{
    public GameObject SkeletonPrefab;
    public GameObject Skeleton;

    [OneTimeSetUp]
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoomGenTest");
    }

    [SetUp]
    public void SetUp()
    {
        SkeletonPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Gabriel/Prefabs/Skeleton.prefab");
    }

    [TearDown]
    public void TearDown()
    {
        if (Skeleton != null)
        {
            Object.Destroy(Skeleton);
        }
    }

    [UnityTest]
    public IEnumerator SkeletonQuantityStressTest()
    {
        int maxSkeletons = 500;
        int currentSkeletons = 0;
        float fps = 0;

        // Variable to track if FPS ever drops below 10
        bool fpsDroppedBelow10 = false;

        while (currentSkeletons < maxSkeletons)
        {
            Skeleton = Object.Instantiate(SkeletonPrefab, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);
            currentSkeletons++;

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
