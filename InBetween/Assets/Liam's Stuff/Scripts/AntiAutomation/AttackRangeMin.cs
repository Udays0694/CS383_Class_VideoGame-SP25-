using UnityEngine;
using System.Reflection;

public class MinAttackBoxSizeTest : MonoBehaviour
{
    void Start()
    {
        // Correct usage with required sort mode parameter
        MonoBehaviour[] allMonoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        foreach (MonoBehaviour mb in allMonoBehaviours)
        {
            if (mb != null)
            {
                FieldInfo attackBoxSizeXField = mb.GetType().GetField("AttackBoxSizeX", BindingFlags.Public | BindingFlags.Instance);
                FieldInfo attackBoxSizeYField = mb.GetType().GetField("AttackBoxSizeY", BindingFlags.Public | BindingFlags.Instance);

                if (attackBoxSizeXField != null && attackBoxSizeYField != null)
                {
                    attackBoxSizeXField.SetValue(mb, float.MinValue);
                    attackBoxSizeYField.SetValue(mb, float.MinValue);

                    Debug.Log($"{mb.name} - Attack Box Size X and Y set to: {float.MinValue}");
                }
            }
        }
    }

    void Update()
    {
        // Still unused, safe to remove unless you need it later
    }
}
