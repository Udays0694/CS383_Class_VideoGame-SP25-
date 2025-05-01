using UnityEngine;

public class DontDestroyonLoadplz : MonoBehaviour
{
    private static DontDestroyonLoadplz instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log($"[PersistentSingleton] Destroying duplicate: {gameObject.name}");
            Destroy(gameObject);
        }
    }
}
