using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SpawnStressTest
{
    [SerializeField] private GameObject boss;

    // Load scene
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("BossFight");
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
            float fps = 1f / Time.deltaTime;
            Assert.GreaterOrEqual(fps, 10, "Took " + counter + " bosses to get below 10 fps");
        }
        Assert.Pass("FPS did not drop below 10 with 600 bosses");
    }
}
