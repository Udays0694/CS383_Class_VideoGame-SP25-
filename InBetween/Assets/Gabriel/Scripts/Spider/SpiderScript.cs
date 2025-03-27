using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SpiderScript : EnemyClass
{
    public string nameType = "Spider";
    public float movementSpeed = 5f;
    public float attackDamage = 15f;
    public float attackCooldown = 0.2f;
    public float attackCooldownTimer = 0f;
    public bool attackReady = true;
    public float xpAward = 5f;
    public float health = 40f;

    public Sprite Spider;
    public Sprite SpiderCharge;
    public Sprite SpiderLunge;

    protected override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (!attackReady)
        {
            attackCooldownTimer += Time.deltaTime;
            if (attackCooldownTimer >= attackCooldown / 1000f)
            {
                attackReady = true;
                attackCooldownTimer = 0f;
                base._spriteRenderer.sprite = Spider;
            }
        }
    }

    public override void Navigation()
    {
        //skeletonScript.getHealth();
        if (attackReady)
        {
            if (transform.position.x < base.playerScript.rb.position.x)
            {
                base._rb.linearVelocityX = movementSpeed;
                base._spriteRenderer.flipX = false;
            }
            else
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
        } else
        {
            base._rb.linearVelocityX = 0;
            base._rb.linearVelocityY = 0;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, base.playerScript.rb.position);
        if (distanceToPlayer < 1.5 && attackReady == true)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        //Debug.Log("Attacked player");
        base.DamagePlayer(attackDamage);
        attackReady = false;
        base._spriteRenderer.sprite = SpiderLunge;
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
