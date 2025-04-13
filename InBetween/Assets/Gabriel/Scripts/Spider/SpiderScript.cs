using UnityEngine;

public class SpiderScript : EnemyClass
{
    public string nameType = "Spider";
    public float movementSpeed = 5f;
    public float attackDamage = 15f;
    public float attackCooldown = 0.2f;
    public float attackCooldownTimer = 0f;
    public bool attackReady = true;
    public int xpAward = 5;
    public float health = 40f;

    private Vector2 moveDirection;

    protected override void Start()
    {
        base.Start();

        // Random starting direction
        moveDirection = Random.insideUnitCircle.normalized;

        // Set initial velocity
        _rb.linearVelocity = moveDirection * movementSpeed;
    }

    public override void Update()
    {
        base.Update();

        if (!attackReady)
        {
            attackCooldownTimer += Time.deltaTime;
            if (attackCooldownTimer >= attackCooldown)
            {
                attackReady = true;
                attackCooldownTimer = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        // Maintain constant movement in current direction
        _rb.linearVelocity = moveDirection * movementSpeed;
    }

    public override void Navigation()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerScript.rb.position);
        if (distanceToPlayer < 1.5 && attackReady)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        base.DamagePlayer(attackDamage);
        attackReady = false;
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            base.XPAward(xpAward);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            Vector2 normal = collision.contacts[0].normal;

            // Reflect direction on impact
            moveDirection = Vector2.Reflect(moveDirection, normal).normalized;
        }
    }
}
