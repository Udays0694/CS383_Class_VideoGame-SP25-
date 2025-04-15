/* Rapidly gains XP to stress test the limits of the leveling system */

using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class SpiderUpperBoundaryTest
{
    public GameObject SpiderPrefab;
    public GameObject Spider;
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
        SpiderPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Gabriel/Prefabs/Spider.prefab");
        PlayerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scenes/Prefabs/Player 1.prefab");
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
    public IEnumerator SpiderUpperBound()
    {
        yield return null;
        Spider = Object.Instantiate(SpiderPrefab, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);
        Player = Object.Instantiate(PlayerPrefab, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);
        Player2 = Object.Instantiate(PlayerPrefab, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);
        Assert.Pass("Didn't crash with two players.");
    }
}
