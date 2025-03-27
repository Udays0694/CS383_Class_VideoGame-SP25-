using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar2 : MonoBehaviour
{
    public Image healthBar;
    public PlayerScript playerScript;
    public float healthAmount;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        healthAmount = playerScript.health;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(15);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Healing(5);
        }

        UpdateHealthBar();
    }


    public void TakeDamage(float Damage)
    {
        healthAmount -= Damage;
        healthBar.fillAmount = healthAmount / 100;
    }

    public void Healing(float healPoints)
    {
        healthAmount += healPoints;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100;
    }

    public void UpdateHealthBar()
    {
        healthAmount = playerScript.health;
        healthBar.fillAmount = healthAmount / 100;
    }
}
