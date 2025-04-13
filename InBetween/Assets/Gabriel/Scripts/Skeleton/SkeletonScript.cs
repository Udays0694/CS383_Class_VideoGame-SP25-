using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SkeletonScriptDataBC
{
    protected float health = 100f;

    public virtual float getHealth()
    {
        Debug.Log($"getHealth called in super class � current health: {health}");
        health = 0;
        return health;
    }

    public virtual void setHealth(float damage)
    {
        health = 0;
    }

    public static implicit operator SkeletonScriptDataBC(SkeletonScript v)
    {
        throw new NotImplementedException();
    }
}

public class SkeletonScriptData : SkeletonScriptDataBC
{
    public override float getHealth()
    {
        //Debug.Log($"getHealth called in sub class � current health: {base.health}");
        return base.health;
    }

    public override void setHealth(float damage)
    {
        base.health -= damage;
    }
}

public class SkeletonScript : EnemyClass
{
    public string nameType = "Skeleton";
    public float movementSpeed = 3f;
    public float attackDamage = 30f;
    public float attackCooldown = 0.4f;
    public float attackCooldownTimer = 0f;
    public bool attackReady = true;

    public int xpAward = 10;

    public float spriteFlipCooldownTimer = 0.0f;
    public float spriteFlipCooldown = 0.5f;
    public bool spriteFlipReady = true;

    public Sprite Skeleton;
    public Sprite SkeletonCharge;
    public Sprite SkeletonLunge;

    public SkeletonScriptDataBC skeletonScript;


    //lhelp get untsuck
    public Vector2 oldPosition = Vector2.zero;
    public bool doWander = false;
    public bool wandering = false;
    public bool movedOnce = false;

    public float targetX = 0;
    public float targetY = 0;

    // sound effect
    public AudioSource skeletonHit;
    public AudioSource skeletonDeath;
    
    protected override void Start()
    {
        base.Start();
        oldPosition = transform.position;
        skeletonDeath.Play();
    }

    public void Awake()
    {
        skeletonScript = new SkeletonScriptData();
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
                base._spriteRenderer.sprite = Skeleton;
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

            //skeletonScript.getHealth();
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
                base._spriteRenderer.sprite = SkeletonCharge;
            } else
            {
                base._spriteRenderer.sprite = Skeleton;
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
        base._spriteRenderer.sprite = SkeletonLunge;
    }

    public override void TakeDamage(float damage)
    {
        Debug.Log("Taking Damage");
        skeletonScript.setHealth(damage);
        skeletonHit.Play();
        if (skeletonScript.getHealth() <= 0)
        {
            base.XPAward(xpAward);
        }
    }
}
