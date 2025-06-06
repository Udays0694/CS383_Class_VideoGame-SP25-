using UnityEngine;

public class ImpFireball : MonoBehaviour
{
	private bool activated = true;	
	private float speed = 2f;
	private float moveDir = 1;

	void Start()
	{
		Debug.Log(transform.localEulerAngles.y);
		if(transform.localEulerAngles.y > 175 && transform.localEulerAngles.y < 185)
		{
			// Moving left
			moveDir = -1;
		}
	}

	void Update()
	{	
		transform.position += new Vector3(moveDir, 0, 0) * Time.deltaTime * speed;
	}

    private void destroy()
    {
 //   	Debug.Log("destroying imp fireball");
    	Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D collideObj)
    {
        if(activated && (collideObj.tag == "Player" || collideObj.tag == "Room"))
        {
        	// Deal damage to the player
        	PlayerScript playerScript = collideObj.GetComponent<PlayerScript>();

            if(playerScript)
            {
            	playerScript.TakeDamage(4f);
            }
        	
        	// Stop fireball from moving
        	speed = 0;
        }
    }
}
