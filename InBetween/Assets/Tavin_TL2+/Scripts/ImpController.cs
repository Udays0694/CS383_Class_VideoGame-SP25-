using UnityEngine;

public class ImpController : BossController
{
	// Options
	private const float attackCooldown = 1f;

	// Characteristics
//	private Slider healthBar;
	private Rigidbody2D rigidbody;

	// Attack
	private float updateMoveTimer = 0.5f;
	private float updateMoveTime = 0.5f;
	private Vector3 moveDir;
	private float shootDist = 2f;
	
	private float attackTimer = 0;
	[SerializeField] private GameObject fireball;

       // Start is called once before the first execution of Update after the
	// MonoBehaviour is created
    void Start()
    {
		// Initiate health bar
/*		healthBar = GetComponentInChildren<Slider>();
    	healthBar.maxValue = health;
		healthBar.value = health;
*/		

		activated = false;
		facingLeft = true;

		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.gravityScale = 0;
		rigidbody.freezeRotation = true;

		// Get animator
		animator = GetComponent<Animator>(); 
		spriteRenderer = GetComponent<SpriteRenderer>();

		// Get reference to player
		Player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    void Update()
    {
    	rigidbody.linearVelocity = Vector2.zero;
    	if(activated)
    	{
			// Calculate direction to player
			playerDir = Player.transform.position - transform.position;
			
			// Flip player if necessary
			if((playerDir.x < 0 && !facingLeft)
			|| (playerDir.x > 0 && facingLeft))
			{
				transform.Rotate(0, 180, 0);
				facingLeft = !facingLeft;
			}

			// Attack player
			move();

			attackTimer += Time.deltaTime;
		}
	}
	
	// Called at the end of the enemy spawn animation
	private void activate()
	{
		activated = true;
	}

	// Attack
	private void attack()
	{
		transform.Rotate(0, 180, 0);
		Instantiate(fireball, transform.position, transform.rotation);
		transform.Rotate(0, 180, 0);
	}

	// Random-ish movement
	private void move()
	{
		// Move to the side of the player
		if(Mathf.Abs(playerDir.x) <= shootDist)
		{
			playerDir.x = 0;
		}
		
		// Play run or attack animation
		if(Mathf.Abs(playerDir.x) <= shootDist && Mathf.Abs(playerDir.y) <= shootDist / 2 && attackTimer >= attackCooldown)
		{
			animator.Play("ImpAttack");
			attackTimer = 0;
		}
/*		else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("ImpAttack")
		     && !animator.GetCurrentAnimatorStateInfo(0).IsName("ImpSpawn")
		     && !animator.GetCurrentAnimatorStateInfo(0).IsName("ImpHurt")
		     && !animator.GetCurrentAnimatorStateInfo(0).IsName("ImpDie"))
		{
			animator.Play("ImpRun");
		}
*/		
		// Only update move direction every half second
		updateMoveTimer += Time.deltaTime;
		if(updateMoveTimer >= updateMoveTime)
		{
			// Reset timer and randomize update move time
			updateMoveTime = Random.Range(0.25f, 0.75f);
			updateMoveTimer = 0;
			
			// Round movement to nearest 45 degree angle
			float roundedAngle = Mathf.Round((Mathf.Atan2(playerDir.y, playerDir.x)
											 * Mathf.Rad2Deg) / 45f) * 45f * Mathf.Deg2Rad;
//			Debug.Log("Rounded angle: " + roundedAngle);			
			
			moveDir = new Vector3(Mathf.Cos(roundedAngle), Mathf.Sin(roundedAngle), 0);
//			Debug.Log("Rounded player vector: (" + moveDir.x + ", " + moveDir.y + ")");
		}
		
		// Calculate and apply movement
		transform.position += moveDir.normalized * Time.deltaTime * speed;
	}

	// Called by die animation
	private void destroy()
	{
		Destroy(gameObject);
	}

	// Check for death, called by hurt animation
/*	private void deathCheck()
	{
		if(health <= 0)
		{
			activated = false;
			if(!animator.GetCurrentAnimatorStateInfo(0).IsName("ImpDie"))
			{
				animator.Play("ImpDie");
			}
		}
		else
		{
			animator.Play("ImpRun");
		}
	}*/

	// Take damage
	public void takeDamage(float amount)
	{
		health -= amount;
//		healthBar.value = health;
		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("ImpHurt")
		&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
		{
			animator.Play("ImpHurt");
		}
	}
}
