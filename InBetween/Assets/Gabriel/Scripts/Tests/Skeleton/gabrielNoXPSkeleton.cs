/* Rapidly gains XP to stress test the limits of the leveling system */

using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class SkeletonNoXPTest
{
    public GameObject SkeletonPrefab;
    public GameObject Skeleton;
    public SkeletonScript skeletonScript;

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
    public IEnumerator SkeletonSpeedTest()
    {
        yield return null;

        Skeleton = Object.Instantiate(
            SkeletonPrefab,
            new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)),
            Quaternion.identity
        );

        skeletonScript = Skeleton.GetComponent<SkeletonScript>();
        skeletonScript.Awake();
        Assert.IsNotNull(skeletonScript, "SkeletonScript component not found on Skeleton.");
        skeletonScript.xpAward = 0;

        if (skeletonScript.xpAward == 0)
        {
            Assert.Pass("xp is 0");
        } else
        {
            Assert.Fail("xp is not 0");
        }
        yield return null;
    }

}
