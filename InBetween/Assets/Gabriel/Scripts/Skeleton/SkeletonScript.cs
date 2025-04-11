using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SkeletonScriptDataBC
{
    protected float health = 100f;

    public virtual float getHealth()
    {
        Debug.Log("setHealth called in super class");
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
        Debug.Log("setHealth called in sub class");
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

    public float xpAward = 10f;

    public float spriteFlipCooldownTimer = 0.0f;
    public float spriteFlipCooldown = 0.5f;
    public bool spriteFlipReady = true;

    public Sprite Skeleton;
    public Sprite SkeletonCharge;
    public Sprite SkeletonLunge;

    public SkeletonScriptDataBC skeletonScript;


    //lhelp get untsuck
    public Vector2 oldPosition = Vector2.zero;
    public bool Stuck = false;
    public GameObject NearestDoor = null;
    public bool movedOnce = false;

    protected override void Start()
    {
        base.Start();
        skeletonScript = new SkeletonScriptData();
        oldPosition = transform.position;
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

    GameObject FindNearestDoor()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        GameObject nearestDoor = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject door in doors)
        {
            Vector3 doorPosition = door.transform.position;
            float distance = Vector3.Distance(currentPosition, doorPosition);

            // Print each door's position and distance to this skeleton
            Debug.Log($"Door: {door.name} at {doorPosition} — Distance: {distance}");

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestDoor = door;
            }
        }

        if (nearestDoor != null)
        {
            Debug.Log($"Nearest door is: {nearestDoor.name} at {nearestDoor.transform.position}");
        }
        else
        {
            Debug.LogWarning("No doors found in scene.");
        }

        return nearestDoor;
    }



    public override void Navigation()
    {
        if (base.playerScript != null)
        {
            
            float targetX = base.playerScript.rb.position.x;
            float targetY = base.playerScript.rb.position.y;

            if ( (Vector2.Distance(transform.position, oldPosition) < 0.0005) && movedOnce && attackReady)
            {
                Stuck = true;
                NearestDoor = FindNearestDoor();
            }

            if (Stuck)
            {
                targetX = NearestDoor.transform.position.x;
                targetY = NearestDoor.transform.position.y;
            }

            //skeletonScript.getHealth();
            if (attackReady)
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
            else
            {
                base._rb.linearVelocityX = 0;
                base._rb.linearVelocityY = 0;
            }

            float distanceToPlayer = Vector2.Distance(transform.position, base.playerScript.rb.position);
            if (distanceToPlayer < 1.5 && attackReady == true)
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

    public override void Attack()
    {
        //Debug.Log("Attacked player");
        base.DamagePlayer(attackDamage);
        attackReady = false;
        base._spriteRenderer.sprite = SkeletonLunge;
    }

    public override void OnTakeDamage(float damage)
    {
        skeletonScript.setHealth(damage);
        if (skeletonScript.getHealth() <= 0)
        {
            base.XPAward(xpAward);
        }
    }
}
