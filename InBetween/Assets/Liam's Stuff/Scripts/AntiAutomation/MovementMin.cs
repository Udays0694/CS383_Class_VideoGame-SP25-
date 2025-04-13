using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    void Start()
    {
        MoveSpeed = Mathf.Epsilon;
        Debug.Log("MoveSpeed set to: " + MoveSpeed);
    }

    void Update()
    {
        if (MoveSpeed == Mathf.Epsilon)
        {
            Debug.Log("MoveSpeed is the smallest possible value.");
        }
    }
}
