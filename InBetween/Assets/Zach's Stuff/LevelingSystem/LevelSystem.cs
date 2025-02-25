using UnityEngine;

namespace Zach.Leveling
{
    public class LevelSystem : MonoBehaviour
    {
        public XP xpSystem;
        public int levelThreshold = 100;
        public int level = 1;

        void Start()
        {
            xpSystem = GetComponent<XP>();
            if (xpSystem == null)
            {
                Debug.LogError("XP component not found on the GameObject.");
            }
        }

        void Update()
        {
            // Check for 'X' key press
            if (Input.GetKeyDown(KeyCode.X))
            {
                GainXP(10); // Award 10 XP for each press
            }
        }

        public void GainXP(int amount)
        {
            xpSystem.AddXP(amount);
            if (xpSystem.GetXP() >= levelThreshold)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            level++;
            xpSystem.AddXP(-levelThreshold);
            levelThreshold += 50; // Increase next level requirement

            // Increase player stats upon leveling up
            IncreasePlayerStats();
            Debug.LogWarning("Player Leveled Up!");
        }

        private void IncreasePlayerStats()
        {
            // Implement stat increases here
            // For example, if you have a PlayerStats component:
            PlayerBuffs playerStats = GetComponent<PlayerBuffs>();
            if (playerStats != null)
            {
                playerStats.IncreaseStats(10f, 2, 2); // Example increments
            }
            else
            {
                Debug.LogWarning("PlayerBuffs component not found on the GameObject.");
            }
        }
    }
}
