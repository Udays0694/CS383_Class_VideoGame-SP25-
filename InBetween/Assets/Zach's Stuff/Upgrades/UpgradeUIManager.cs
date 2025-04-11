using System.Collections.Generic;
using UnityEngine;

public class UpgradeUIManager : MonoBehaviour
{
    public GameObject upgradeButtonPrefab;
    public Transform buttonContainer;
    public GameObject panel;

    public Sprite speedIcon;
    public Sprite strengthIcon;
    public Sprite healthIcon;

    private UpgradeSystem upgradeSystem;

    void Start()
    {
        panel.SetActive(false);
        upgradeSystem = FindFirstObjectByType<UpgradeSystem>();
    }

    public void ShowUpgradeOptions()
    {
        Time.timeScale = 0f; // ⏸ Pause the game
        panel.SetActive(true);
        ClearButtons();

        string[] allUpgrades = { "Speed", "Strength", "Health" };
        List<string> options = new List<string>();

        while (options.Count < 2)
        {
            string candidate = allUpgrades[Random.Range(0, allUpgrades.Length)];
            if (!options.Contains(candidate)) options.Add(candidate);
        }

        foreach (string upgrade in options)
        {
            Sprite icon = GetUpgradeIcon(upgrade);
            string description = upgradeSystem.GetUpgradeDescription(upgrade);
            GameObject btn = Instantiate(upgradeButtonPrefab, buttonContainer);
            btn.GetComponent<UpgradeButtonUI>().Initialize(upgrade, description, icon, upgrade, this);
        }
    }

    private Sprite GetUpgradeIcon(string upgradeType)
    {
        switch (upgradeType)
        {
            case "Speed": return speedIcon;
            case "Strength": return strengthIcon;
            case "Health": return healthIcon;
            default: return null;
        }
    }

    private void ClearButtons()
    {
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void SelectUpgrade(string upgradeType)
    {
        upgradeSystem.AwardUpgrade(upgradeType);
        HideUpgradeOptions();
    }

    private void HideUpgradeOptions()
    {
        ClearButtons();
        panel.SetActive(false);
        Time.timeScale = 1f; // ▶️ Unpause the game
    }
}
