using UnityEngine;
//This is the room Class that will run 
public class Room : MonoBehaviour
{
    [SerializeField] // for debug purposes (unessesary)
    RoomGenerator RoomGeneratorObj;
    FurnitureSpawnPoint[] SpawnPointfurn;
    

    private void Start() // Everything here only needs to run once as it is running the generation algorythsm
    {
        RoomGeneratorObj = GameObject.FindAnyObjectByType<RoomGenerator>();
        SpawnPointfurn = FindSpawnPoints();

    }

    FurnitureSpawnPoint[] FindSpawnPoints() {
        FurnitureSpawnPoint[] spwn;
        spwn = GetComponentsInChildren<FurnitureSpawnPoint>();
        if (spwn.Length > 0) { Debug.Log("Found " + spwn.Length + " Spawn Points Attempting to spawn furniture"); } 
        else {  Debug.Log("Can Not Generate Furniture Loading Default Layout"); } // my bad for if else statement lmao 
        return spwn;
    }

    void PlaceObjects()
    {

    }

    void InitializeRoom() { 
    
    }
}
