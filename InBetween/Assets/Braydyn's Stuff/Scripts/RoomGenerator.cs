using UnityEngine;
/*This is the room generation handler, essentailly anything that is causing something to generate will be done through here. This Script will make things generate.*/
public class RoomGenerator : MonoBehaviour
{
    [SerializeField]
    private Room SpawnRoom;
    [SerializeField] private Room[] Rooms; // List of the current Generated Rooms
    [SerializeField] private Room[] GeneratableRooms; // list of the room layouts that are possible to generate
    
    private void Start()
    {
        SpawnRoom = GameObject.FindAnyObjectByType<Room>(); // Needed to run at the start as there will only be one possible room at the start.
    }

    private void Update()
    {
        if (GeneratableRooms == null) { Debug.Log("No Avaliable Rooms to Generate"); }
        if (Input.GetKeyDown(KeyCode.E)) { GenerateRoom(); }
    }

    int DetermineRoom() {
    int ind;
    ind = Random.Range(0, GeneratableRooms.Length);
        Debug.Log("Picked a room" + "Room: " + ind);
        return ind;
    }
    void UpdateRoomsList()
    {
        Rooms = GameObject.FindObjectsByType<Room>(FindObjectsSortMode.None);
    }

    Room GenerateRoom() {
        int ind; 
        ind = DetermineRoom();
        Debug.Log("Spawning room" + ind);
        Instantiate(GeneratableRooms[ind], new Vector2(12, 30), Quaternion.identity);
        UpdateRoomsList();
        return null; // null for now;

    }


}
