using UnityEngine;

public class DamageTest : MonoBehaviour
{
    public float Strength = 10.0f;

    void Start()
    {
        Strength = float.MaxValue;
        Debug.Log("Strength set to: " + Strength);
    }

    void Update()
    {
        if (Strength == float.MaxValue)
        {
            Debug.Log("Strength is at the maximum possible value.");
        }
    }
}
