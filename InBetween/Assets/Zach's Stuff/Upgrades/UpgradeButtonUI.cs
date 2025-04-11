using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonUI : MonoBehaviour
{
    public TextMeshProUGUI upgradeNameText;
    public TextMeshProUGUI upgradeDescriptionText;
    public Image upgradeIcon;

    private string upgradeType;
    private UpgradeUIManager upgradeUIManager;

    public void Initialize(string name, string description, Sprite icon, string type, UpgradeUIManager manager)
    {
        upgradeNameText.text = name;
        upgradeDescriptionText.text = description;
        upgradeIcon.sprite = icon;

        upgradeType = type;
        upgradeUIManager = manager;

        // ✅ THIS IS THE IMPORTANT PART:
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        upgradeUIManager.SelectUpgrade(upgradeType);
    }
}
