/* Rapidly gains XP to stress test the limits of the leveling system */

using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class SkeletonLowerBoundaryTest
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
    public IEnumerator SkeletonLowerBound()
    {
        yield return null;
        Skeleton = Object.Instantiate(SkeletonPrefab, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);
        Assert.Pass("Didn't crash without player.");
    }
}
