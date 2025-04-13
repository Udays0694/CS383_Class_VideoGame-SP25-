using UnityEngine;
using System.Collections;

public class AutoKeyPressTest : MonoBehaviour
{
    private int wPresses = 0;
    private int aPresses = 0;
    private int sPresses = 0;
    private int dPresses = 0;
    private float testDuration = 10f;
    private bool isTesting = false;

    void Start()
    {
        StartCoroutine(KeyPressTestCoroutine());
    }

    private IEnumerator KeyPressTestCoroutine()
    {
        isTesting = true;
        float startTime = Time.time;

        while (Time.time - startTime < testDuration)
        {
            SimulateKeyPress(KeyCode.W, ref wPresses);
            SimulateKeyPress(KeyCode.A, ref aPresses);
            SimulateKeyPress(KeyCode.S, ref sPresses);
            SimulateKeyPress(KeyCode.D, ref dPresses);
            yield return null;
        }

        isTesting = false;
        Debug.Log($"Test complete! W: {wPresses} presses, A: {aPresses} presses, S: {sPresses} presses, D: {dPresses} presses.");
    }

    private void SimulateKeyPress(KeyCode key, ref int pressCount)
    {
        if (Time.frameCount % 2 == 0)
        {
            if (key == KeyCode.W)
            {
                pressCount++;
            }
            else if (key == KeyCode.A)
            {
                pressCount++;
            }
            else if (key == KeyCode.S)
            {
                pressCount++;
            }
            else if (key == KeyCode.D)
            {
                pressCount++;
            }
        }
    }

    void Update()
    {
        if (!isTesting && (wPresses + aPresses + sPresses + dPresses > 0))
        {
            Debug.Log($"Final press counts after 10 seconds: W = {wPresses}, A = {aPresses}, S = {sPresses}, D = {dPresses}");
        }
    }
}
