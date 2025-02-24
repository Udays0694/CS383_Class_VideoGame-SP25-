using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
	// Enemy options
	public float speed = 5f;
	public float damage = 5f;
	public float health = 10f;

	private Slider healthBar; 

    // Start is called once before the first execution of Update after the
	// MonoBehaviour is created
    void Start()
    {
		// Initiate health bar
		healthBar = GetComponentInChildren<Slider>();
    	healthBar.maxValue = health;
		healthBar.value = health;
	}

    // Update is called once per frame
    void Update()
    {
        chasePlayer();
	}

	// Move towards player
	private void chasePlayer()
	{
		// Calculate direction of player
/*		Vector2 direction;
		direction.x = PlayerController.playerPos.x - transform.position.x;
		direction.y = PlayerController.playerPos.y - transform.position.y;
		
		// Calculate move distance
		direction.x = transform.position.x
					  + direction.normalized.x * Time.deltaTime * speed;
		direction.y = transform.position.y
				   	  + direction.normalized.y * Time.deltaTime * speed;

		// Apply movement
		transform.position = direction;
*/	}

	// Take damage
	private void takeDamage(float amount)
	{
		health -= amount;
		healthBar.value = health;
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}

	// Handle collisions with player attacks
	void OnTriggerEnter2D(Collider2D collideObj)
	{
		if(collideObj.tag == "")
		{
			takeDamage(5f);
		}
	}
}
