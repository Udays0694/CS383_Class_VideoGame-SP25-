using UnityEngine;

public class SkeletonScript : EnemyClass
{
    public string nameType = "Skeleton";
    public float health = 100;
    public float movementSpeed = 3f;
    public float attackDamage = 30f;
    public float xpAward = 10f;



    public override void Navigation()
    {
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
        health -= damage;
        if (health <= 0)
        {
            base.XPAward(xpAward);
        }
    }
}
