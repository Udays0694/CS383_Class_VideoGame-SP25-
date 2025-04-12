using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public Transform attackOrigin;
    public float attackRange = 1.5f;
    public Vector2 attackBoxSize = new Vector2(1.5f, 1f);
    public float cooldownTime = 0.3f;
    private float cooldownTimer = 0f;

    public float attackDamage = 50f;
    private Vector2 attackDirection = Vector2.right;

    // Dict that sets the offset for tha attacks 
    private Dictionary<Vector2, Vector2> directionOffsets = new Dictionary<Vector2, Vector2>()
    {
        { Vector2.right, new Vector2(1f, 0f) },
        { Vector2.left,  new Vector2(-2f, 0f) },
        { Vector2.up,    new Vector2(0f, 2f) },
        { Vector2.down,  new Vector2(0f, -2f) }
    };

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            attackDirection = new Vector2(horizontal, vertical).normalized;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldownTimer <= 0f)
        {
            Attack();
            cooldownTimer = cooldownTime;
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");

        Vector2 origin = attackOrigin.position;

        //this snaps to a side
        Vector2 snappedDirection = SnapToCardinal(attackDirection);
        Vector2 offset = directionOffsets.ContainsKey(snappedDirection) ? directionOffsets[snappedDirection] * (attackRange / 2f) : snappedDirection * (attackRange / 2f);

        Vector2 boxCenter = origin + offset;
        float angle = GetAngleFromVector(snappedDirection);

        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, attackBoxSize, angle);

        foreach (var hit in hits) // finds if they have the enemy tag 
        {
            if (hit.CompareTag("Enemy"))
            {
                Debug.Log($"Hit: {hit.name}");
                hit.GetComponent<EnemyClass>().TakeDamage(attackDamage);
            }
        }
    }

    private Vector2 SnapToCardinal(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            return dir.x > 0 ? Vector2.right : Vector2.left;
        else
            return dir.y > 0 ? Vector2.up : Vector2.down;
    }

    private float GetAngleFromVector(Vector2 dir)
    {
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    private void OnDrawGizmosSelected() //debug 
    {
        if (attackOrigin == null) return;

        Vector2 origin = attackOrigin.position;
        Vector2 snappedDirection = SnapToCardinal(attackDirection);
        Vector2 offset = directionOffsets.ContainsKey(snappedDirection) ? directionOffsets[snappedDirection] * (attackRange / 2f) : snappedDirection * (attackRange / 2f);

        Vector2 boxCenter = origin + offset;
        float angle = GetAngleFromVector(snappedDirection);

        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, Quaternion.Euler(0, 0, angle), Vector3.one);
        Gizmos.DrawWireCube(Vector2.zero, attackBoxSize);
    }
}
