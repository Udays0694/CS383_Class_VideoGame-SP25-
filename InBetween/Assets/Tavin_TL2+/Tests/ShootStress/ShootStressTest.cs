using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class StressTest
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
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
        //script = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossController>();
		//Debug.Log("Looking for boss");		

        script = GameObject.FindAnyObjectByType<BossController>();
        Assert.NotNull(script, "Failed to get script");
        bool done = false;
        int counter = 0;
        
        while(!done && counter < 2500)
        {
            script.attack1();
            counter++;
            Debug.Log(counter);
            float fps = 1f / Time.deltaTime;
            Assert.GreaterOrEqual(fps, 10, "Took " + counter + " bullets to get below 10 fps");
        }
    }
}
