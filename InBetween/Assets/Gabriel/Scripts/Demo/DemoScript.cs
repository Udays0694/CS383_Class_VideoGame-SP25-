using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DemoScript : MonoBehaviour
{
    public PlayerScript playerScript = null;
    public bool PlayerIsMoving = false;
    public float AFKTimer = 0f;

#if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneAsset;
#endif
    [SerializeField] private string sceneName;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerScript>();
        }
        else
        {
            Debug.LogWarning("No player found. The enemy will not be able to interact with the player.");
        }

#if UNITY_EDITOR
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name;
        }
#endif
    }

    void Update()
    {
        if (playerScript == null) return;

        PlayerIsMoving = playerScript.movementKeyPressed;

        if (PlayerIsMoving)
        {
            playerScript.moveUp = false;
            playerScript.moveDown = false;
            playerScript.moveLeft = false;
            playerScript.moveRight = false;
            AFKTimer = 0f;
        }
        else
        {
            AFKTimer += Time.deltaTime;
            if (AFKTimer > 30f)
            {
                PlayerControl();
            }
        }
    }

    void PlayerControl()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not set. Drag a scene asset into the Inspector.");
        }
    }
}
