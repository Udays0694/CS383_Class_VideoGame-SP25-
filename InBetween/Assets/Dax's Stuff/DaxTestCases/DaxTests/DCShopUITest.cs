using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DCShopUITest
{
    private GameObject shopGameObject;
    private UI_Shop shop;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        //Load shop prefab from Resources
        GameObject prefab = Resources.Load<GameObject>("ShopKeeperEverything");
        Assert.IsNotNull(prefab, "ShopKeeperEverything.prefab not found.");

        shopGameObject = Object.Instantiate(prefab);

        shop = shopGameObject.GetComponentInChildren<UI_Shop>();
        Assert.IsNotNull(shop, "UI_Shop component not found in prefab children.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator ShopUI_OpensAndDisplaysFiveItems()
    {
        shop.Show(null);
        yield return null;

        var buttons = shopGameObject.GetComponentsInChildren<Button>(true);
        int activeCount = 0;
        foreach (var button in buttons)
        {
            if (button.gameObject.activeSelf)
                activeCount++;
        }

        Assert.AreEqual(5, activeCount, "Shop should show 5 items.");
    }

    [UnityTest]
    public IEnumerator ShopUI_ItemsAreDif()
    {
        shop.Show(null);
        yield return null;

        var buttons = shopGameObject.GetComponentsInChildren<Button>(true);
        HashSet<string> itemNames = new HashSet<string>();

        foreach (var button in buttons)
        {
            if (!button.gameObject.activeSelf) continue;

            var nameText = button.transform.Find("itemName")?.GetComponent<TextMeshProUGUI>();
            Assert.IsNotNull(nameText, "Missing itemName text on button.");

            string name = nameText.text;
            Assert.IsFalse(itemNames.Contains(name), $"Duplicate item found: {name}");
            itemNames.Add(name);
        }
    }

    [UnityTest]
    public IEnumerator ShopUI_HidesCorrectly()
    {
        shop.Show(null);
        yield return null;

        shop.Hide();
        yield return null;

        Assert.IsFalse(shopGameObject.activeSelf, "Shop GameObject should be inactive.");
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(shopGameObject);
        yield return null;
    }
}

