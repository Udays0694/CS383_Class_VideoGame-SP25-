using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class RapidDamageTest : MonoBehaviour
{
    private float playerHealth;
    private AudioSource damageSound;
    private GameObject damageSoundObject;
    private bool isDead;

    [SetUp]
    public void Setup()
    {
        playerHealth = 100f;
        isDead = false;
    }

    [UnityTest]
    public IEnumerator TestTakeDamageRapidly()
    {
        float initialHealth = playerHealth;
        

        float startTime = Time.time;
        while (Time.time - startTime < 1f)
        {
            TakeDamage(10f);
            yield return null;
        }

        Assert.Less(playerHealth, initialHealth, "Health should decrease after taking damage.");

        if (playerHealth <= 0)
        {
            Assert.IsTrue(isDead, "The player should be dead when health reaches 0 or below.");
            Assert.IsTrue(damageSound.isPlaying, "Damage sound should be playing when health reaches 0.");
        }
        else
        {
            Assert.IsFalse(damageSound.isPlaying, "Damage sound should not be playing if health is above 0.");
        }
    }

    private void TakeDamage(float damage)
    {
        playerHealth -= damage;
        damageSound.Play();
        if (playerHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        Debug.Log("Player has died.");
    }
}
