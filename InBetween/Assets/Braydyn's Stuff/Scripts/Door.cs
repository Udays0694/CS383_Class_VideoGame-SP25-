using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private RoomGenerator roomGenerator;
    private bool playerNearby = false;

    private void Start()
    {
        // Automatically find the RoomGenerator if not assigned
        if (roomGenerator == null)
        {
            roomGenerator = GameObject.FindAnyObjectByType<RoomGenerator>();
        }
    }

    private void Update()
    {
        // Check if the player presses the interaction key (E) while nearby
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            InstantiateRoom();
        }
    }

    private void InstantiateRoom()
    {
        if (roomGenerator != null)
        {
            // Capture the global position of the current door
            Vector3 doorPosition = transform.position;
            Debug.Log($"Current Door Position: {doorPosition}");

            // Generate the new room at the position and rotation of the spawnPoint
            Room newRoom = roomGenerator.GenerateRoom();
            if (newRoom != null && spawnPoint != null)
            {
                newRoom.transform.position = spawnPoint.position;
                newRoom.transform.rotation = spawnPoint.rotation;
                Debug.Log("New room instantiated at door's spawn point with position and rotation.");

                // Disable this door
                this.gameObject.SetActive(false);
                Debug.Log($"Disabled the current door at: {transform.position}");

                // Find and disable the closest door in the new room
                DisableClosestDoor(newRoom, doorPosition);
            }
        }
    }

    private void DisableClosestDoor(Room newRoom, Vector3 referencePosition)
    {
        // Find all doors in the new room
        Door[] doors = newRoom.GetComponentsInChildren<Door>();

        if (doors.Length > 0)
        {
            Door closestDoor = null;
            float closestDistance = Mathf.Infinity;

            foreach (Door door in doors)
            {
                // Calculate the distance from the reference position
                float distance = Vector3.Distance(referencePosition, door.transform.position);
                Debug.Log($"Checking door: {door.gameObject.name}, Distance: {distance}");

                // Find the closest door
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDoor = door;
                }
            }

            // Disable the closest door
            if (closestDoor != null)
            {
                closestDoor.gameObject.SetActive(false);
                Debug.Log($"Disabled the closest door in the new room at {closestDoor.transform.position}");
            }
            else
            {
                Debug.LogWarning("No closest door found to disable.");
            }
        }
        else
        {
            Debug.Log("No doors found in the new room to disable.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player is nearby
        if (collision.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player leaves the area
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
