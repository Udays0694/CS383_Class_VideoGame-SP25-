using UnityEngine;

public class SpawnByChance : MonoBehaviour
{
    [System.Serializable]
    public class SpawnOption
    {
        public GameObject prefab;
        [Range(0f, 100f)]
        public float chancePercent = 100f;
    }

    [Header("Spawn Settings")]
    public SpawnOption[] spawnOptions;
    public Transform spawnPoint;

    private bool hasTriedSpawn = false;

    private void Awake()
    {
        TrySpawnOnce(); // Safe and won't repeat
    }

    public void TrySpawnOnce()
    {
        if (hasTriedSpawn) return;
        TrySpawn();
        hasTriedSpawn = true;
    }

    public void TrySpawn()
    {
        float roll = Random.Range(0f, 100f);
        float cumulative = 0f;

        foreach (var option in spawnOptions)
        {
            cumulative += option.chancePercent;

            if (roll <= cumulative && option.prefab != null)
            {
                // Determine where to spawn
                Transform parentTransform = spawnPoint ? spawnPoint : transform;
                GameObject obj = Instantiate(option.prefab, parentTransform.position, Quaternion.identity);

                // Optional: parent it for hierarchy organization
                obj.transform.SetParent(parentTransform, true); // true = keep world position

                break;
            }
        }
    }

}
