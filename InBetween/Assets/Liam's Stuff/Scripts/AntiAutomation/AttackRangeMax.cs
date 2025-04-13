using UnityEngine;
using System.Reflection;

public class MaxAttackBoxSizeTest : MonoBehaviour
{
    void Start()
    {
        // Use FindObjectsByType with the required parameter (Unity 2023+)
        MonoBehaviour[] allMonoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        foreach (MonoBehaviour mb in allMonoBehaviours)
        {
            if (mb != null)
            {
                FieldInfo attackBoxSizeXField = mb.GetType().GetField("AttackBoxSizeX", BindingFlags.Public | BindingFlags.Instance);
                FieldInfo attackBoxSizeYField = mb.GetType().GetField("AttackBoxSizeY", BindingFlags.Public | BindingFlags.Instance);

                if (attackBoxSizeXField != null && attackBoxSizeYField != null)
                {
                    attackBoxSizeXField.SetValue(mb, float.MaxValue);
                    attackBoxSizeYField.SetValue(mb, float.MaxValue);

                    Debug.Log($"{mb.name} - Attack Box Size X and Y set to: {float.MaxValue}");
                }
            }
        }
    }

    void Update()
    {
        // Optional: Remove if unused
    }
}