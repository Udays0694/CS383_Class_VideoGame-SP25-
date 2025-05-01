using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DaxPlayerAddOn : MonoBehaviour, IShopCustomer
{

    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI potionText;
    [SerializeField] private GameObject floatingTextPrefab;
    public int coinsCount = 0; // Player start with 0 coins 
    public int potionsCount = 1; //Player Starts with 1 healing potion

    private PlayerScript playerScript;

    private void Start()
    {
        UpdateCoinText();
        UpdatePotionText();

        playerScript = GetComponent<PlayerScript>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (potionsCount > 0)
            {
                SubtractPotion(1);
                GetComponent<PlayerScript>().HealME(25f);
            }
        }
    }
    public void AddCoins(int amount)
    {
        coinsCount += amount;
        StartCoroutine(showCoins(amount.ToString())); //Show floating text for added coins
        UpdateCoinText();
    }

    public void SubtractCoins(int amount)
    {
        if (coinsCount > 0)
        {
            coinsCount -= amount;

            if (coinsCount < 0)
            {
                coinsCount = 0;
            }

            UpdateCoinText();
        }
    }

    public void AddPotion(int amount)
    {
        potionsCount += amount;
        UpdatePotionText();
    }

    public void SubtractPotion(int amount)
    {
        if (potionsCount > 0)
        {
            potionsCount -= amount;

            if (potionsCount < 0)
            {
                potionsCount = 0;
            }

            UpdatePotionText();
        }
    }

    void UpdateCoinText()
    {
        if (coinsText != null)
        {
            coinsText.text = " " + coinsCount;
        }
    }

    void UpdatePotionText()
    {
        if (potionText != null)
        {
            potionText.text = " " + potionsCount;
        }
    }
    IEnumerator showCoins(string text)
    {
        if (floatingTextPrefab)
        {
            Vector3 offset = new Vector3(0, 2, 0); // Adjust the offset as needed
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position + offset, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;

            yield return new WaitForSeconds(1.04f); // Waits for 2 seconds before destroying the text

            Destroy(prefab);
        }
    }
    public bool TrySpendCoins(int spendCoins)
    {
        if(coinsCount >= spendCoins)
        {
            coinsCount -= spendCoins;
            coinsText.text = " " + coinsCount;
            return true; 
        }
        else
        {
            return false; 
        }
    }

    public void BoughtItem(Item.ItemType itemType)
    {
        Debug.Log("Bought item: " + itemType);

        switch(itemType)
        {
            case Item.ItemType.SwordOne: break;
            case Item.ItemType.SwordTwo: break;
            case Item.ItemType.AxeOne: break;
            case Item.ItemType.DaggerOne: break;
            case Item.ItemType.PotionOne: break; 
        }
    }
}
