using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SkeletonScriptDataBC
{
    protected float health = 100;

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
    public float xpAward = 10f;

    public SkeletonScriptDataBC skeletonScript;

    protected override void Start()
    {
        base.Start();
        skeletonScript = new SkeletonScriptData();
    }

    public override void Navigation()
    {
        //skeletonScript.getHealth();
        if (transform.position.x < base.playerScript.rb.position.x)
        {
            base._rb.linearVelocityX = movementSpeed;
            base._spriteRenderer.flipX = false;
        } else
        {
            base._rb.linearVelocityX = -1 * movementSpeed;
            base._spriteRenderer.flipX = true;
        }

        if (transform.position.y < base.playerScript.rb.position.y)
        {
            base._rb.linearVelocityY = movementSpeed;
        }
        else
        {
            base._rb.linearVelocityY = -1 * movementSpeed;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, base.playerScript.rb.position);
        if (distanceToPlayer < 3)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        base.DamagePlayer(attackDamage);
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
