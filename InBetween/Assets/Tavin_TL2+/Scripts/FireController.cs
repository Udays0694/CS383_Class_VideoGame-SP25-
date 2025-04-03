using UnityEngine;

public class FireController : MonoBehaviour
{
	private float damageTimer = 0;
	private float damageCooldown = 0.5f;

    // Update is called once per frame
    void Update()
    {
        damageTimer += Time.deltaTime;
    }
    
    // Called on collision
    void OnTriggerStay2D(Collider2D collideObj)
    {
        if(collideObj.tag == "Player" && damageTimer >= damageCooldown)
        {
        	// Deal damage to the player
            collideObj.GetComponent<PlayerScript>().TakeDamage(5f);
        	
        	damageTimer = 0;
        }
    }
}
