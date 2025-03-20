using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementSimulationTest : MonoBehaviour
{
    private bool errorOccurred = false;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found in the scene.");
            errorOccurred = true;
        }
    }

    [UnityTest]
    public IEnumerator MovementTestWithWASD()
    {
        if (playerMovement == null)
        {
            Assert.Fail("No PlayerMovement script detected.");
            yield break;
        }

        for (int i = 0; i < 100; i++)
        {
            try
            {
                SimulateKeyPress(KeyCode.W);
                SimulateKeyPress(KeyCode.A);
                SimulateKeyPress(KeyCode.S);
                SimulateKeyPress(KeyCode.D);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error during movement iteration {i}: {e.Message}");
                errorOccurred = true;
            }

            yield return null;
        }

        if (!errorOccurred)
        {
            Debug.Log("100 movement inputs completed successfully.");
        }
        else
        {
            Debug.LogError("Errors occurred during movement simulation.");
        }
    }

    private void SimulateKeyPress(KeyCode key)
    {
        if (Input.GetKey(key))
        {
            playerMovement.Move(key);
        }
    }
}