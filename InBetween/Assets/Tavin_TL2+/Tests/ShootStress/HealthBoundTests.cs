using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

/********/
/* BOSS */
/********/

public class BoundTestLower
{
	private GameObject boss;
    private BossController script;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
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

public class BoundTestUpper
{
	private GameObject boss;
    private BossController script;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator HealthBoundUpper()
    {
    	yield return null;

        script = GameObject.FindAnyObjectByType<BossController>();
        Assert.NotNull(script, "Failed to get script");
        
        float maxHealth = script.health;
        script.takeDamage(-1000f);
        yield return null;
        if(script.health > maxHealth)
        {
        	Assert.Fail("Negative damage causes an overfill of the healthbar");
    	}
    	else
    	{
    		Assert.Pass("Health is capped at " + maxHealth);
    	}
    }
}

/*******/
/* IMP */
/*******/

public class ImpHealthUpper : MonoBehaviour
{
    private GameObject imp;
    private ImpController script;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator HealthBoundUpper()
    {
    	yield return null;		

		imp = Instantiate((GameObject)
			  AssetDatabase.LoadAssetAtPath("Assets/Tavin_TL2+/Prefabs/Imp.prefab",
			  								typeof(GameObject)));
		
        script = imp.GetComponent<ImpController>();
        Assert.NotNull(script, "Failed to get script");
        
        float maxHealth = script.health;
        script.takeDamage(-1000f);
        yield return null;
        if(script.health > maxHealth)
        {
        	Assert.Fail("Negative damage causes an overfill of the healthbar");
    	}
    	else
    	{
    		Assert.Pass("Health is capped at " + maxHealth);
    	}
    }
}

public class ImpHealthLower : MonoBehaviour
{
    private GameObject imp;
    private ImpController script;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator HealthBoundUpper()
    {
    	yield return null;		

		imp = Instantiate((GameObject)
			  AssetDatabase.LoadAssetAtPath("Assets/Tavin_TL2+/Prefabs/Imp.prefab",
			  								typeof(GameObject)));
		yield return null;
        script = imp.GetComponent<ImpController>();
        Assert.NotNull(script, "Failed to get script");
        
        script.takeDamage(1000f);
        yield return null;
        if(imp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
        	Assert.Fail("Imp is not killed with negative health");
        }
        Assert.Pass("Negative health results in the death of the imp.");
    }
}

