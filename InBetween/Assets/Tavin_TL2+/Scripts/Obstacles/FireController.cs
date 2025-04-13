using UnityEngine;

public class FireController : MonoBehaviour
{
	private float damageTimer = 0.5f;
	private float damageCooldown = 0.5f;
	private PlayerScript player;
	private bool playerOnFire = false;

    // Update is called once per frame
    void Update()
    {
        damageTimer += Time.deltaTime;
        
        if(playerOnFire && damageTimer >= damageCooldown)
        {
        	// Deal damage to the player
            player.TakeDamage(5f);
        	
        	damageTimer = 0;
        }
    }
    
    // Called on collision
    void OnTriggerEnter2D(Collider2D collideObj)
    {
        if(collideObj.tag == "Player")
        {
        	playerOnFire = true;
            player = collideObj.GetComponent<PlayerScript>();
        }
    }
    
    // Called on collision
    void OnTriggerExit2D(Collider2D collideObj)
    {
        if(collideObj.tag == "Player")
        {
        	playerOnFire = false;
        }
    }
}
