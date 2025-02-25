using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private RoomGenerator roomGenerator;
    private bool playerNearby = false;
    private string direction;

    private void Start()
    {
        if (roomGenerator == null)
        {
            roomGenerator = GameObject.FindAnyObjectByType<RoomGenerator>();
        }

        // Automatically calculate the door's direction based on its position within the parent room
        CalculateDirection();
        Debug.Log($"Door {gameObject.name} set to direction: {direction}");
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
            // Generate the new room at the position and rotation of the spawnPoint
            Room newRoom = roomGenerator.GenerateRoom();
            if (newRoom != null && spawnPoint != null)
            {
                newRoom.transform.position = spawnPoint.position;
                newRoom.transform.rotation = spawnPoint.rotation;
                Debug.Log("New room instantiated at door's spawn point with position and rotation.");

                // Disable this door
                this.gameObject.SetActive(false);

                // Disable the corresponding door in the new room
                DisableCorrespondingDoor(newRoom);
            }
        }
    }

    private void DisableCorrespondingDoor(Room newRoom)
    {
        // Determine the opposite direction
        string oppositeDirection = GetOppositeDirection(direction);

        // Find all doors in the new room
        Door[] doors = newRoom.GetComponentsInChildren<Door>();

        if (doors.Length > 0)
        {
            foreach (Door door in doors)
            {
                // Recalculate the direction for each door in the new room
                door.CalculateDirection();
                Debug.Log($"Checking door: {door.gameObject.name}, Direction: {door.direction}");

                // Disable the door that has the opposite direction
                if (door.direction == oppositeDirection)
                {
                    door.gameObject.SetActive(false);
                    Debug.Log($"Disabled the {oppositeDirection} door in the new room.");
                }
            }
        }
        else
        {
            Debug.Log("No doors found in the new room to disable.");
        }
    }

    private string GetOppositeDirection(string dir)
    {
        switch (dir)
        {
            case "North": return "South";
            case "South": return "North";
            case "East": return "West";
            case "West": return "East";
            default: return "";
        }
    }

    private void CalculateDirection()
    {
        // Calculate the door's local position relative to its parent room
        Vector3 localPosition = transform.localPosition;

        // Determine direction based on position
        if (Mathf.Abs(localPosition.y) > Mathf.Abs(localPosition.x))
        {
            if (localPosition.y > 0)
                direction = "North";
            else
                direction = "South";
        }
        else
        {
            if (localPosition.x > 0)
                direction = "East";
            else
                direction = "West";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
