using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;


public class DCStressTest
{
    private CoinManager coinManager;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject and attach CoinManager script
        GameObject obj = new GameObject();
        coinManager = obj.AddComponent<CoinManager>();
    }

    [UnityTest]
    public IEnumerator StressTest_SubtractCoins()
    {
        //Set coins to 10,000
        coinManager.coinsCount = 10000;
        Debug.Log("Starting Coins: " + coinManager.coinsCount);

        for (int i = 0; i < 2000; i++)
        {
            coinManager.SubtractCoins(10); // subtract 10 coins each time

            // Log every 1000 iterations
            if (i % 100 == 0)
            {
                Debug.Log("Coins Left: " + coinManager.coinsCount);
            }

            yield return null;
        }

        Debug.Log("Final Coins: " + coinManager.coinsCount);
        Assert.AreEqual(0, coinManager.coinsCount, "Failed as coins were not equal to 0");
    }
}
