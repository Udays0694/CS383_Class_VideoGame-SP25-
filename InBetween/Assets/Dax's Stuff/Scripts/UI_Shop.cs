using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UI_Shop : MonoBehaviour
{
    //Stuff For Shop UI
    private Transform container;
    private Transform shopItemTemplate;

    //Purchase objects & Open Shop 
    private IShopCustomer shopCustomer;

    //Access to health potions and ability to heal 
    private DaxPlayerAddOn daxPlayerAddOn;

    //Inventory System Changes
    public Image weaponSlot;
    public Image armorSlot;
    public Image shieldSlot;

    //Access to Inventory Script 
    public InventorySystem inventorySystem;

    private List<Item.ItemType> GetAllItemTypes()
    {
        return new List<Item.ItemType>((Item.ItemType[])System.Enum.GetValues(typeof(Item.ItemType))); //Builds the item list
    }
    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(true);

        if(inventorySystem == null)
        {
            inventorySystem = FindObjectOfType<InventorySystem>();
        }
    }

    private void Start()
    {
        CreateItemButton(Item.ItemType.Sword, Item.GetSprite(Item.ItemType.Sword), "Iron Sword", Item.GetCost(Item.ItemType.Sword), 0, Item.GetEffectValue(Item.ItemType.Sword), Item.GetDescription(Item.ItemType.Sword));
        CreateItemButton(Item.ItemType.Sword, Item.GetSprite(Item.ItemType.Sword), "Gold Sword", Item.GetCost(Item.ItemType.Sword), 1, Item.GetEffectValue(Item.ItemType.Sword), Item.GetDescription(Item.ItemType.Sword));
        CreateItemButton(Item.ItemType.Axe, Item.GetSprite(Item.ItemType.Axe), "Axe", Item.GetCost(Item.ItemType.Axe), 2, Item.GetEffectValue(Item.ItemType.Axe), Item.GetDescription(Item.ItemType.Axe));
        CreateItemButton(Item.ItemType.Dagger, Item.GetSprite(Item.ItemType.Dagger), "Dagger", Item.GetCost(Item.ItemType.Dagger), 3, Item.GetEffectValue(Item.ItemType.Dagger), Item.GetDescription(Item.ItemType.Dagger));
        CreateItemButton(Item.ItemType.PotionOne, Item.GetSprite(Item.ItemType.PotionOne), "Potion", Item.GetCost(Item.ItemType.PotionOne), 4, Item.GetEffectValue(Item.ItemType.PotionOne), Item.GetDescription(Item.ItemType.PotionOne));

        Hide();

        daxPlayerAddOn = GameObject.FindWithTag("Player").GetComponent<DaxPlayerAddOn>();
    }

    //Works to reference ShopItemButton prefab and duplicates it for each item needed 
    private void CreateItemButton(Item.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex, int effectValue, string description)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container); //Placing each Shop Item 
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        
        float shopItemHeight = 70f; //Choosing where to place the first one, and then rest will stem from it 
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName); //Set Item Name 
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString()); //Set CostText 

        shopItemTransform.Find("itemSprite").GetComponent<Image>().sprite = itemSprite; //Set Item Image

        shopItemTransform.Find("descriptionText").GetComponent<TextMeshProUGUI>().SetText(description);

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            //Clicking on shop item button 
            TryBuyItem(itemType);
        });
    }

    private void TryBuyItem(Item.ItemType itemType)
    {
        if(shopCustomer.TrySpendCoins(Item.GetCost(itemType))) //Checks if the player has enough coins for the item
        {
            shopCustomer.BoughtItem(itemType);

            if(itemType == Item.ItemType.PotionOne)
            {

                daxPlayerAddOn.AddPotion(1);
            }
            else
            {
                Item.ItemCategory category = Item.GetCategory(itemType);
                Sprite sprite = Item.GetSprite(itemType);

                inventorySystem.UpdateInventorySlot(category, sprite);
            }
        }
        else
        {
            Debug.Log("Can Not Afford Item");
        }
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);

        List<Transform> toDelete = new List<Transform>();
        foreach (Transform child in container) //Clears previous shop items
        {
            if (child != shopItemTemplate) //Stops from removing template  
            {
                toDelete.Add(child);
            }
        }
        foreach (Transform child in toDelete)
        {
            Destroy(child.gameObject);
        }

        List<Item.ItemType> allItems = GetAllItemTypes();

        //Shuffle the items in the list 
        for( int i = 0; i < allItems.Count; i++)
        {
            int rand = Random.Range(i, allItems.Count);
            var temp = allItems[i];
            allItems[i] = allItems[rand];
            allItems[rand] = temp;
        }

        //Show 5 items, differnt each time to the shop is opened 
        for( int i = 0; i < 5; i++)
        {
            Item.ItemType type = allItems[i];
            CreateItemButton(type, Item.GetSprite(type), type.ToString(), Item.GetCost(type), i, Item.GetEffectValue(type), Item.GetDescription(type));
        }
    
    }

    public void Hide() //Close the shop menu 
    {
        gameObject.SetActive(false);
    }

}
