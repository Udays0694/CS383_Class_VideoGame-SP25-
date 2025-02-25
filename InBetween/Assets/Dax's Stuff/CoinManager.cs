using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    public Button addCoinButton;
    public Button subtractCoinButton;

   // [SerializeField] private GameObject floatingTextPrefab;

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
        //showCoins(amount.ToString()); //Show floating text for added coins
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
    //void showCoins(string text)
    //{
     //   if(floatingTextPrefab)
     //   {
     //       GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
      //      prefab.GetComponentInChildren<TextMesh>().text = text;
      //  }  
   // }

}
