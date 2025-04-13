using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class RapidPlayerSpawnTest
{
    public GameObject playerPrefab;
    private int spawnCount;

    [SetUp]
    public void Setup()
    {
        spawnCount = 0;
    }

    [UnityTest]
    public IEnumerator TestRapidPlayerSpawn()
    {
        float startTime = Time.time;

        while (Time.time - startTime < 1f)
        {
            SpawnPlayer();
            yield return null;
        }

        Debug.Log($"Total Players Spawned in 1 second: {spawnCount}");

        Assert.Greater(spawnCount, 100, "More than 100 players should be spawned in 1 second.");
    }

    private void SpawnPlayer()
    {
        UnityEngine.Object.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        spawnCount++;
    }
}
