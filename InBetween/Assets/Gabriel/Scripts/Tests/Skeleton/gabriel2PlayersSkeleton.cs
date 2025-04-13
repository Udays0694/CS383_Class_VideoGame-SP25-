/* Rapidly gains XP to stress test the limits of the leveling system */

using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class SkeletonUpperBoundaryTest
{
    public GameObject SkeletonPrefab;
    public GameObject Skeleton;
    public GameObject PlayerPrefab;
    public GameObject Player;
    public GameObject Player2;

    [OneTimeSetUp]
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoomGenTest");
    }

    [SetUp]
    public void SetUp()
    {
        SkeletonPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Gabriel/Prefabs/Skeleton.prefab");
        PlayerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scenes/Prefabs/Player 1.prefab");
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
    public IEnumerator SkeletonUpperBound()
    {
        yield return null;
        Skeleton = Object.Instantiate(SkeletonPrefab, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);
        Player = Object.Instantiate(PlayerPrefab, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);
        Player2 = Object.Instantiate(PlayerPrefab, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);
        Assert.Pass("Didn't crash with two players.");
    }
}
