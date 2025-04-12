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
    public int xpAward = 5;
    public float health = 40f;

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
            }
        }
    }

    public override void Navigation()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, base.playerScript.rb.position);
        if (distanceToPlayer < 1 && attackReady == true)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        //Debug.Log("Attacked player");
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
}
