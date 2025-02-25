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
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                GainXP(10); // On 'x' input give xp for testing
            }
        }

        public void GainXP(int amount) //will give xp upon enemy death checks for lvl up
        {
            xpSystem.AddXP(amount);
            if (xpSystem.GetXP() >= levelThreshold)
            {
                LevelUp();
            }
        }

        private void LevelUp() //increases player stats
        {
            level++;
            xpSystem.AddXP(-levelThreshold);
            levelThreshold += 50; // increasese lvl up threshold

            
            IncreasePlayerStats();
            Debug.LogWarning("Player Leveled Up!");
        }

        private void IncreasePlayerStats()
        {
            
            PlayerBuffs playerStats = GetComponent<PlayerBuffs>(); 
            if (playerStats != null)
            {
                playerStats.IncreaseStats(10f, 2, 2); // increment player stats
            }
            else
            {
                Debug.LogWarning("PlayerBuffs component not found on the GameObject.");
            }
        }
    }
}
