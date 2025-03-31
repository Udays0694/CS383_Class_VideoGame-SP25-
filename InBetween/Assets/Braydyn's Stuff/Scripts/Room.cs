using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] furniturePrefabs;

    private FurnitureSpawnPoint[] spawnPoints;

    private void Start()
    {
        spawnPoints = GetComponentsInChildren<FurnitureSpawnPoint>();

        if (spawnPoints.Length > 0)
        {
            Debug.Log("Room has furniture spawn points.");
            GenerateFurniture();
        }
        else
        {
            Debug.Log("Room has no furniture points.");
        }
    }

    private void GenerateFurniture()
    {
        if (furniturePrefabs == null || furniturePrefabs.Length == 0)
        {
            Debug.LogWarning("No furniture prefabs assigned to room.");
            return;
        }

        foreach (FurnitureSpawnPoint point in spawnPoints)
        {
            int randomIndex = Random.Range(0, furniturePrefabs.Length);
            Instantiate(furniturePrefabs[randomIndex], point.transform.position, point.transform.rotation, transform);
        }
    }

    public FurnitureSpawnPoint[] GetFurniturePoints() => spawnPoints;
}
