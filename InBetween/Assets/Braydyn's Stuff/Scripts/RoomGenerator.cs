using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Room SpawnRoom;
    [SerializeField] private Room[] Rooms;
    [SerializeField] private Room[] GeneratableRooms;
    [SerializeField] private Room DefaultRoom;

    public Vector2 RoomBoundsSize = new Vector2(10f, 10f); // Adjust to your prefab’s real size

    private void Start()
    {
        SpawnRoom = GameObject.FindAnyObjectByType<Room>();
    }

    private void Update()
    {
        if (GeneratableRooms == null || GeneratableRooms.Length == 0)
            Debug.Log("No Available Rooms to Generate — Falling back to default room");
    }

    int DetermineRoom()
    {
        if (GeneratableRooms == null || GeneratableRooms.Length == 0)
            return DefaultRoom ? -2 : -1;

        int ind = Random.Range(0, GeneratableRooms.Length);
        return ind;
    }

    void UpdateRoomsList()
    {
        Rooms = GameObject.FindObjectsByType<Room>(FindObjectsSortMode.InstanceID);
    }

    public Room GenerateRoom()
    {
        int ind = DetermineRoom();

        if (ind == -1)
        {
            Debug.LogWarning("No rooms available.");
            return null;
        }

        Room newRoom = (ind == -2) ? Instantiate(DefaultRoom) : Instantiate(GeneratableRooms[ind]);
        UpdateRoomsList();
        return newRoom;
    }
}
