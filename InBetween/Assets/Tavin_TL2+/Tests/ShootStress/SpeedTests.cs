using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

/********/
/* BOSS */
/********/

// Bottom wall
public class BossSpeedTestDown
{
    private GameObject boss;
    private BossController bossScript;
    private GameObject player;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
    	// Get objects/components
    	boss = GameObject.FindGameObjectWithTag("Boss");
    	player = GameObject.FindGameObjectWithTag("Player");
    	Assert.NotNull(boss, "Could not get boss");
    	Assert.NotNull(player, "Could not get player");
    	
    	// Move to proper locations
    	player.transform.position = new Vector3(0, -100, 0);
    	boss.transform.position = new Vector3(0, -3.5f, 0);
    	
    	bossScript = boss.GetComponent<BossController>();
    	Assert.NotNull(bossScript, "Could not get boss script");
        bossScript.speed = 0.5f;
        
        while(bossScript.speed < 100)
        {
            bossScript.speed += 0.5f;
            Debug.Log(bossScript.speed);
            yield return null;
            if(boss.transform.position.y < -12)
            {
            	Assert.Fail("Took " + bossScript.speed + " speed to get through the bottom wall");
            }
        }
        Assert.Pass("Boss did not clip through the bottom wall with 100 speed");
    }
}

// Left wall
public class BossSpeedTestLeft
{
    private GameObject boss;
    private BossController bossScript;
    private GameObject player;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
    	// Get objects/components
    	boss = GameObject.FindGameObjectWithTag("Boss");
    	player = GameObject.FindGameObjectWithTag("Player");
    	Assert.NotNull(boss, "Could not get boss");
    	Assert.NotNull(player, "Could not get player");
    	
    	// Move to proper locations
    	player.transform.position = new Vector3(-100, 0, 0);
    	boss.transform.position = new Vector3(-8, 0, 0);
    	
    	bossScript = boss.GetComponent<BossController>();
    	Assert.NotNull(bossScript, "Could not get boss script");
        bossScript.speed = 0.5f;
        
        while(bossScript.speed < 100)
        {
            bossScript.speed += 0.5f;
            Debug.Log(bossScript.speed);
            yield return null;
            if(boss.transform.position.x < -16)
            {
            	Assert.Fail("Took " + bossScript.speed + " speed to get through the left wall");
            }
        }
        Assert.Pass("Boss did not clip through the left wall with 100 speed");
    }
}

// Top wall
public class BossSpeedTestUp
{
    private GameObject boss;
    private BossController bossScript;
    private GameObject player;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
    	// Get objects/components
    	boss = GameObject.FindGameObjectWithTag("Boss");
    	player = GameObject.FindGameObjectWithTag("Player");
    	Assert.NotNull(boss, "Could not get boss");
    	Assert.NotNull(player, "Could not get player");
    	
    	// Move to proper locations
    	player.transform.position = new Vector3(0, 100, 0);
    	boss.transform.position = new Vector3(0, 4, 0);
    	
    	bossScript = boss.GetComponent<BossController>();
    	Assert.NotNull(bossScript, "Could not get boss script");
        bossScript.speed = 0.5f;
        
        while(bossScript.speed < 100)
        {
            bossScript.speed += 0.5f;
            Debug.Log(bossScript.speed);
            yield return null;
            if(boss.transform.position.y > 11)
            {
            	Assert.Fail("Took " + bossScript.speed + " speed to get through the left wall");
            }
        }
        Assert.Pass("Boss did not clip through the left wall with 100 speed");
    }
}

// Top wall
public class BossSpeedTestRight	
{
    private GameObject boss;
    private BossController bossScript;
    private GameObject player;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
    	// Get objects/components
    	boss = GameObject.FindGameObjectWithTag("Boss");
    	player = GameObject.FindGameObjectWithTag("Player");
    	Assert.NotNull(boss, "Could not get boss");
    	Assert.NotNull(player, "Could not get player");
    	
    	// Move to proper locations
    	player.transform.position = new Vector3(100, 0, 0);
    	boss.transform.position = new Vector3(4.5f, 0, 0);
    	
    	bossScript = boss.GetComponent<BossController>();
    	Assert.NotNull(bossScript, "Could not get boss script");
        bossScript.speed = 0.5f;
        
        while(bossScript.speed < 100)
        {
            bossScript.speed += 0.5f;
            Debug.Log(bossScript.speed);
            yield return null;
            if(boss.transform.position.x > 12)
            {
            	Assert.Fail("Took " + bossScript.speed + " speed to get through the left wall");
            }
        }
        Assert.Pass("Boss did not clip through the left wall with 100 speed");
    }
}

