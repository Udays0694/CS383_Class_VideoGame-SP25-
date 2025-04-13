using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SpawnSpikeStress
{
	[SerializeField] private GameObject spike;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossFight");
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
            float fps = 1f / Time.deltaTime;
            Assert.GreaterOrEqual(fps, 10, "Took " + counter + " spikes to get below 10 fps");
        }
        Assert.Pass("FPS did not drop below 10 with 1000 spikes");
    }
}
