using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DaxPlayerAddOn : MonoBehaviour, IShopCustomer
{

    public TextMeshProUGUI coinsText;

    [SerializeField] private GameObject floatingTextPrefab;

    public int coinsCount = 0; // Player start with 0 coins 

    private void Start()
    {
        UpdateCoinText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddCoins(1); // Adds 1 coin when C is pressed 
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            AddCoins(10); // Adds 1 coin when C is pressed 
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SubtractCoins(10); // Adds 1 coin when C is pressed 
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

    void UpdateCoinText()
    {
        if (coinsText != null)
        {
            coinsText.text = " " + coinsCount;
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
