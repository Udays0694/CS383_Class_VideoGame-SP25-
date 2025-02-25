using UnityEngine;

namespace Zach.Leveling
{
    public class XP : MonoBehaviour
    {
        private int xp = 0;
        private int level = 1;

        public void AddXP(int amount)
        {
            xp += amount;
        }

        public int GetLevel()
        {
            return level;
        }

        public int GetXP()
        {
            return xp;
        }
    }
}
