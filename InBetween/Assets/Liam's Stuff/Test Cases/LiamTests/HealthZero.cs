using UnityEngine;

public class HealthZero : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public SpriteRenderer sprite;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Vector2 movement;
    public float health = 100f;
    public int strength = 10;

    public bool moveUp = false;
    public bool moveDown = false;
    public bool moveLeft = false;
    public bool moveRight = false;
    public bool movementKeyPressed = false;

    public bool testDamageOnStart = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        if (testDamageOnStart)
        {
            Debug.Log("TEST: Applying fatal damage...");
            TakeDamage(999f); // Kill immediately
        }
    }

    void Update()
    {
        movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || moveUp) movement.y = 1;
        if (Input.GetKey(KeyCode.S) || moveDown) movement.y = -1;
        if (Input.GetKey(KeyCode.A) || moveLeft)
        {
            movement.x = -1;
            sprite.flipX = true;
        }
        if (Input.GetKey(KeyCode.D) || moveRight)
        {
            movement.x = 1;
            sprite.flipX = false;
        }

        movementKeyPressed = movement != Vector2.zero;

        if (_animator != null)
        {
            _animator.SetBool("isRunning", movementKeyPressed);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"Taking damage: {damage}");
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Player Died");
        Destroy(gameObject);
    }
}
