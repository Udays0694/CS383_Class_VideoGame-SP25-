/* Rapidly gains XP to stress test the limits of the leveling system */

using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class SpiderLowerHealthTest
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
    public IEnumerator SpiderHealthTest()
    {
        yield return null;

        Spider = Object.Instantiate(
            SpiderPrefab,
            new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)),
            Quaternion.identity
        );

        spiderScript = Spider.GetComponent<SpiderScript>();
        Assert.IsNotNull(spiderScript, "SpiderScript component not found on Spider.");
        spiderScript.TakeDamage(10f);

        if (spiderScript.health == 30f)
        {
            Assert.Pass("Took correct amount of damage.");
        } else
        {
            Assert.Fail("Didn't take correct amount of damage.");
        }
        yield return null;
    }

}
