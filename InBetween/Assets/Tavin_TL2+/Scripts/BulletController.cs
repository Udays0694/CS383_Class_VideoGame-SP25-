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

    // Start is called once before the first execution of Update after the
	// MonoBehaviour is created
    void Start()
    {
    	// Movement direction
 		moveDir = transform.up;
        
        // Get reference to player
		Player = GameObject.FindGameObjectWithTag("Player");
		
		// Find Player centerpoint offset
/*		playerCenterOffset = Player.GetComponent<SpriteRenderer>().bounds.size / 2;
		playerCenterOffset.y = -playerCenterOffset.y;
		playerCenterOffset.z = 0;
*/		
		// Initiate animator
		animator = GetComponent<Animator>();
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
		Destroy(gameObject);
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
			animator.Play("FireballHit");

        }
    }
}
