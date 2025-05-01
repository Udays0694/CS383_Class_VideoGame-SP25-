using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [System.Serializable]
    public class RoomChance
    {
        public Room room;
        [Range(0f, 100f)] public float chancePercent = 100f;
    }

    [SerializeField] private Room SpawnRoom;
    [SerializeField] private RoomChance[] RoomChances;
    [SerializeField] private Room DefaultRoom;

    public Vector2 RoomBoundsSize = new Vector2(10f, 10f);

    private void Start()
    {
        SpawnRoom = GameObject.FindAnyObjectByType<Room>();
    }

    private void Update()
    {
        if (RoomChances == null || RoomChances.Length == 0)
            Debug.Log("No RoomChances assigned — using default room.");
    }

    int DetermineRoomIndex()
    {
        if (RoomChances == null || RoomChances.Length == 0)
            return -2;

        float roll = Random.Range(0f, 100f);
        float cumulative = 0f;

        for (int i = 0; i < RoomChances.Length; i++)
        {
            cumulative += RoomChances[i].chancePercent;

            if (roll <= cumulative)
                return i;
        }

        return -2;
    }

    void UpdateRoomsList()
    {
        Room[] rooms = GameObject.FindObjectsByType<Room>(FindObjectsSortMode.InstanceID);
    }

    public Room GenerateRoom()
    {
        int ind = DetermineRoomIndex();

        if (ind == -1)
        {
            Debug.LogWarning("No rooms available.");
            return null;
        }

        Room newRoom = (ind == -2 || RoomChances[ind].room == null)
            ? Instantiate(DefaultRoom)
            : Instantiate(RoomChances[ind].room);

        UpdateRoomsList();
        return newRoom;
    }
}
