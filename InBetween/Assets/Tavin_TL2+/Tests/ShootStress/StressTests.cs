using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

// Boss fireball
public class StressTest
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
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
        //script = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossController>();
		//Debug.Log("Looking for boss");		

        script = GameObject.FindAnyObjectByType<BossController>();
        Assert.NotNull(script, "Failed to get script");
        bool done = false;
        int counter = 0;
        
        while(!done && counter < 1000)
        {
            script.attack1();
            counter++;
            Debug.Log(counter);
            yield return null;
            float fps = 1f / Time.deltaTime;
            Assert.GreaterOrEqual(fps, 10, "Took " + counter + " bullets to get below 10 fps");
        }
    }
}

// Bosses
public class SpawnStressTest
{
    [SerializeField] private GameObject boss;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }
    
    [SetUp]
    public void SetUp()
    {
    	boss = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Tavin_TL2+/Prefabs/Boss.prefab", typeof(GameObject));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
        bool done = false;
        int counter = 0;
        
        while(!done && counter < 600)
        {
            GameObject.Instantiate(boss);
            counter++;
            Debug.Log(counter);
            yield return null;
            float fps = 1f / Time.deltaTime;
            Assert.GreaterOrEqual(fps, 10, "Took " + counter + " bosses to get below 10 fps");
        }
        Assert.Pass("FPS did not drop below 10 with 600 bosses");
    }
}

// Imps
public class SpawnImpStress
{
    [SerializeField] private GameObject imp;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }
    
    [SetUp]
    public void SetUp()
    {
    	imp = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Tavin_TL2+/Prefabs/Imp.prefab", typeof(GameObject));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
        bool done = false;
        int counter = 0;
        
        while(!done && counter < 800)
        {
            GameObject.Instantiate(imp);
            counter++;
            Debug.Log(counter);
            yield return null;
            float fps = 1f / Time.deltaTime;
            Assert.GreaterOrEqual(fps, 10, "Took " + counter + " imps to get below 10 fps");
        }
        Assert.Pass("FPS did not drop below 10 with 800 imps");
    }
}

// Imp fireball
public class ImpFireballStress
{
    [SerializeField] private GameObject impFire;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }
    
    [SetUp]
    public void SetUp()
    {
    	impFire = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Tavin_TL2+/Prefabs/ImpFireball.prefab", typeof(GameObject));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
        bool done = false;
        int counter = 0;
        
        while(!done && counter < 1000)
        {
            GameObject.Instantiate(impFire);
            counter++;
            Debug.Log(counter);
            yield return null;
            float fps = 1f / Time.deltaTime;
            Assert.GreaterOrEqual(fps, 10, "Took " + counter + " imp fireballs to get below 10 fps");
        }
        Assert.Pass("FPS did not drop below 10 with 1000 imp fireballs");
    }
}

// Fire
public class SpawnFireStress
{
    [SerializeField] private GameObject fire;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }
    
    [SetUp]
    public void SetUp()
    {
    	fire = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Tavin_TL2+/Prefabs/Fire.prefab", typeof(GameObject));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
        bool done = false;
        int counter = 0;
        
        while(!done && counter < 1000)
        {
            GameObject.Instantiate(fire);
            counter++;
            Debug.Log(counter);
            yield return null;
            float fps = 1f / Time.deltaTime;
            Assert.GreaterOrEqual(fps, 10, "Took " + counter + " fire to get below 10 fps");
        }
        Assert.Pass("FPS did not drop below 10 with 1000 fire");
    }
}

// Spikes
public class SpawnSpikeStress
{
	[SerializeField] private GameObject spike;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossTestRoom");
    }
    
    [SetUp]
    public void SetUp()
    {
    	spike = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Tavin_TL2+/Prefabs/Spike.prefab", typeof(GameObject));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShootLoadsStressTest()
    {
    	yield return null;
    	
        bool done = false;
        int counter = 0;
        
        while(!done && counter < 1000)
        {
            GameObject.Instantiate(spike);
            counter++;
            Debug.Log(counter);
            yield return null;
            float fps = 1f / Time.deltaTime;
            Assert.GreaterOrEqual(fps, 10, "Took " + counter + " spikes to get below 10 fps");
        }
        Assert.Pass("FPS did not drop below 10 with 1000 spikes");
    }
}


