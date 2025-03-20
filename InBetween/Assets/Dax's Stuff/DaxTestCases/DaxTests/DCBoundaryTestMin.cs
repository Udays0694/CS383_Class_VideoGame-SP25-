using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class DCBoundaryTestMin
{
    private CoinManager coinManager;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject and attach CoinManager script
        GameObject obj = new GameObject();
        coinManager = obj.AddComponent<CoinManager>();
    }

    [Test]
    public void SubtractCoins_NoNegative()
    {
        // Set coins to 10
        coinManager.coinsCount = 10;
        Debug.Log("Starting Coins: " + coinManager.coinsCount);

        // First subtraction: 10 - 10 = 0
        coinManager.SubtractCoins(10);
        Debug.Log("After subtracting 10: " + coinManager.coinsCount);
        Assert.AreEqual(0, coinManager.coinsCount, "First subtraction failed: Coins should be 0!");

        // Second subtraction: 0 - 10 = should still be 0
        coinManager.SubtractCoins(10);
        Debug.Log("After subtracting 10 Again: " + coinManager.coinsCount);
        Assert.AreEqual(0, coinManager.coinsCount, "Second subtraction failed: Coins should not go below 0!");
    
    }

    [Test]

    public void AddCoins_Max9999999()
    {
        // Set coins to one less than the max 
        coinManager.coinsCount = 9999998;
        Debug.Log("Starting Coins: " + coinManager.coinsCount);

        //Add 1 coin
        coinManager.AddCoins(1);
        Debug.Log("After Adding 1: " + coinManager.coinsCount);
        Assert.AreEqual(9999999, coinManager.coinsCount, "First addition failed: Coins should be 9999999");

        //Add 1 more coin 
        coinManager.AddCoins(1);
        Debug.Log("After Adding 2: " + coinManager.coinsCount);
        Assert.AreEqual(9999999, coinManager.coinsCount, "Second Addition failed: Coins should be 9999999");


    }
}
