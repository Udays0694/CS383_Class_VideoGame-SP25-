using UnityEngine;

public class HealthMaxTest : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public SpriteRenderer sprite;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Vector2 movement;
    public float health = 100f;
    public float maxHealth = 100f;
    public int strength = 10;

    public bool moveUp = false;
    public bool moveDown = false;
    public bool moveLeft = false;
    public bool moveRight = false;
    public bool movementKeyPressed = false;

    public bool testHealingOnStart = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        if (testHealingOnStart)
        {
            Debug.Log("TEST: Trying to overheal...");
            Heal(25f);  // Try to heal when already at max
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

    public void Heal(float amount)
    {
        float oldHealth = health;
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        Debug.Log($"Healing Attempt: +{amount} | Before: {oldHealth} | After: {health}");
    }
}
