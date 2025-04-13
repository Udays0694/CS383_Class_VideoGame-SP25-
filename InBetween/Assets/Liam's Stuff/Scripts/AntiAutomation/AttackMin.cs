using UnityEngine;

public class RandomKeyPressAndMouseClick : MonoBehaviour
{
    private KeyCode[] movementKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    void Start()
    {
        InvokeRepeating("SimulateRandomInputs", 0f, 2f);
    }

    void SimulateRandomInputs()
    {
        KeyCode firstKey = GetRandomMovementKey();
        KeyCode secondKey = GetRandomMovementKey();

        SimulateKeyPress(firstKey);
        SimulateKeyPress(secondKey);
        SimulateMouseClick();
    }

    KeyCode GetRandomMovementKey()
    {
        int randomIndex = Random.Range(0, movementKeys.Length);
        return movementKeys[randomIndex];
    }

    void SimulateKeyPress(KeyCode key)
    {
        Debug.Log($"Simulating {key} key press");
    }

    void SimulateMouseClick()
    {
        Debug.Log("Simulating left mouse button click");
    }
}
