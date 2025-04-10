using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Slider xpSlider;  // The slider to represent XP
    public XP xpSystem;      // The XP system to get current XP value
    public LevelSystem levelSystem; // Level system to check the level progress

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure XP slider has the correct maximum value based on the level threshold
        if (xpSlider != null && levelSystem != null)
        {
            xpSlider.maxValue = levelSystem.levelThreshold;  // Set the max value based on the current level threshold
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update the slider value to reflect the current XP value
        if (xpSlider != null && xpSystem != null)
        {
            xpSlider.value = xpSystem.GetXP();  // Set the slider's value to the current XP
        }
    }
}
