using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;

    private IShopCustomer shopCustomer;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(true);
    }

    private void Start()
    {
        CreateItemButton(Item.ItemType.SwordOne, Item.GetSprite(Item.ItemType.SwordTwo), "Iron Sword", Item.GetCost(Item.ItemType.SwordOne), 1);
        CreateItemButton(Item.ItemType.SwordTwo, Item.GetSprite(Item.ItemType.SwordTwo), "Gold Sword", Item.GetCost(Item.ItemType.SwordTwo), 1);
        CreateItemButton(Item.ItemType.AxeOne, Item.GetSprite(Item.ItemType.AxeOne), "Axe", Item.GetCost(Item.ItemType.AxeOne), 2);
        CreateItemButton(Item.ItemType.DaggerOne, Item.GetSprite(Item.ItemType.DaggerOne), "Dagger", Item.GetCost(Item.ItemType.DaggerOne), 3);
        CreateItemButton(Item.ItemType.PotionOne, Item.GetSprite(Item.ItemType.PotionOne), "Potion", Item.GetCost(Item.ItemType.PotionOne), 4);

        Hide();
    }

    //Works to reference ShopItemButton prefab and duplicates it for each item needed 
    private void CreateItemButton(Item.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container); //Placing each Shop Item 
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        
        float shopItemHeight = 70f; //Choosing where to place the first one, and then rest will stem from it 
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName); //Set Item Name 
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString()); //Set CostText 

        shopItemTransform.Find("itemSprite").GetComponent<Image>().sprite = itemSprite; //Set Item Image

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            //Clicking on shop item button 
            TryBuyItem(itemType);
        });
    }

    private void TryBuyItem(Item.ItemType itemType)
    {
        shopCustomer.BoughtItem(itemType);
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
