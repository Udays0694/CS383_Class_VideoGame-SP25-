using System.Collections.Generic;
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
        }
    }
}
