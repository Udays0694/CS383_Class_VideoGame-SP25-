using UnityEngine;

public class MoveSpeedStandaloneTest : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxMoveSpeed = 5f;
    public float testSpeedIncrease = 20f;

    void Start()
    {
        Debug.Log($"[TEST] Starting moveSpeed: {moveSpeed}");
        Debug.Log($"[TEST] Attempting to increase moveSpeed by {testSpeedIncrease}...");

        moveSpeed += testSpeedIncrease;

        if (moveSpeed > maxMoveSpeed)
        {
            moveSpeed = maxMoveSpeed;
            Debug.Log($"[RESULT] moveSpeed exceeded max! Capped to: {moveSpeed}");
        }
        else
        {
            Debug.Log($"[RESULT] moveSpeed increased successfully to: {moveSpeed}");
        }
    }
}