/*******/
/* IMP */
/*******/

// Bottom wall
public class ImpSpeedTestDown
{
    private GameObject boss;
    private ImpController bossScript;
    private GameObject player;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
    	// Get objects/components
    	boss = GameObject.FindGameObjectWithTag("Imp");
    	player = GameObject.FindGameObjectWithTag("Player");
    	Assert.NotNull(boss, "Could not get imp");
    	Assert.NotNull(player, "Could not get player");
    	
    	// Move to proper locations
    	player.transform.position = new Vector3(0, -100, 0);
    	boss.transform.position = new Vector3(0, -3.5f, 0);
    	
    	bossScript = boss.GetComponent<ImpController>();
    	Assert.NotNull(bossScript, "Could not get script");
        bossScript.speed = 0.5f;
        
        while(bossScript.speed < 100)
        {
            bossScript.speed += 0.5f;
            Debug.Log(bossScript.speed);
            yield return null;
            if(boss.transform.position.y < -12)
            {
            	Assert.Fail("Took " + bossScript.speed + " speed to get through the bottom wall");
            }
        }
        Assert.Pass("Imp did not clip through the bottom wall with 100 speed");
    }
}

// Left wall
public class ImpSpeedTestLeft
{
    private GameObject boss;
    private ImpController bossScript;
    private GameObject player;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
    	// Get objects/components
    	boss = GameObject.FindGameObjectWithTag("Imp");
    	player = GameObject.FindGameObjectWithTag("Player");
    	Assert.NotNull(boss, "Could not get imp");
    	Assert.NotNull(player, "Could not get player");
    	
    	// Move to proper locations
    	player.transform.position = new Vector3(-100, 0, 0);
    	boss.transform.position = new Vector3(-8, 0, 0);
    	
    	bossScript = boss.GetComponent<ImpController>();
    	Assert.NotNull(bossScript, "Could not get imp script");
        bossScript.speed = 0.5f;
        
        while(bossScript.speed < 100)
        {
            bossScript.speed += 0.5f;
            Debug.Log(bossScript.speed);
            yield return null;
            if(boss.transform.position.x < -16)
            {
            	Assert.Fail("Took " + bossScript.speed + " speed to get through the left wall");
            }
        }
        Assert.Pass("Imp did not clip through the left wall with 100 speed");
    }
}

// Top wall
public class ImpSpeedTestUp
{
    private GameObject boss;
    private ImpController bossScript;
    private GameObject player;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
    	// Get objects/components
    	boss = GameObject.FindGameObjectWithTag("Imp");
    	player = GameObject.FindGameObjectWithTag("Player");
    	Assert.NotNull(boss, "Could not get imp");
    	Assert.NotNull(player, "Could not get player");
    	
    	// Move to proper locations
    	player.transform.position = new Vector3(0, 100, 0);
    	boss.transform.position = new Vector3(0, 4, 0);
    	
    	bossScript = boss.GetComponent<ImpController>();
    	Assert.NotNull(bossScript, "Could not get script");
        bossScript.speed = 0.5f;
        
        while(bossScript.speed < 100)
        {
            bossScript.speed += 0.5f;
            Debug.Log(bossScript.speed);
            yield return null;
            if(boss.transform.position.y > 11)
            {
            	Assert.Fail("Took " + bossScript.speed + " speed to get through the left wall");
            }
        }
        Assert.Pass("Imp did not clip through the left wall with 100 speed");
    }
}

// Top wall
public class ImpSpeedTestRight	
{
    private GameObject boss;
    private ImpController bossScript;
    private GameObject player;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
    	// Get objects/components
    	boss = GameObject.FindGameObjectWithTag("Imp");
    	player = GameObject.FindGameObjectWithTag("Player");
    	Assert.NotNull(boss, "Could not get imp");
    	Assert.NotNull(player, "Could not get player");
    	
    	// Move to proper locations
    	player.transform.position = new Vector3(100, 0, 0);
    	boss.transform.position = new Vector3(4.5f, 0, 0);
    	
    	bossScript = boss.GetComponent<ImpController>();
    	Assert.NotNull(bossScript, "Could not get script");
        bossScript.speed = 0.5f;
        
        while(bossScript.speed < 100)
        {
            bossScript.speed += 0.5f;
            Debug.Log(bossScript.speed);
            yield return null;
            if(boss.transform.position.x > 12)
            {
            	Assert.Fail("Took " + bossScript.speed + " speed to get through the left wall");
            }
        }
        Assert.Pass("Imp did not clip through the left wall with 100 speed");
    }
}

