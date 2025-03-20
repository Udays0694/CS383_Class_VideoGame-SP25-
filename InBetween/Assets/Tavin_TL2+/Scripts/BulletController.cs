using UnityEngine;

public class BulletController : MonoBehaviour
{
	// Characteristics
	private float speed = 3f;

    // Start is called once before the first execution of Update after the
	// MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	move();
    }

	// Move the bullet in whatever direction its facing
	private void move()
	{
		Vector2 moveDir = transform.up;
		moveDir = moveDir.normalized * Time.deltaTime * speed;

		moveDir.x += transform.position.x;
		moveDir.y += transform.position.y;
		
		transform.position = moveDir;
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
