using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public PlayerScript playerScript = null;
    public bool PlayerIsMoving = false;
    public float AFKTimer = 0f;

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
    }

    // Update is called once per frame
    void Update()
    {
        PlayerIsMoving = playerScript.movementKeyPressed;
        if (PlayerIsMoving)
        {
            playerScript.moveUp = false;
            playerScript.moveDown = false;
            playerScript.moveLeft = false;
            playerScript.moveRight = false;
            AFKTimer = 0;
        }
        else
        {
            AFKTimer += Time.deltaTime;
            if (AFKTimer > 2)
            {
                PlayerControl();
            }
        }
    }

    void PlayerControl()
        {
            playerScript.moveUp = true;
            Debug.Log("Player movement increased");
        }
}
