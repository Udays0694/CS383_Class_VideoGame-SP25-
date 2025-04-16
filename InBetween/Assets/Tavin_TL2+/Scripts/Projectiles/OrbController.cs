using UnityEngine;

public class OrbController : MonoBehaviour
{
	// Characteristics
	private float speed = 10f;
	
	private float deathTimer = 0;
	private float deathTime = 10f;
	
	private Vector3 moveDir;

	private Animator animator;
	private bool isGenerating = true;
	
    // Player
	private GameObject Player;
	private Vector2 playerDir;
	
	// Boss
	private GameObject Boss;
	private BossController BossScript;
	private Vector3 hornOffset = new Vector3(1.45f, -0.3f, 0f);
    
    // Start is called once before the first execution of Update after the
	// MonoBehaviour is created
    void Start()
    {
    	// Singleton functionality
    	if(GameObject.FindGameObjectWithTag("Orb"))
    	{
    		Destroy(gameObject);
    	}
    	
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
    }

	// Allows Destroy to be called on a keyframe in an animation
	private void destroy()
	{
		Destroy(gameObject);
	}

	// Handle player collisions
    void OnTriggerEnter2D(Collider2D collideObj)
    {
        if(collideObj.tag == "Player" || collideObj.tag == "Room")
        {
        	// Deal damage to the player
        	PlayerScript playerScript = collideObj.GetComponent<PlayerScript>();

            if(playerScript)
            {
            	playerScript.TakeDamage(10f);
            }
			
			speed /= 2f;

			animator.Play("OrbHit");
        }
    }
}
