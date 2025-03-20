using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Vector2 movement;
    public float health = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from player
        movement.x = 0;
        movement.y = 0;

        if (Input.GetKey(KeyCode.W))
        {
            movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
        }
        
        // Normalize movement to prevent faster diagonal movement
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Move the player
        rb.linearVelocity = movement * moveSpeed;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
