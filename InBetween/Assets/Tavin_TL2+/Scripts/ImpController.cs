using UnityEngine;
using UnityEngine.UI;

public class ImpController : MonoBehaviour
{
	// Options
	public float speed = 1.5f;
	public float health = 40f;
	private const float attackCooldown = 1f;

	// Characteristics
//	private Slider healthBar;
	private Animator animator;
	private SpriteRenderer sprite;
	private bool facingRight = true;

	// Attack
	private float updateMoveTimer = 0.5f;
	private Vector3 moveDir;
	private float shootDist = 2f;
	
	private float attackTimer = 0;
	[SerializeField] private GameObject fireball;

	// Player
	private GameObject Player;
	private Vector3 playerDir;

    // Start is called once before the first execution of Update after the
	// MonoBehaviour is created
    void Start()
    {
		// Initiate health bar
/*		healthBar = GetComponentInChildren<Slider>();
    	healthBar.maxValue = health;
		healthBar.value = health;
*/		
		// Get animator
		animator = GetComponent<Animator>(); 
		sprite = GetComponent<SpriteRenderer>();

		// Get reference to player
		Player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    void Update()
    {
		// Calculate direction to player
		playerDir = Player.transform.position - transform.position;
		
		// Flip player if necessary
		if((playerDir.x < 0 && facingRight)
		|| (playerDir.x > 0 && !facingRight))
		{
			transform.Rotate(0, 180, 0);
			facingRight = !facingRight;
		}

		// Attack player
		move();

		attackTimer += Time.deltaTime;
	}

	// Attack
	private void attack()
	{
		Instantiate(fireball, transform.position, transform.rotation);
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
		else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("ImpAttack"))
		{
			animator.Play("ImpRun");
		}
		
		// Only update move direction every half second
		updateMoveTimer += Time.deltaTime;
		if(updateMoveTimer >= 0.5f)
		{
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

	// Take damage
	public void takeDamage(float amount)
	{
		health -= amount;
//		healthBar.value = health;
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
