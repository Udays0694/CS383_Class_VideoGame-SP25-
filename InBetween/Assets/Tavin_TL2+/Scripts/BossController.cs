using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
	// Options
	public float speed = 0.5f;
	public float health = 100f;
	private float attack1Cooldown = 2f;

	// Characteristics
	private Slider healthBar; 
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
	private float attack1Timer = 0;
	
	// Fireball
	[SerializeField] private GameObject Bullet1;
	private FireballPool pool;
	private Vector2 mouthPos;
	
	// Orb
	[SerializeField] private GameObject Orb;
	private float orbRespawnTimer = 0;
	private float orbRespawnTime = 1;
	private bool respawnOrb = false;
	
	// Minions
	private bool spawnMinions = true;
	
	// Public so orb can see it
	public bool facingLeft = true;

	// Player
	protected GameObject Player;
	protected PlayerScript playerScript;
	protected Vector2 playerDir;

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
		
		// Get reference to animator
		animator = GetComponent<Animator>();
		
		// Get reference to sprite renderer
		spriteRenderer = GetComponent<SpriteRenderer>();
		
		// Get fireball pool
		pool = GameObject.FindGameObjectWithTag("Pool").GetComponent<FireballPool>();
		
		// Test orb attack
		attack2();
	}

    // Update is called once per frame
    void Update()
    {	
		// Calculate mouth position and center of face
		float xTransform = 1.1f;
		float centerDiff = 1.54f;
		if(!facingLeft)
		{
			xTransform = -xTransform;
			centerDiff = -centerDiff;
		}
		
		mouthPos = transform.position + new Vector3(xTransform, -2.3f, 0);

		// Distance the boss must be transformed to make the flip look natural
		float flipTransformDist = 3.08f;  
		
		// Calculate direction to player
		playerDir = Player.transform.position
					- (transform.position + new Vector3(centerDiff, -2.5f, 0));
		
		// Face the player
		if(facingLeft && playerDir.x > 0)
		{
			transform.position += new Vector3(flipTransformDist, 0, 0);
//			spriteRenderer.flipX = true;
			healthBar.transform.Rotate(0, 180, 0);
			transform.Rotate(0, 180, 0);
			facingLeft = false;
		}
		else if(!facingLeft && playerDir.x < 0)
		{
			transform.position += new Vector3(-flipTransformDist, 0, 0);
//			spriteRenderer.flipX = false;
			healthBar.transform.Rotate(0, 180, 0);
			transform.Rotate(0, 180, 0);
			facingLeft = true;
		}

		// Attack player
		move();
        if(attack1Timer >= attack1Cooldown
        && !animator.GetCurrentAnimatorStateInfo(0).IsName("BossDie")
        && !animator.GetCurrentAnimatorStateInfo(0).IsName("BossHurt"))
		{
			// attack1() is called from the animation 
			animator.Play("BossAttack");
			attack1Timer = 0;
		}
		
		// Spawn another orb after delay
    	if(respawnOrb)
    	{
			orbRespawnTimer += Time.deltaTime;
			
			if(orbRespawnTimer >= orbRespawnTime)
			{
				orbRespawnTimer = 0;
				respawnOrb = false; // Prevents the orb from spawning multiple orbs
				Instantiate(Orb);
			}
		}

		attack1Timer += Time.deltaTime;
	}

	// Fireball attack
	public void attack1()
	{
		// Generate quaternion for spawn rotation of bullet
		int xDir = 1;
		if(facingLeft)
		{
			xDir = -1;
		}
		Quaternion shootDir = Quaternion.LookRotation(Vector3.forward, new Vector3(xDir, 0, 0));
	
		// Get fireball from pool
		GameObject fireball = pool.getInstance();
		
		// Apply transform and rotation
		fireball.transform.position = mouthPos;
		fireball.transform.rotation = shootDir;
		fireball.SetActive(true);
			
		// Spawn bullet
//		Instantiate(Bullet1, mouthPos, shootDir);
	}
	
	// Orb attack
	public void attack2()
	{
		respawnOrb = true;
	}

	// Move towards player
	private void move()
	{
		// Move away from player if its too close
		Vector3 moveDir = playerDir;
		if(playerDir.magnitude < 3.5f)
		{
			moveDir = -moveDir;
		}
		
		// Calculate move distance
		Vector3 move;
		move = transform.position + moveDir.normalized * Time.deltaTime * speed;

		// Apply movement
		transform.position = move;
	}

	// Called by death animation
	private void destroy()
	{
		Destroy(gameObject);
	}	
	
	// Take damage
	public void takeDamage(float amount)
	{
		// Decrease health
		health -= amount;
		healthBar.value = health;
		
		Debug.Log("Boss Health: " + health);
		// Die
		if(health <= 0)
		{
			destroy();
		}
/*		if(health <= 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("BossDie"))
		{
			animator.Play("BossDie");
		}
		// Play damage animation
		else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("BossHurt"))
		{
			animator.Play("BossHurt");
		}
*/		
		// Enable orb attack and minion spawning
		if(health <= healthBar.maxValue * 0.333f)
		{
			spawnMinions = true;
		}
		// Enable minion spawning
		else if(health <= healthBar.maxValue * 0.667f)
		{
			respawnOrb = true;
		}
	}
}
