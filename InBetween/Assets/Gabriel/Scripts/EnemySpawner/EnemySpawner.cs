using log4net.Util;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Misc
    public PlayerScript playerScript;
    public GameObject player = null;
    public Vector3 oldPlayerPosition;

    // Skeleton
    public GameObject SkeletonPrefab;
    public GameObject Skeleton;

    // Spider
    public GameObject SpiderPrefab;
    public GameObject Spider;

    // Zombie
    public GameObject ZombiePrefab;
    public GameObject Zombie;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerScript>();
        }
        else
        {
            Debug.LogWarning("No player found. The enemy will not be able to interact with the player.");
        }

        Invoke("QueueSkeletonSpawn", 30f);
        Invoke("QueueSpiderSpawn", 20f);
        Invoke("QueueZombieSpawn", 30f);
    }

    public void QueueSkeletonSpawn()
    {
        oldPlayerPosition = player.transform.position;
        oldPlayerPosition.z = -1f;
        Invoke("SpawnSkeleton", Random.Range(5f, 10f));
    }

    public void SpawnSkeleton()
    {
        Skeleton = Instantiate(SkeletonPrefab, oldPlayerPosition, Quaternion.identity);
        QueueSkeletonSpawn();
    }

    public void QueueSpiderSpawn()
    {
        oldPlayerPosition = player.transform.position;
        oldPlayerPosition.z = -1f;
        Invoke("SpawnSpider", Random.Range(2f, 5f));
    }

    public void SpawnSpider()
    {
        Spider = Instantiate(SpiderPrefab, oldPlayerPosition, Quaternion.identity);
        QueueSpiderSpawn();
    }

    public void QueueZombieSpawn()
    {
        oldPlayerPosition = player.transform.position;
        oldPlayerPosition.z = -1f;
        Invoke("SpawnZombie", Random.Range(2f, 5f));
    }

    public void SpawnZombie()
    {
        Spider = Instantiate(ZombiePrefab, oldPlayerPosition, Quaternion.identity);
        QueueZombieSpawn();
    }
}
