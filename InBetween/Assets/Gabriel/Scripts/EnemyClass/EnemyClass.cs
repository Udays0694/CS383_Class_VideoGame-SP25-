using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class EnemyClass : MonoBehaviour
{
    // Player script
    public PlayerScript playerScript = null;

    // Rigid body
    public Rigidbody2D _rb = null;

    // Animate
    public SpriteRenderer _spriteRenderer = null;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerScript>();
        }
        else
        {
            Debug.LogWarning("No player found. The enemy will not be able to interact with the player.");
        }
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
