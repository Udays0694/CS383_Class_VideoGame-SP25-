/* Rapidly gains XP to stress test the limits of the leveling system */

using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class ZombieLowerHealthTest
{
    public GameObject ZombiePrefab;
    public GameObject Zombie;
    public ZombieScript zombieScript;

    [OneTimeSetUp]
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoomGenTest");
    }

    [SetUp]
    public void SetUp()
    {
        ZombiePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Gabriel/Prefabs/Zombie.prefab");
    }

    [TearDown]
    public void TearDown()
    {
        if (Zombie != null)
        {
            Object.Destroy(Zombie);
        }
    }

    [UnityTest]
    public IEnumerator ZombieHealthTest()
    {
        yield return null;

        Zombie = Object.Instantiate(
            ZombiePrefab,
            new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)),
            Quaternion.identity
        );

        zombieScript = Zombie.GetComponent<ZombieScript>();
        Assert.IsNotNull(zombieScript, "ZombieScript component not found on Zombie.");
        zombieScript.TakeDamage(10f);

        if (zombieScript.health == 10f)
        {
            Assert.Pass("Took correct amount of damage.");
        } else
        {
            Assert.Fail("Didn't take correct amount of damage.");
        }
        yield return null;
    }

}
