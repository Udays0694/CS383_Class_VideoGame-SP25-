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
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
	private float attack1Timer = 0;
	[SerializeField] private GameObject Bullet1;
	private bool facingLeft = true;
	private Vector2 mouthPos;

	// Player
	private GameObject Player;
	private Vector2 playerDir;

    // Start is called once before the first execution of Update after the
	// MonoBehaviour is created
    void Start()
    {
		// Initiate health bar
		healthBar = GetComponentInChildren<Slider>();
    	healthBar.maxValue = health + 2f;
		healthBar.value = health;
		
		// Get reference to player
		Player = GameObject.FindGameObjectWithTag("Player");
		
		// Get reference to animator
		_animator = GetComponent<Animator>();
		
		// Get reference to sprite renderer
		_spriteRenderer = GetComponent<SpriteRenderer>();
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
		playerDir = Player.transform.position - (transform.position + new Vector3(centerDiff, -2.5f, 0));
		
		// Face the player
		if(facingLeft && playerDir.x > 0)
		{
			transform.position += new Vector3(flipTransformDist, 0, 0);
//			_spriteRenderer.flipX = true;
			healthBar.transform.Rotate(0, 180, 0);
			transform.Rotate(0, 180, 0);
			facingLeft = false;
		}
		else if(!facingLeft && playerDir.x < 0)
		{
			transform.position += new Vector3(-flipTransformDist, 0, 0);
//			_spriteRenderer.flipX = false;
			healthBar.transform.Rotate(0, 180, 0);
			transform.Rotate(0, 180, 0);
			facingLeft = true;
		}

		// Attack player
		chasePlayer();
        if(attack1Timer >= attack1Cooldown)
		{
			_animator.Play("BossAttack");
			attack1Timer = 0;
		}

		attack1Timer += Time.deltaTime;
	}

	// Base attack
	public void attack1()
	{
		// Generate quaternion for spawn rotation of bullet
		int xDir = 1;
		if(facingLeft)
		{
			xDir = -1;
		}
		Quaternion shootDir = Quaternion.LookRotation(Vector3.forward, new Vector3(xDir, 0, 0));
		
		// Spawn bullet
		Instantiate(Bullet1, mouthPos, shootDir);
	}

	// Move towards player
	private void chasePlayer()
	{
		// Move away from player if its too close
		Vector3 moveDir = playerDir;
/*		if(playerDir.magnitude < 3.5f)
		{
			moveDir = -moveDir;
		}
*/		
		// Calculate move distance
		Vector3 move;
		move = transform.position + moveDir.normalized * Time.deltaTime * speed;

		// Apply movement
		transform.position = move;
	}

	// Take damage
	public void takeDamage(float amount)
	{
		// Decrease health
		health -= amount;
		healthBar.value = health;
		
		// Die
		if(health <= 0)
		{
			_animator.Play("BossDie");
			Destroy(gameObject);
		}
		// Play damage animation
		else
		{
			_animator.Play("BossHurt");
		}
	}
}
