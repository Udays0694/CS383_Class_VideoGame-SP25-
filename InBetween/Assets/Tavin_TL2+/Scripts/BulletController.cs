using UnityEngine;

public class BulletController : MonoBehaviour
{
	// Characteristics
	private float speed = 3f;
	private Vector2 moveDir;
	private float timer = 0;
	private float deathTimer = 0;
	
	// Player
	private GameObject Player;
	private Vector2 playerDir;

    // Start is called once before the first execution of Update after the
	// MonoBehaviour is created
    void Start()
    {
    	// Movement direction
 		moveDir = transform.up;
		
        transform.Rotate(0, 0, -90);
        
        // Get reference to player
		Player = GameObject.FindGameObjectWithTag("Player");
		
		// Initiate animator
//		_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    	// Animate flame
    	timer += Time.deltaTime;
    	if(timer >= 0.08f)
    	{
    		timer = 0;
    		transform.Rotate(180, 0, 0);
    	}
    	
    	// Move
    	move();
    	
    	// Destroy if alive for too long
    	deathTimer += Time.deltaTime;
    	if(deathTimer >= 5)
    	{
    		Destroy(gameObject);
    	}
    }

	// Move the bullet in whatever direction its facing
	private void move()
	{
		const float LERP_AMT = 0.03f;
		Vector3 playerDir = Player.transform.position - transform.position;
		moveDir = new Vector2(Mathf.Lerp(moveDir.x, playerDir.x, LERP_AMT * (1 / playerDir.magnitude)),
							  Mathf.Lerp(moveDir.y, playerDir.y, LERP_AMT * (1 / playerDir.magnitude)));
		Vector3 move = moveDir.normalized * Time.deltaTime * speed;
		
		// Generate quaternion 
		transform.localRotation = Quaternion.LookRotation(Vector3.forward, moveDir);

		// Apply the move
		move += transform.position;
//		move.y += transform.position.y;
		
		transform.position = move;
	}

	// Handle player collisions
    void OnTriggerEnter2D(Collider2D collideObj)
    {
        if(collideObj.tag == "Player")
        {
//            collideObj.GetComponent<PlayerScript>().TakeDamage(5f);
			Destroy(gameObject);
        }
    }
}
