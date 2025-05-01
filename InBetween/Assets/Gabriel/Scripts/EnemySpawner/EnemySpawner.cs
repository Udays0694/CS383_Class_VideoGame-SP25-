using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Misc
    public PlayerScript playerScript;
    public GameObject player = null;
    public Vector3 oldPlayerPosition;

    // Skeleton
    public GameObject SkeletonPrefab;

    // Spider
    public GameObject SpiderPrefab;

    // Zombie
    public GameObject ZombiePrefab;

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
        if (Vector3.Distance(player.transform.position, oldPlayerPosition) >= 3f)
        {
            Instantiate(SkeletonPrefab, oldPlayerPosition, Quaternion.identity);
        }
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
        if (Vector3.Distance(player.transform.position, oldPlayerPosition) >= 3f)
        {
            Instantiate(SpiderPrefab, oldPlayerPosition, Quaternion.identity);
        }
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
        if (Vector3.Distance(player.transform.position, oldPlayerPosition) >= 3f)
        {
            Instantiate(ZombiePrefab, oldPlayerPosition, Quaternion.identity);
        }
        QueueZombieSpawn();
    }
}
