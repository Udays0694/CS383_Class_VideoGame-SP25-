using log4net.Util;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public PlayerScript playerScript;
    public float rayDistance = 100f;
    public GameObject player = null;
    public LayerMask floorMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerScript>();
        }
        else
        {
            Debug.LogWarning("No player found. The enemy will not be able to interact with the player.");
        }
    }

    // Update is called once per 
    void Update()
    {
        
    }
}
