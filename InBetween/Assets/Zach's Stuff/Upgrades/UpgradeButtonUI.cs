/*using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeButtonUI : MonoBehaviour
{
    public TMP_Text upgradeNameText;
    private string upgradeType;
    private UpgradeUIManager manager;

    public void Initialize(string type, UpgradeUIManager uiManager)
    {
        upgradeType = type;
        upgradeNameText.text = type;
        manager = uiManager;
    }

    public void OnClick()
    {
        manager.SelectUpgrade(upgradeType);
    }
}
*/