using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMelee : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public Transform attackOrigin;
    public float attackRadius = 1f;
    public LayerMask enemyMask;
 
    public float cooldownTime = .5f;
    private float cooldownTimer = 0f;
 
    public int attackDamage = 25;


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("InputTrigger");
            // Example of playing attack animation
            _animator.SetBool("isAttacking", true);
            StartCoroutine(ResetAttackAnimation());

            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, enemyMask);
            foreach (var enemy in enemiesInRange)
            {
                //enemy.GetComponent<HealthManager>().TakeDamage(attackDamage, transform.position);
            }


        }
    }

    private IEnumerator ResetAttackAnimation()
    {
        yield return new WaitForSeconds(0.45f); // adjust based on animation length
        _animator.SetBool("isAttacking", false);
    }




    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }
}