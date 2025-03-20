using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BoundTestLower
{
	private GameObject boss;
    private BossController script;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossFight");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator HealthBoundLower()
    {
    	yield return null;		

        script = GameObject.FindAnyObjectByType<BossController>();
        Assert.NotNull(script, "Failed to get script");
        
        script.takeDamage(11f);
        yield return null;
        Assert.IsNull(boss, "Didn't delete object");
    }
}
