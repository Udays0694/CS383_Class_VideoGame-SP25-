using UnityEngine;

public class BulletController : MonoBehaviour
{
	// Characteristics
	private float speed = 3f;
	private Vector2 moveDir;
	private float deathTimer = 0;
	private Animator animator;
	
	// Player
	private GameObject Player;
	private Vector2 playerDir;
	private Vector3 playerCenterOffset;

	// Called at instantiation
	void Start()
	{
		// Initiate animator
		animator = GetComponent<Animator>();
		
		// Get reference to player
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Called at the start and when it is activated
    void OnEnable()
    {
    	// Movement direction
 		moveDir = transform.up;
 		
 		animator.Play("FireballBurn");
 		
 		deathTimer = 0;
 		speed = 3;
    }

    // Update is called once per frame
    void Update()
    {   	
    	// Move
    	move();
    	
    	// Destroy if alive for too long
    	deathTimer += Time.deltaTime;
    	if(deathTimer >= 5)
    	{
    		disappear();
    	}
    }

	// Move the bullet in whatever direction its facing
	private void move()
	{
		// Lag behind player position to make the "heatseeking" effect more natural
		const float LERP_AMT = 0.03f;
		playerDir = Player.transform.position /*+ playerCenterOffset*/ - transform.position;
		moveDir = new Vector2(Mathf.Lerp(moveDir.x, playerDir.x, LERP_AMT * (1 / playerDir.magnitude)),
							  Mathf.Lerp(moveDir.y, playerDir.y, LERP_AMT * (1 / playerDir.magnitude)));
		Vector3 move = moveDir.normalized * Time.deltaTime * speed;
		
		// Generate quaternion 
		transform.localRotation = Quaternion.LookRotation(Vector3.forward, moveDir);

		// Apply the move
		move += transform.position;
		transform.position = move;
	}

	// Play animation and destroy object
	private void disappear()
	{
		animator.Play("FireballDisappear");
	}
	
	// Allows Destroy to be called on a keyframe in an animation
	private void destroy()
	{
		gameObject.SetActive(false);
//		Destroy(gameObject);
	}

	// Handle player collisions
    void OnTriggerEnter2D(Collider2D collideObj)
    {
        if(collideObj.tag == "Player")
        {
        	// Deal damage to the player
            collideObj.GetComponent<PlayerScript>().TakeDamage(5f);
			
			// Stop the fireball from moving while it's playing the animation
			speed = 0f;
//			transform.localRotation = Quaternion.LookRotation(Vector3.forward, playerDir);
			if(!animator.GetCurrentAnimatorStateInfo(0).IsName("FireballDisappear"))
			{
				animator.Play("FireballHit");
			}
        }
    }
}
