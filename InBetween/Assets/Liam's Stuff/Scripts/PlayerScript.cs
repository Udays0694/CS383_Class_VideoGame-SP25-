using UnityEngine;
using UnityEngine.SceneManagement;
//This entire area of the project took me about 35-40 hours, this was due to some reworks and certain things not taking as long as expected.
//Reworking code or communicating with partners was probably the greatest holdup as we were unable to have meetings as frequently as we should have. 
//I estimated it would take around 46 hours.I learned that typically in these scenarios when I am estimating, it is that typically I tend to overestimate heavily, especially when dealing with a project of this magnitude.

public class PlayerScriptDataBC
{
    protected float health = 100f; //Private Class Data Pattern example, as it uses encapsulation to protect interal data
    public float healbase = 20f;
    public virtual float getHealth() //Health is not directly exposed, rather it is accessed via methods.
    {
        return health;
    }

    public virtual void setHealth(float damage)
    {
        Debug.Log($"PLAYER getHealth called in super class ? current health: {health}");
        health -= 5f;
    }
}

public class PlayerScriptData : PlayerScriptDataBC
{
    public override void setHealth(float damage)
    {
        Debug.Log($"PLAYER getHealth called in sub class ? current health: {base.health}");
        base.health -= damage;
    }
}

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public SpriteRenderer Sprite;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Vector2 movement;
    public float health = 0f; //updated to dynamic binding version
    public int strength = 10;


    public PlayerScriptDataBC playerScript;

    //demo move settings
    public bool moveUp = false;
    public bool moveDown = false;
    public bool moveLeft = false;
    public bool moveRight = false;
    public bool movementKeyPressed = false;

    void Start() //This is part of the Strategy pattern as it works within the use of PlayerScriptDataBC and PlayerScriptData polymorphically in PlayerScript. 
    { //Used to define a family of algorithms/behaviors, in this case alllowing for things such as setHealth and getHealth to be swapped at runtime, with a subclass implementation such as PlayerScriptData.
        rb = GetComponent<Rigidbody2D>();
        playerScript = new PlayerScriptData();
        Sprite = GetComponent<SpriteRenderer>();
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
            Sprite.flipX = true;
        }
        if (Input.GetKey(KeyCode.D) || moveRight == true)
        {
            movement.x = 1;
            Sprite.flipX = false;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            movementKeyPressed = true;
            _animator.SetBool("isRunning", true);
        }
        else
        {
            movementKeyPressed = false;
            _animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(20);
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
        //GameObject.Find("DamageSound").GetComponent<AudioSource>().Play();
        if (playerScript.getHealth() <= 0)
        {
            _animator.SetBool("isDead", true);
            Death();
        }
    }

    public void HealME(float healbase)
    {
        playerScript.setHealth(healbase);
        if (playerScript.getHealth() >= health)
        {
            playerScript.setHealth(health);
        }
    }

    void Death()
    {
        Debug.Log("Player Died");
        _animator.SetBool("isDead", true);
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }


}
