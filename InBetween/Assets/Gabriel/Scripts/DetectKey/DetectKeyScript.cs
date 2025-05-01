using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneSwitcher : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneAsset;
#endif
    [SerializeField] private string sceneName;

    void Start()
    {
#if UNITY_EDITOR
        // Automatically set sceneName from sceneAsset in the editor
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name;
        }
#endif
    }

    void Update()
    {
        if (Input.anyKeyDown && !string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
