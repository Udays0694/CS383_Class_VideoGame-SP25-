using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int MaxHealth = 3;
    public int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = MaxHealth;
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        }
    }

    void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        
    }
}
