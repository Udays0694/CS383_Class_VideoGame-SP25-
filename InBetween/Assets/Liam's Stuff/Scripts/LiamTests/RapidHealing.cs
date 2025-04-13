using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class RapidHealTest : MonoBehaviour
{
    private float playerHealth;
    private AudioSource healSound;
    private GameObject healSoundObject;

    [SetUp]
    public void Setup()
    {
        playerHealth = 50f;
        GameObject healSoundObject = new GameObject("HealSound");
        healSound = healSoundObject.AddComponent<AudioSource>();
    }

    [UnityTest]
    public IEnumerator TestHealRapidly()
    {
        float initialHealth = playerHealth;

        float startTime = Time.time;
        while (Time.time - startTime < 1f)
        {
            Heal(10f);
            yield return null;
        }

        Assert.Greater(playerHealth, initialHealth, "Health should increase after healing.");

        if (playerHealth > initialHealth)
        {
            Assert.IsTrue(healSound.isPlaying, "Heal sound should be playing when health increases.");
        }
        else
        {
            Assert.IsFalse(healSound.isPlaying, "Heal sound should not be playing if health does not increase.");
        }
    }

    private void Heal(float healAmount)
    {
        playerHealth += healAmount;
        healSound.Play();
    }
}
