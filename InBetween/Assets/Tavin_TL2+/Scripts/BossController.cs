using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
	// Options
	public float speed = 0.5f;
	public float maxHealth = 100f;
	public float health;
	private float attack1Cooldown = 2f;

	// Characteristics
	private Slider healthBar; 
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
	private float attack1Timer = 0;
	protected bool activated = true;
	
	// Fireball
	[SerializeField] private GameObject Bullet1;
	private FireballPool pool;
	private Vector2 mouthPos;
	
	// Orb
	[SerializeField] private GameObject Orb;
	private float orbRespawnTime = 1;
	private float orbRespawnTimer = 1;
	private bool orbEnable = false;
	
	// Minions
	[SerializeField] private GameObject Minion;
	private float minionRespawnTime = 12;
	private float minionRespawnTimer = 12;
	private bool spawnMinions = false;
	private Vector3[] minionSpawnLocations = { new Vector3(-6f, 1.5f, 0), new Vector3(6f, 1.5f, 0),
											   new Vector3(6f, -4.5f, 0), new Vector3(-6f, -4.5f, 0) };
	
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
    	// DYNAMIC BINDING TEST
    	DynamicDemo demo = new DynamicDemo();
    	demo.start();
    
		// Initiate health bar
		health = maxHealth;
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
	}

    // Update is called once per frame
    void Update()
    {	
    	if(!activated)
    	{
    		return;
    	}
    	    	
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
		
		// Check if it's time for another orb to spawn
		attack2();

		attack1Timer += Time.deltaTime;
		
		spawnMinion();
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
		if(!fireball)
		{
			Debug.Log("Failed to get fireball from object pool");
		}
		else
		{
			// Apply transform and rotation
			fireball.transform.position = mouthPos;
			fireball.transform.rotation = shootDir;
			fireball.SetActive(true);
		}
	}
	
	// Orb attack
	private void attack2()
	{
		// Spawn another orb after delay
    	if(orbEnable && !GameObject.FindGameObjectWithTag("Orb"))
    	{
			orbRespawnTimer += Time.deltaTime;
			
			if(orbRespawnTimer >= orbRespawnTime)
			{
				orbRespawnTimer = 0;
				Instantiate(Orb);
			}
		}
	}
	
	// Spawn minion imps
	private void spawnMinion()
	{
		minionRespawnTimer += Time.deltaTime;
		if(spawnMinions && minionRespawnTimer >= minionRespawnTime)
		{
			for(int i = 0; i < minionSpawnLocations.Length; i++)
			{
				minionRespawnTimer = 0;
				Instantiate(Minion, minionSpawnLocations[i], Quaternion.identity);
			}
		}
	}

	// Move towards player
	protected void move()
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
	
	// Called by hurt animation
	protected void deathCheck()
	{
		if(health <= 0)
		{
			activated = false;
			if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
			{
				SceneManager.LoadScene("Credits");
				animator.Play("Die");
			}
		}
		else
		{
			animator.Play("Run");
		}
	}
	
	// Take damage
	public void takeDamage(float amount)
	{
		// Decrease health
		health -= amount;
		if(health > maxHealth)
		{
			health = maxHealth;
		}
		
		healthBar.value = health;
		
		Debug.Log("Boss Health: " + health);

		// Play damage animation
		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("BossHurt")
		&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
		{
			animator.Play("BossHurt");
		}
		
		// Enable orb attack and minion spawning
		if(health <= healthBar.maxValue * 0.5f)
		{
			spawnMinions = true;
		}
		if(health <= healthBar.maxValue * 0.75f)
		{
			orbEnable = true;
		}
	}
}
