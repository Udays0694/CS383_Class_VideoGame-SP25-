using UnityEngine;

// Just a simple health manager to show how you can controll the HealthBar UI
public class HealthManager : MonoBehaviour
{

    public HealthBar healthBar;
    public PlayerScript playerScript;

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
        healthBar.SetMaxHealth(100);
    }

    private void Update()
    {
        UpdateBar();
    }

    void UpdateBar()
    {
        healthBar.SetCurrentHealth(playerScript.health);
    }
}