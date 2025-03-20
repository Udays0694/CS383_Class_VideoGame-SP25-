using UnityEngine;

/*This is the room generation handler, essentially anything that is causing something to generate will be done through here. This Script will make things generate.*/
public class RoomGenerator : MonoBehaviour
{
    [SerializeField]
    private Room SpawnRoom;
    [SerializeField] private Room[] Rooms; // List of the current Generated Rooms
    [SerializeField] private Room[] GeneratableRooms; // List of the room layouts that are possible to generate
    [SerializeField] private Room DefaultRoom; // Default fallback room

    private void Start()
    {
        SpawnRoom = GameObject.FindAnyObjectByType<Room>();
    }

    private void Update()
    {
        if (GeneratableRooms == null || GeneratableRooms.Length == 0)
        {
            Debug.Log("No Available Rooms to Generate — Falling back to default room");
        }
    }

    int DetermineRoom()
    {
        if (GeneratableRooms == null || GeneratableRooms.Length == 0)
        {
            if (DefaultRoom != null)
            {
                Debug.Log("GeneratableRooms is empty — using DefaultRoom");
                return -2;
            }
            else
            {
                Debug.LogWarning("GeneratableRooms is null or empty — No DefaultRoom assigned");
                return -1;
            }
        }

        int ind = Random.Range(0, GeneratableRooms.Length);

        // Catch any negative or out-of-bounds index
        if (ind < 0 || ind >= GeneratableRooms.Length)
        {
            if (DefaultRoom != null)
            {
                Debug.LogWarning("Level selection out of scope — selecting DefaultRoom");
                return -2;
            }
            else
            {
                Debug.LogWarning("Level selection out of scope — no DefaultRoom assigned");
                return -1;
            }
        }

        Debug.Log("Picked a room index: " + ind);
        return ind;
    }

    void UpdateRoomsList()
    {
        Rooms = GameObject.FindObjectsByType<Room>(FindObjectsSortMode.InstanceID);
    }

    public Room GenerateRoom()
    {
        int ind = DetermineRoom();

        if (ind == -1) // Nothing to generate
        {
            Debug.LogWarning("Failed to generate room — no valid rooms available");
            return null;
        }

        Room newRoom;
        if (ind == -2) // Use DefaultRoom if GeneratableRooms is empty or null
        {
            Debug.Log("Spawning default room");
            newRoom = Instantiate(DefaultRoom, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.Log("Spawning room at index: " + ind);
            newRoom = Instantiate(GeneratableRooms[ind], Vector3.zero, Quaternion.identity);
        }

        UpdateRoomsList();
        return newRoom;
    }
}
