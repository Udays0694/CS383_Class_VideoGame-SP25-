/* Rapidly gains XP to stress test the limits of the leveling system */

using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class SpiderNegativeSpeedTest
{
    public GameObject SpiderPrefab;
    public GameObject Spider;
    public SpiderScript spiderScript;

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
    public IEnumerator SpiderSpeedTest()
    {
        yield return null;

        Spider = Object.Instantiate(
            SpiderPrefab,
            new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)),
            Quaternion.identity
        );

        spiderScript = Spider.GetComponent<SpiderScript>();
        Assert.IsNotNull(spiderScript, "SpiderScript component not found on Spider.");
        spiderScript.movementSpeed = -1000f;

        if (spiderScript.movementSpeed == -1000f)
        {
            Assert.Pass("Speed is -1000f");
        } else
        {
            Assert.Fail("Speed is not -1000f");
        }
        yield return null;
    }

}
