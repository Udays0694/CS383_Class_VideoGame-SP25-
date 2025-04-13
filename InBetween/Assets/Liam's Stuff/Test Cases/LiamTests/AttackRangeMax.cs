using UnityEngine;
using System.Reflection;

public class MaxAttackBoxSizeTest : MonoBehaviour
{
    void Start()
    {
        MonoBehaviour[] allMonoBehaviours = FindObjectsOfType<MonoBehaviour>();

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

                    Debug.Log("Attack Box Size X set to: " + float.MaxValue);
                    Debug.Log("Attack Box Size Y set to: " + float.MaxValue);
                }
            }
        }
    }

    void Update()
    {

    }
}
