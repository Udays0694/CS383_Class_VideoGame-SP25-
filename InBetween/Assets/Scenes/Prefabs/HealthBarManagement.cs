using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar2 : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100;



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
}
