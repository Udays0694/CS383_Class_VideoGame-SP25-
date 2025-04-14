using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


public class DCSpawnShopKeeperTest
{
    private GameObject testObject;
    private GameObject shopkeeperPrefab;
    private GameObject shopkeeperInstance;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        testObject = new GameObject("TestObject");
        testObject.SetActive(false);

        shopkeeperPrefab = new GameObject("ShopkeeperPrefab");
        shopkeeperPrefab.AddComponent<ShopKeeperSpawnMarker>();

        yield return null;
    }

    [UnityTest] //First test, used to ensure the shopKeeper can spawn properly. 
    public IEnumerator ShopKeeperSpawnOneTime()
    {
        testObject.SetActive(true);
        yield return null;
        Assert.IsTrue(testObject.activeSelf, "SpawnKeeper should be active after calling SetActive(true).");
    }

    [UnityTest] //Test trying to spawn the ShopKeeper Multiple times
    public IEnumerator ShopKeeperSpawnedTenTimes()
    {
        for (int i = 0; i < 10; i++)
        {
            TrySpawnShopKeeper();
            yield return null;
        }

        var shopkeepers = GameObject.FindObjectsOfType<ShopKeeperSpawnMarker>();
        Assert.AreEqual(1, shopkeepers.Length, "Only one Shopeeper should exist in the scene.");
    }

    [UnityTest] //ShopKeeper spawned and despawned repeatedly
    public IEnumerator ShopKeeperSpawnAndDespawn()
    {
        for (int i = 0; i < 5; i++)
        {
            TrySpawnShopKeeper();
            yield return null;

            Assert.IsNotNull(shopkeeperInstance);
            Assert.IsTrue(shopkeeperInstance.activeSelf, "Shopkeeper should be active after spawning.");

            shopkeeperInstance.SetActive(false); //Despawn
            yield return null;

            Assert.IsFalse(shopkeeperInstance.activeSelf, "Shopkeeper should be inactive after despawning.");
        }

        //Should have only spawned one ShopKeeper
        var shopkeepers = GameObject.FindObjectsOfType<ShopKeeperSpawnMarker>();
        Assert.AreEqual(1, shopkeepers.Length, "Only one ShopKeeper at a time and at end.");
    }

    private void TrySpawnShopKeeper()
    {
        if (shopkeeperInstance == null)
        {
            shopkeeperInstance = Object.Instantiate(shopkeeperPrefab);
            shopkeeperInstance.name = "ShopKeeper";
        }

        shopkeeperInstance.SetActive(true);
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(testObject);
        Object.Destroy(shopkeeperPrefab);
        if (shopkeeperInstance != null)
            Object.Destroy(shopkeeperInstance);
        yield return null;
    }
    private class ShopKeeperSpawnMarker : MonoBehaviour { }
}


