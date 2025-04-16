using UnityEngine;

public class SpikeController : MonoBehaviour
{
	void Start()
	{
//		GetComponent<Rigidbody2D>().gravityScale = 0;
	} 

    // Called on collision
    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
        	// Deal damage to the player
            collider.gameObject.GetComponent<PlayerScript>().TakeDamage(5f);
        }
    }
}
