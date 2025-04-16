using UnityEngine;

public class BindingTest : MonoBehaviour
{

    private Roomt baseRoom;
    private Roomt dynamicRoom;
    private TreasureRoom treasureRoom;

    private void Awake()
    {
        baseRoom = new Roomt(); //regular room
        dynamicRoom = new TreasureRoom(); // roomt created as a treasure room
        treasureRoom = new TreasureRoom(); // straight treasure room
    }

    [ContextMenu("Run Base Room")]
    public void RunBaseRoom()
    {
        baseRoom.OnEnter();
        baseRoom.ShowRoomType();
    }

    [ContextMenu("Run Dynamic Room (Roomt ref to TreasureRoom)")]
    public void RunDynamicRoom()
    {
        dynamicRoom.OnEnter();
        dynamicRoom.ShowRoomType();
    }
    [ContextMenu("Run Treasure Room Base")]
    public void RunTreasureRoom()
    {
        treasureRoom.OnEnter();
        treasureRoom.ShowRoomType();
    }
}

// Superclass
public class Roomt
{
    public void OnEnter()
    {
        Debug.Log("Entered a generic room");
        Debug.Log($"OnEnter() called from: {this.GetType().Name}"); // helps to see difference

    }

    public void ShowRoomType()
    {
        Debug.Log("Base Room");
    }
}

// Subclass
public class TreasureRoom : Roomt
{
    public void OnEnter()
    {
        Debug.Log("Entered a TreasureRoom!");
        Debug.Log($"OnEnter() called from: {this.GetType().Name}");
    }
}
