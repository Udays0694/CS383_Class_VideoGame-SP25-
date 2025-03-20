using UnityEngine;

public class PlayerBuffs : MonoBehaviour
{
    public float health = 100f;
    public int strength = 10;
    public int agility = 10;

    public void IncreaseStats(float healthIncrease, int strengthIncrease, int agilityIncrease)
    {
        health += healthIncrease;
        strength += strengthIncrease;
        agility += agilityIncrease;
    }
}
