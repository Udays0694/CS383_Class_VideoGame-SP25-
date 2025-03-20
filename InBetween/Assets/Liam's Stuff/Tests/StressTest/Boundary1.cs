using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HealthTest
{
    private GameObject player;
    public Health health;

    [SetUp]
    public void Setup()
    {
        player = new GameObject();
        health = player.AddComponent<Health>();
        health.currentHealth = health.MaxHealth;
    }

    [UnityTest]
    public IEnumerator TestHealthDepletesToZeroAndLoadsScene()
    {
        health.currentHealth = 1;
        health.TakeDamage(1);

        yield return null;

        Assert.IsTrue(SceneManager.GetSceneByName("GameOver").isLoaded);
    }
}
