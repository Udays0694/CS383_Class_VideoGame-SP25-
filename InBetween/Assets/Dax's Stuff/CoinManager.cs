using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    public Button addCoinButton;
    public Button subtractCoinButton;

    [SerializeField] private GameObject floatingTextPrefab;

    private int coinsCount = 0; // Player start with 0 coins 

    private void Start()
    {
        UpdateCoinText();

        //Assign Button Listeners
        addCoinButton.onClick.AddListener(() => AddCoins(10));
        subtractCoinButton.onClick.AddListener(() => subtractCoins(10));
    }

    private void Update()
    {
        if( Input.GetKeyDown(KeyCode.C))
        {
            AddCoins(1); // Adds 1 coin when C is pressed 
        }
    }
    void AddCoins(int amount)
    {
        coinsCount += amount;
        StartCoroutine(showCoins(amount.ToString())); //Show floating text for added coins
        UpdateCoinText();
    }

    void subtractCoins(int amount)
    {
        if (coinsCount > 0)
        {
            coinsCount -= amount;

            if(coinsCount < 0)
            {
                coinsCount = 0;
            }

            UpdateCoinText();
        }
    }

    void UpdateCoinText()
    {
        if( coinsText != null)
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



}
