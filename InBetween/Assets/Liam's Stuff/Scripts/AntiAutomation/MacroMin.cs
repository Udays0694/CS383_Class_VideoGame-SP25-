using UnityEngine;

public class SimulateMovementKeys : MonoBehaviour
{
    void Update()
    {
        SimulateKeyPress(KeyCode.W);
        SimulateKeyPress(KeyCode.A);
        SimulateKeyPress(KeyCode.S);
        SimulateKeyPress(KeyCode.D);
    }

    private void SimulateKeyPress(KeyCode key)
    {
        if (key == KeyCode.W)
        {
            Debug.Log("Simulating W key press (Move Forward)");
        }
        if (key == KeyCode.A)
        {
            Debug.Log("Simulating A key press (Move Left)");
        }
        if (key == KeyCode.S)
        {
            Debug.Log("Simulating S key press (Move Backward)");
        }
        if (key == KeyCode.D)
        {
            Debug.Log("Simulating D key press (Move Right)");
        }
    }
}
