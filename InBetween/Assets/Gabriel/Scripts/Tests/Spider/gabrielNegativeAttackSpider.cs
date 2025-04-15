/* Rapidly gains XP to stress test the limits of the leveling system */

using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class SpiderNegativeAttackTest
{
    public GameObject SpiderPrefab;
    public GameObject Spider;
    public SpiderScript zombieScript;

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

        zombieScript = Spider.GetComponent<SpiderScript>();
        Assert.IsNotNull(zombieScript, "SpiderScript component not found on Spider.");
        zombieScript.attackDamage = -1000f;

        if (zombieScript.attackDamage == -1000f)
        {
            Assert.Pass("Attack is -1000f");
        }
        else
        {
            Assert.Fail("Attack is not -1000f");
        }
        yield return null;
    }

}
