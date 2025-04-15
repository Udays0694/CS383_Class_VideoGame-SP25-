using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ZombieScript : EnemyClass
{
    public string nameType = "Zombie";
    public float movementSpeed = 2f;
    public float attackDamage = 20f;
    public float attackCooldown = 0.4f;
    public float attackCooldownTimer = 0f;
    public bool attackReady = true;
    public int xpAward = 10;
    public float health = 20;

    public float spriteFlipCooldownTimer = 0.0f;
    public float spriteFlipCooldown = 0.5f;
    public bool spriteFlipReady = true;

    public Sprite Zombie;
    public Sprite ZombieCharge;
    public Sprite ZombieLunge;

    //lhelp get untsuck
    public Vector2 oldPosition = Vector2.zero;
    public bool doWander = false;
    public bool wandering = false;
    public bool movedOnce = false;

    public float targetX = 0;
    public float targetY = 0;

    // sound effect
    //public AudioSource zombieHit;
    //public AudioSource zombieDeath;
    
    protected override void Start()
    {
        base.Start();
        oldPosition = transform.position;
        //zombieDeath.Play();
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
                movedOnce = false;
                attackCooldownTimer = 0f;
                base._spriteRenderer.sprite = Zombie;
            }
        }
        if (!spriteFlipReady)
        {
            spriteFlipCooldownTimer += Time.deltaTime;
            if (spriteFlipCooldownTimer >= spriteFlipCooldown)
            {
                spriteFlipReady = true;
                spriteFlipCooldownTimer = 0f;
            }
        }
    }

    public void WanderMove()
    {
        float direction = 0;
        wandering = true;

        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            direction = 1;
            base._spriteRenderer.flipX = true;
        }
        else
        {
            direction = -1;
            base._spriteRenderer.flipX = false;
        }

        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            base._rb.linearVelocityX = direction * movementSpeed;
        }
        else
        {
            base._rb.linearVelocityY = direction * movementSpeed; ;
        }

        StartCoroutine(base.Delayed(() => ClearWanderingMovement(), UnityEngine.Random.Range(1f, 3f)));
    }

    public override void Navigation()
    {
        if (base.playerScript != null)
        {

            float distanceToPlayer = Vector2.Distance(transform.position, base.playerScript.rb.position);
            if (distanceToPlayer < 10f)
            {
                targetX = base.playerScript.rb.position.x;
                targetY = base.playerScript.rb.position.y;
                doWander = false;

            } else
            {
                base._rb.linearVelocityX = 0;
                base._rb.linearVelocityY = 0;
                doWander = true;
            }

            if ((Vector2.Distance(transform.position, oldPosition) < 0.0005) && movedOnce && attackReady && !doWander)
            {
                doWander = true;
            }

            if (doWander)
            {
                if (wandering == false)
                {
                    WanderMove();
                }
                doWander = false;
            }

            //zombieScript.getHealth();
            if (attackReady && !wandering)
            {
                if (transform.position.x < targetX)
                {
                    base._rb.linearVelocityX = movementSpeed;
                    if (spriteFlipReady)
                    {
                        base._spriteRenderer.flipX = false;
                        spriteFlipReady = false;
                    }
                }
                else
                {
                    base._rb.linearVelocityX = -1 * movementSpeed;
                    if (spriteFlipReady)
                    {
                        base._spriteRenderer.flipX = true;
                        spriteFlipReady = false;
                    }
                }

                if (transform.position.y < targetY)
                {
                    base._rb.linearVelocityY = movementSpeed;
                }
                else
                {
                    base._rb.linearVelocityY = -1 * movementSpeed;
                }
            }

            if (distanceToPlayer < 2 && attackReady == true && !wandering)
            {
                base._spriteRenderer.sprite = ZombieCharge;
            } else
            {
                base._spriteRenderer.sprite = Zombie;
            }

            if (distanceToPlayer < 1.5 && attackReady == true && !wandering)
            {
                Attack();
            }

            // check if moved
            if (!movedOnce)
            {
                if (Vector2.Distance(transform.position, oldPosition) > 0.01f)
                {
                    movedOnce = true;
                }
            }

            // update old
            oldPosition = transform.position;
        }
    }

    public void ClearWanderingMovement()
    {
        base._rb.linearVelocityX = 0;
        base._rb.linearVelocityY = 0;
        wandering = false;
    }

    public override void Attack()
    {
        //Debug.Log("Attacked player");
        base.DamagePlayer(attackDamage);
        attackReady = false;
        base._spriteRenderer.sprite = ZombieLunge;
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        // zombieHit.Play();
        if (health <= 0)
        {
            base.XPAward(xpAward);
        }
        //Debug.Log($"Zombie taking Damage - Current Health: {health}");
    }
}
