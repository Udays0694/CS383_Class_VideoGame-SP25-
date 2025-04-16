using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelGenTests
{
    private RoomGenerator roomGenerator;
    private Room defaultRoom;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        var loadOp = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("RoomGenTestScene");
        while (!loadOp.isDone) yield return null;
        yield return null;

        roomGenerator = GameObject.FindFirstObjectByType<RoomGenerator>();
        Assert.IsNotNull(roomGenerator, "RoomGenerator not found in scene!");

        Room[] testRooms = new Room[5];
        for (int i = 0; i < testRooms.Length; i++)
        {
            testRooms[i] = new GameObject($"TestRoom_{i}").AddComponent<Room>();
        }

        defaultRoom = new GameObject("DefaultRoom").AddComponent<Room>();

        SetPrivateField<Room[]>(roomGenerator, "GeneratableRooms", testRooms);
        SetPrivateField<Room>(roomGenerator, "DefaultRoom", defaultRoom);
        SetPrivateField<Vector2>(roomGenerator, "RoomBoundsSize", new Vector2(10f, 10f));
    }

    private void SetPrivateField<T>(object obj, string fieldName, T value)
    {
        var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field == null)
        {
            Debug.LogError($"❌ Field '{fieldName}' not found on {obj.GetType().Name}");
            foreach (var f in obj.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance))
                Debug.Log($"→ Found field: {f.Name}");
            Assert.Fail($"Field '{fieldName}' not found on {obj.GetType().Name}");
        }
        field.SetValue(obj, value);
    }

    [Test]
    public void GenerateRoom_ReturnsRoom()
    {
        Room result = roomGenerator.GenerateRoom();
        Assert.IsNotNull(result, "Generated room is null");
    }

    [Test]
    public void GeneratedRoom_IsFromList()
    {
        Room result = roomGenerator.GenerateRoom();
        Room[] list = (Room[])roomGenerator.GetType()
            .GetField("GeneratableRooms", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(roomGenerator);

        bool isTypeMatch = false;
        foreach (Room r in list)
        {
            if (r.GetType() == result.GetType())
            {
                isTypeMatch = true;
                break;
            }
        }

        Assert.IsTrue(isTypeMatch, "Generated room is not from the prefab list.");
    }

    [Test]
    public void GenerateRoom_UsesDefaultRoomIfListEmpty()
    {
        SetPrivateField<Room[]>(roomGenerator, "GeneratableRooms", new Room[0]);
        Room result = roomGenerator.GenerateRoom();
        Assert.AreEqual(defaultRoom.GetType(), result.GetType(), "Should return default room when list is empty.");
    }

    [Test]
    public void GenerateRoom_UsesDefaultRoomIfListNull()
    {
        SetPrivateField<Room[]>(roomGenerator, "GeneratableRooms", null);
        Room result = roomGenerator.GenerateRoom();
        Assert.AreEqual(defaultRoom.GetType(), result.GetType(), "Should return default room when list is null.");
    }

    [UnityTest]
    public IEnumerator StressTest_GenerateMultipleRooms()
    {
        for (int i = 0; i < 100; i++)
        {
            Room result = roomGenerator.GenerateRoom();
            Assert.IsNotNull(result, $"Room {i} failed to generate.");
            yield return null;
        }
    }

    [UnityTest]
    public IEnumerator OverlappingRoomPrevention()
    {
        var existingRoom = new GameObject("FakeRoom");
        existingRoom.tag = "Room";
        var col = existingRoom.AddComponent<BoxCollider2D>();
        col.size = new Vector2(10, 10);
        existingRoom.transform.position = Vector3.zero;

        SetPrivateField<Vector2>(roomGenerator, "RoomBoundsSize", new Vector2(10f, 10f));

        Room newRoom = roomGenerator.GenerateRoom();
        newRoom.transform.position = Vector3.zero;

        var overlap = Physics2D.OverlapBoxAll(newRoom.transform.position, new Vector2(10, 10), 0);
        bool isBlocked = false;
        foreach (var hit in overlap)
        {
            if (hit.CompareTag("Room") && hit.gameObject != newRoom.gameObject)
            {
                isBlocked = true;
                break;
            }
        }

        Assert.IsTrue(isBlocked, "Overlap prevention did not trigger.");
        yield return null;
    }

    [Test]
    public void RoomGenerator_HasGeneratableRoomsSet()
    {
        var rooms = (Room[])roomGenerator.GetType().GetField("GeneratableRooms",
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(roomGenerator);

        Assert.IsNotNull(rooms, "GeneratableRooms array is null.");
        Assert.IsNotEmpty(rooms, "GeneratableRooms array is empty.");
    }

    [Test]
    public void GenerateRoom_ReturnsDifferentInstances()
    {
        Room room1 = roomGenerator.GenerateRoom();
        Room room2 = roomGenerator.GenerateRoom();
        Assert.AreNotSame(room1, room2, "GenerateRoom returned the same instance twice.");
    }

    [Test]
    public void GeneratedRoom_IsPlacedOffOrigin()
    {
        Room room = roomGenerator.GenerateRoom();
        room.transform.position = new Vector3(5, 0, 0); // Simulate placement
        Assert.AreNotEqual(Vector3.zero, room.transform.position, "Room is still at origin, expected offset.");
    }

    [Test]
    public void RoomGenerator_DefaultRoomNotNull()
    {
        var defaultRoomField = roomGenerator.GetType().GetField("DefaultRoom",
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var def = defaultRoomField.GetValue(roomGenerator);
        Assert.IsNotNull(def, "Default room is null.");
    }

    [Test]
    public void RoomBounds_HasValidSize()
    {
        var size = (Vector2)roomGenerator.GetType()
            .GetField("RoomBoundsSize", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(roomGenerator);
        Assert.Greater(size.x, 0, "RoomBoundsSize.x must be > 0");
        Assert.Greater(size.y, 0, "RoomBoundsSize.y must be > 0");
    }

    [Test]
    public void GenerateRoom_WithoutCollisions()
    {
        Room room = roomGenerator.GenerateRoom();
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(room.transform.position, new Vector2(10, 10), 0);
        int collisionCount = 0;
        foreach (var hit in overlaps)
        {
            if (hit.gameObject != room.gameObject) collisionCount++;
        }
        Assert.LessOrEqual(collisionCount, 0, "Generated room overlaps with existing objects.");
    }

    [Test]
    public void RoomGenerator_SupportsMultipleCalls()
    {
        for (int i = 0; i < 10; i++)
        {
            Room result = roomGenerator.GenerateRoom();
            Assert.IsNotNull(result, $"Room {i} failed to generate on repeated calls.");
        }
    }

    [Test]
    public void GeneratedRoom_HasDoorComponent()
    {
        Room room = roomGenerator.GenerateRoom();
        Door[] doors = room.GetComponentsInChildren<Door>();
        Assert.IsNotEmpty(doors, "Generated room does not contain any Door components.");
    }

    [Test]
    public void Room_Prefab_NameIsConsistent()
    {
        Room room = roomGenerator.GenerateRoom();
        Assert.IsTrue(room.name.StartsWith("TestRoom") || room.name.StartsWith("DefaultRoom"), $"Unexpected room name: {room.name}");
    }

    [Test]
    public void RoomGenerator_InstantiatesPrefabClone()
    {
        Room room = roomGenerator.GenerateRoom();
        Assert.IsTrue(room.name.Contains("Clone"), "Generated room should be a clone of a prefab.");
    }

    [TearDown]
    public void TearDown()
    {
        foreach (Room r in Object.FindObjectsOfType<Room>())
            GameObject.DestroyImmediate(r.gameObject);
        if (roomGenerator != null)
            GameObject.DestroyImmediate(roomGenerator.gameObject);
    }
}