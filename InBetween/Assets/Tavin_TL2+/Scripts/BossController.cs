using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
	// Options
	public float speed = 0.5f;
	public float damage = 5f;
	public float health = 10f;
	private float attack1Cooldown = 1f;

	// Characteristics
	private Slider healthBar; 
	private float attack1Timer = 0;
	[SerializeField] private GameObject Bullet1;

	// Player
	private GameObject Player;
	private Vector2 playerDir;

    // Start is called once before the first execution of Update after the
	// MonoBehaviour is created
    void Start()
    {
		// Initiate health bar
		healthBar = GetComponentInChildren<Slider>();
    	healthBar.maxValue = health;
		healthBar.value = health;
		
		// Get reference to player
		Player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    void Update()
    {
		// Calculate direction to player
		playerDir.x = Player.transform.position.x - transform.position.x;
		playerDir.y = Player.transform.position.y - transform.position.y;

		// Attack player
		chasePlayer();
        if(attack1Timer >= attack1Cooldown)
		{
			attack1();
			attack1Timer = 0;
		}

		attack1Timer += Time.deltaTime;
	}

	// Base attack
	public void attack1()
	{
		// Generate quaternion
		Vector3 playerDir3d = new Vector3(playerDir.x, playerDir.y, 0);
		Quaternion shootDir = Quaternion.LookRotation(Vector3.forward, playerDir3d);
		Debug.Log("Shooting bullet");
		Instantiate(Bullet1, transform.position, shootDir);
	}

	// Move towards player
	private void chasePlayer()
	{
		// Calculate move distance
		Vector2 move;
		move.x = transform.position.x
				 + playerDir.normalized.x * Time.deltaTime * speed;
		move.y = transform.position.y
				   	  + playerDir.normalized.y * Time.deltaTime * speed;

		// Apply movement
		transform.position = move;
	}

	// Take damage
	public void takeDamage(float amount)
	{
		health -= amount;
		healthBar.value = health;
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
