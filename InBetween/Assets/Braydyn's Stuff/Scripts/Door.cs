using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DoorDirection
{
    North,
    South,
    East,
    West
}

public class Door : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject corridorPrefab;
    [SerializeField] private GameObject brokenCorridorPrefab;
    [SerializeField] private RoomGenerator roomGenerator;
    [SerializeField] private DoorDirection direction;

    private static HashSet<Vector3> openedDoors = new HashSet<Vector3>();
    private bool playerNearby = false;

    private void Start()
    {
        if (roomGenerator == null)
            roomGenerator = GameObject.FindAnyObjectByType<RoomGenerator>();

        if (openedDoors.Contains(transform.position))
            gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!openedDoors.Contains(transform.position))
            {
                StartCoroutine(SpawnCorridorAndRoom());
            }
        }
    }

    private IEnumerator SpawnCorridorAndRoom()
    {
        if (roomGenerator == null || spawnPoint == null || corridorPrefab == null)
        {
            Debug.LogWarning("Missing references for spawning corridor and room.");
            yield break;
        }

        // Spawn corridor
        GameObject corridor = Instantiate(corridorPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform endPoint = corridor.transform.Find("CorridorEndPoint");

        if (endPoint == null)
        {
            Debug.LogError("Corridor prefab must have a 'CorridorEndPoint' child.");
            Destroy(corridor);
            yield break;
        }

        yield return null;

        // Overlap check
        Vector3 roomSpawnPos = endPoint.position;
        Collider2D[] hits = Physics2D.OverlapBoxAll(roomSpawnPos, roomGenerator.RoomBoundsSize, 0f);
        bool blocked = false;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Room") || hit.CompareTag("Corridor"))
            {
                blocked = true;
                break;
            }
        }

        if (blocked)
        {
            Debug.Log("Spawn blocked — replacing with broken corridor.");
            Destroy(corridor);

            if (brokenCorridorPrefab != null)
                Instantiate(brokenCorridorPrefab, spawnPoint.position, spawnPoint.rotation);

            openedDoors.Add(transform.position);
            gameObject.SetActive(false);

            // Disable opposite door in blocking room
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Room"))
                {
                    Room blockingRoom = hit.GetComponent<Room>();
                    if (blockingRoom != null)
                    {
                        DoorDirection opposite = GetOppositeDirection(direction);
                        Door[] doors = blockingRoom.GetComponentsInChildren<Door>();

                        foreach (Door d in doors)
                        {
                            if (d.direction == opposite)
                            {
                                d.enabled = false;
                                openedDoors.Add(d.transform.position);
                                Debug.Log("Disabled matching opposite door in blocking room.");
                                break;
                            }
                        }

                        break;
                    }
                }
            }

            yield break;
        }

        // Success case: spawn room
        Room newRoom = roomGenerator.GenerateRoom();
        if (newRoom != null)
        {
            newRoom.transform.position = roomSpawnPos;
            // No rotation applied — keep prefab’s original orientation

            openedDoors.Add(transform.position);
            gameObject.SetActive(false);

            DisableOppositeDoorInNewRoom(newRoom);
            DisablePreviouslyOpenedDoors(newRoom);
        }
    }

    private void DisableOppositeDoorInNewRoom(Room newRoom)
    {
        DoorDirection opposite = GetOppositeDirection(direction);
        Door[] doors = newRoom.GetComponentsInChildren<Door>();

        foreach (Door d in doors)
        {
            if (d.direction == opposite)
            {
                d.gameObject.SetActive(false);
                openedDoors.Add(d.transform.position);
                Debug.Log($"Disabled opposite door ({opposite}) in new room at {d.transform.position}");
                return;
            }
        }

        Debug.LogWarning("No matching opposite door found in new room.");
    }

    private void DisablePreviouslyOpenedDoors(Room newRoom)
    {
        foreach (Door d in newRoom.GetComponentsInChildren<Door>())
        {
            if (openedDoors.Contains(d.transform.position))
            {
                d.enabled = false;
            }
        }
    }

    private DoorDirection GetOppositeDirection(DoorDirection dir)
    {
        switch (dir)
        {
            case DoorDirection.North: return DoorDirection.South;
            case DoorDirection.South: return DoorDirection.North;
            case DoorDirection.East: return DoorDirection.West;
            case DoorDirection.West: return DoorDirection.East;
            default: return dir;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerNearby = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (corridorPrefab != null && roomGenerator != null && spawnPoint != null)
        {
            Transform corridorEnd = corridorPrefab.transform.Find("CorridorEndPoint");

            if (corridorEnd != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(
                    spawnPoint.position + (corridorEnd.position - corridorPrefab.transform.position),
                    roomGenerator.RoomBoundsSize
                );
            }
        }
    }
}
