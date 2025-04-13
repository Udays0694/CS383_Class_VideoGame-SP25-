using UnityEngine;

public class ReverseDamageTest : MonoBehaviour
{
    public float Strength = 10.0f;

    void Start()
    {
        Strength = float.MinValue;
        Debug.Log("Strength reversed to: " + Strength);
    }

    void Update()
    {
        if (Strength == float.MinValue)
        {
            Debug.Log("Strength is at the minimum (negative) possible value.");
        }
    }
}
