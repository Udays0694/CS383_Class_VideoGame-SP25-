using UnityEngine;

public class PlayerScriptDataBC
{
    protected float health = 100f;

    public virtual float getHealth()
    {
        return health;
    }

    public virtual void setHealth(float damage)
    {
        Debug.Log("setHealth called in super class");
        health -= 5f;
    }
}

public class PlayerScriptData : PlayerScriptDataBC
{
    public override void setHealth(float damage)
    {
        Debug.Log("setHealth called in sub class");
        base.health -= damage;
    }
}

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Vector2 movement;
    public float health = 0f;
    public int strength = 10; 


    public PlayerScriptDataBC playerScript;

    //demo move settings
    public bool moveUp = false;
    public bool moveDown = false;
    public bool moveLeft = false;
    public bool moveRight = false;
    public bool movementKeyPressed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScript = new PlayerScriptData();
    }

    void Update()
    {
        // Get input from player
        movement.x = 0;
        movement.y = 0;

        if (Input.GetKey(KeyCode.W) || moveUp == true)
        {
            movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S) || moveDown == true)
        {
            movement.y = -1;
        }
        if (Input.GetKey(KeyCode.A) || moveLeft == true)
        {
            movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D) || moveRight == true)
        {
            movement.x = 1;
        }
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            movementKeyPressed = true;
            _animator.SetBool("isRunning", true);
        } else
        {
            movementKeyPressed = false;
            _animator.SetBool("isRunning", false);
        }

       
        // Normalize movement to prevent faster diagonal movement
        movement = movement.normalized;
        health = playerScript.getHealth();
    }

    void FixedUpdate()
    {
        // Move the player
        rb.linearVelocity = movement * moveSpeed;
    }

    public void TakeDamage(float damage)
    {
        playerScript.setHealth(damage);
        if (playerScript.getHealth() <= 0)
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
