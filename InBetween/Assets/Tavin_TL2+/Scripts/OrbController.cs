using UnityEngine;

public class OrbController : MonoBehaviour
{
	// Characteristics
	[SerializeField] private float speed = 8f;
	
	private float deathTimer = 0;
	[SerializeField] private float deathTime = 3f;
	
	private Vector3 moveDir;

	private Animator animator;
	private bool isGenerating = true;
	
    // Player
	private GameObject Player;
	private Vector2 playerDir;
	private Vector3 playerCenterOffset;
	
	// Boss
	private GameObject Boss;
	private BossController BossScript;
	private Vector3 hornOffset = new Vector3(1.45f, -0.3f, 0f);
    
    // Start is called once before the first execution of Update after the
	// MonoBehaviour is created
    void Start()
    {
    	// Movement direction
 		moveDir = transform.up.normalized;
 		
 		// Get reference to player
		Player = GameObject.FindGameObjectWithTag("Player");
		
		// Get reference to boss
		Boss = GameObject.FindGameObjectWithTag("Boss");
		BossScript = Boss.GetComponent<BossController>();
        
		// Initiate animator
		animator = GetComponent<Animator>();
		
		// Move to starting position
		move();
    }

    // Update is called once per frame
    void Update()
    {   	
		move();
    	
    	// Destroy if alive for too long
    	deathTimer += Time.deltaTime;
    	if(deathTimer >= deathTime)
    	{
			destroy();
    	}
    }
    
    private void move()
    {
    	// Move to horn location
		if(isGenerating && BossScript.facingLeft)
		{
			transform.position = Boss.transform.position + hornOffset;	
    	}
    	else if(isGenerating && !BossScript.facingLeft)
    	{
    		transform.position = Boss.transform.position + new Vector3(-hornOffset.x, hornOffset.y, 0);
    	}
    	// Move in a straight line
    	else
    	{
    		transform.position += moveDir * Time.deltaTime * speed;
    	}
    }
    
    // Initiate shooting at player
    private void shoot()
    {
    	// Get player location
    	animator.Play("OrbShoot");
    	transform.localRotation = Quaternion.LookRotation(Vector3.forward, Player.transform.position - transform.position); ;
    	moveDir = transform.up;
    	isGenerating = false;
    	
    	// Tell the boss to spawn another orb
    	BossScript.attack2();
    }

	// Allows Destroy to be called on a keyframe in an animation
	private void destroy()
	{
		// Ensure that the next orb is spawned if this one hasn't finished generating
		if(isGenerating)
		{
			BossScript.attack2();
		}
		
		Destroy(gameObject);
	}

	// Handle player collisions
    void OnTriggerEnter2D(Collider2D collideObj)
    {
        if(collideObj.tag == "Player")
        {
        	// Deal damage to the player
//            collideObj.GetComponent<PlayerScript>().TakeDamage(5f);
			
			// Stop the fireball from moving while it's playing the animation
			speed = 0f;
//			animator.Play("OrbHit");
			destroy();
        }
    }
}
