/*Hurts skeleton to see if they die*/

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[TestFixture]
public class SkeletonHealthUpper
{
    private GameObject skeletonPrefab;
    private SkeletonScript skeletonScript;

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
    public IEnumerator SkeletonBoundaryStressTest()
    {
        GameObject skeleton = Object.Instantiate(skeletonPrefab, Vector3.zero, Quaternion.identity);
        skeletonScript = skeleton.GetComponent<SkeletonScript>();
        Assert.NotNull(skeletonScript, "Failed to get script");
        skeletonScript.OnTakeDamage(-100f);
        yield return null;
        Assert.IsNull(skeleton, "Didn't delete object");
    }
}
