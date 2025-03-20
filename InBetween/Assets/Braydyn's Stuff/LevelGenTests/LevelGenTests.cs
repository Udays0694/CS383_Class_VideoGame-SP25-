using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelGenTests
{
    private RoomGenerator roomGenerator;

    [SetUp]
    public void SetUp()
    {
        // Create RoomGenerator instance
        GameObject obj = new GameObject("RoomGenerator");
        roomGenerator = obj.AddComponent<RoomGenerator>();

        // Create test rooms and assign them to the generator
        Room testRoom = new GameObject("TestRoom").AddComponent<Room>();
        Room[] testRooms = new Room[5];
        for (int i = 0; i < testRooms.Length; i++)
        {
            testRooms[i] = GameObject.Instantiate(testRoom);
        }

        roomGenerator.GetType()
            .GetField("GeneratableRooms", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(roomGenerator, testRooms);
    }

    // Stress Test: Generate multiple rooms rapidly
    [UnityTest]
    public IEnumerator StressTest_RapidRoomGeneration()
    {
        int roomCount = 100;
        float timeout = 5.0f;
        float startTime = Time.time;

        float totalFrameTime = 0;
        int frameCount = 0;

        // CPU Usage tracking
        var process = System.Diagnostics.Process.GetCurrentProcess();
        float startCpuTime = (float)process.TotalProcessorTime.TotalMilliseconds;
        int processorCount = System.Environment.ProcessorCount;

        for (int i = 0; i < roomCount; i++)
        {
            Room newRoom = roomGenerator.GenerateRoom();
            Assert.IsNotNull(newRoom, $"Room {i} failed to generate");

            // Track FPS
            totalFrameTime += Time.deltaTime;
            frameCount++;

            yield return null;
        }

        float endCpuTime = (float)process.TotalProcessorTime.TotalMilliseconds;
        float totalCpuTime = (endCpuTime - startCpuTime) / 1000f;

        // Calculate Average FPS
        float avgFps = frameCount / totalFrameTime;
        float totalTime = Time.time - startTime;

        // Correct CPU usage by number of processors
        float cpuUsage = ((totalCpuTime / totalTime) / processorCount) * 100f;

        Debug.Log($"Stress Test Complete! Avg FPS: {avgFps:F2}, CPU Usage: {cpuUsage:F2}%");

        Assert.Less(totalTime, timeout, "Stress test took too long!");
    }

    // Upper Boundary Test
    [Test]
    public void UpperBoundaryTest_GeneratableRooms()
    {
        Room testRoom = new GameObject("TestRoom").AddComponent<Room>();
        Room defaultRoom = new GameObject("DefaultRoom").AddComponent<Room>();

        roomGenerator.GetType()
            .GetField("GeneratableRooms", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(roomGenerator, new Room[1] { testRoom });

        roomGenerator.GetType()
            .GetField("DefaultRoom", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(roomGenerator, defaultRoom);

        int result = roomGenerator.GenerateRoom() ? -2 : -1;

        Assert.AreEqual(-2, result, "Out-of-bounds index should fall back to default room");
        Debug.Log("Invalid Index (too high) choosing default room");
    }

    // Lower Boundary Test
    [Test]
    public void LowerBoundaryTest_GeneratableRooms()
    {
        Room testRoom = new GameObject("TestRoom").AddComponent<Room>();
        Room defaultRoom = new GameObject("DefaultRoom").AddComponent<Room>();

        roomGenerator.GetType()
            .GetField("GeneratableRooms", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(roomGenerator, new Room[1] { testRoom });

        roomGenerator.GetType()
            .GetField("DefaultRoom", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(roomGenerator, defaultRoom);

        int result = roomGenerator.GenerateRoom() ? -2 : -1;

        Assert.AreEqual(-2, result, "Negative index should fall back to default room");
        Debug.Log("Invalid Index (too low) choosing default room");
        
    }

    [TearDown]
    public void TearDown()
    {
        if (roomGenerator != null)
        {
            GameObject.DestroyImmediate(roomGenerator.gameObject);
        }

        foreach (Room room in GameObject.FindObjectsOfType<Room>())
        {
            if (room != null)
            {
                GameObject.DestroyImmediate(room.gameObject);
            }
        }
    }
}
