using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyClass : MonoBehaviour
{
    // Player script
    public PlayerScript playerScript = null;

    // Rigid body
    public Rigidbody2D _rb = null;

    // Animate
    public SpriteRenderer _spriteRenderer = null;

    public GameObject XPBar = null;
    public XP xp;

    // player
    public GameObject player;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        XPBar = GameObject.FindGameObjectWithTag("XPBar");

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerScript>();
        }
        else
        {
            Debug.LogWarning("No player found. The enemy will not be able to interact with the player.");
        }
    }
    protected IEnumerator Delayed(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }


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
        //Debug.Log($"Damaged Player for: {damage}");
        playerScript.TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage)
    {
        // Class specific
    }

    public void XPAward(int xp_amount)
    {
        if (player != null){
            XPBar.GetComponent<XP>().AddXP(xp_amount);
        }
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
