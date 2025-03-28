using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class EnemyClass : MonoBehaviour
{
    // Player script
    public PlayerScript playerScript;

    // Rigid body
    public Rigidbody2D _rb = null;

    // Animate
    public SpriteRenderer _spriteRenderer = null;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        _rb.freezeRotation = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Navigation();
    }

    public virtual void Navigation()
    {
        // Class specifc navigation
    }

    public void DamagePlayer(float damage)
    {
        playerScript.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    { 
        OnTakeDamage(damage);
    }

    public virtual void OnTakeDamage(float damage)
    {
        // Class specific health here
    }

    public void XPAward(float xp_amount)
    {
        Death();
    }

    void Death()
    {
        Destroy(gameObject);
    }

    public virtual void Attack()
    {
        // Class specific attack here
    }
}
