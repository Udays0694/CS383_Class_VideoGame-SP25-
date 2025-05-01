using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<CraftingRecipeClass> craftingRecipes = new List<CraftingRecipeClass>();

    [SerializeField] private GameObject itemCursor;

    [SerializeField] private GameObject slotHolder;
    [SerializeField] private GameObject hotbarSlotHolder;
    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;

    [SerializeField] private SlotClass[] startingItems;

    private SlotClass[] items;

    private GameObject[] slots;
    private GameObject[] hotbarSlots;

    private SlotClass movingSlot;
    private SlotClass tempSlot;
    private SlotClass originalSlot;
    bool isMovingItem;

    [SerializeField] private GameObject hotbarSelector;
    [SerializeField] private int selectedSlotIndex = 0;
    public ItemClass selectedItem;

    private void Start()
{
    // Initialize hotbar slots
    hotbarSlots = new GameObject[hotbarSlotHolder.transform.childCount];
    for (int i = 0; i < hotbarSlots.Length; i++)
        hotbarSlots[i] = hotbarSlotHolder.transform.GetChild(i).gameObject;

    // Initialize inventory slots and UI references
    slots = new GameObject[slotHolder.transform.childCount];
    for (int i = 0; i < slots.Length; i++)
        slots[i] = slotHolder.transform.GetChild(i).gameObject;

    // Ensure items array aligns with slot array
    items = new SlotClass[slots.Length];
    for (int i = 0; i < items.Length; i++)
        items[i] = new SlotClass();

    // Add starting items AFTER everything is initialized
    for (int i = 0; i < startingItems.Length; i++)
        Add(startingItems[i].item, startingItems[i].quantity);

    // Refresh the UI to reflect item states
    RefreshUI();
}

    private void Update()
{
    // Crafting (temporary example using first recipe)
    if (Input.GetKeyDown(KeyCode.C))
        Craft(craftingRecipes[0]);

    // Update cursor visibility and position
    itemCursor.SetActive(isMovingItem);
    itemCursor.transform.position = Input.mousePosition;

    if (isMovingItem && movingSlot.item != null)
        itemCursor.GetComponent<Image>().sprite = movingSlot.item.itemIcon;

    // Handle left click
    if (Input.GetMouseButtonDown(0))
    {
        if (isMovingItem)
            EndItemMove();
        else
            BeginItemMove();
    }
    // Handle right click
    else if (Input.GetMouseButtonDown(1))
    {
        if (isMovingItem)
            EndItemMove_Single();
        else
            BeginItemMove_Half();
    }

    // Scroll through hotbar
    if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        selectedSlotIndex = Mathf.Clamp(selectedSlotIndex + 1, 0, hotbarSlots.Length - 1);
    else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        selectedSlotIndex = Mathf.Clamp(selectedSlotIndex - 1, 0, hotbarSlots.Length - 1);

    // Move selector to selected slot
    hotbarSelector.transform.position = hotbarSlots[selectedSlotIndex].transform.position;

    // Correct index mapping for selected item
    int hotbarRowsFromBottom = 1; // if hotbar is the bottom row only
    int itemIndex = selectedSlotIndex + (hotbarSlots.Length * (4 - hotbarRowsFromBottom));

    if (itemIndex >= 0 && itemIndex < items.Length && items[itemIndex] != null)
        selectedItem = items[itemIndex].item;
    else
    {
        selectedItem = null;
#if UNITY_EDITOR
        Debug.LogWarning($"[InventoryManager] Invalid item index: {itemIndex}. Items length: {items.Length}");
#endif
    }
}

    private void Craft(CraftingRecipeClass recipe)
    {
        if (recipe.CanCraft(this))
            recipe.Craft(this);
        else
            //show error msg
            Debug.Log("Can't craft that item!");
    }

    #region Inventory Utils
    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
            RefreshSlot(items[i], slots[i].transform);
        RefreshHotbar();
    }
    private void RefreshSlot(SlotClass slot, Transform slotUI)
    {
        if (slot.item is null)
        {
            //item is null in the slot
            slotUI.transform.GetChild(0).GetComponent<Image>().sprite = null;
            slotUI.transform.GetChild(0).GetComponent<Image>().enabled = false;
            slotUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            return;
        }

        //item exists in slot
        slotUI.transform.GetChild(0).GetComponent<Image>().enabled = true;
        slotUI.transform.GetChild(0).GetComponent<Image>().sprite = slot.item.itemIcon;

        if (slot.item.isStackable)
            slotUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = slot.quantity + "";
        else
            slotUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
    }
    public void RefreshHotbar() 
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
            RefreshSlot(items[i + (hotbarSlots.Length * 3)], hotbarSlots[i].transform);
    }
    public bool Add(ItemClass item, int quantity)
    {
        //check if inventory contains item
        SlotClass slot = Contains(item);

        if (slot != null && slot.item.isStackable && slot.quantity < item.stackSize)
        {
            // going to add 20 = quantity
            // there is already 5 = slot.quantity;
            var quantityCanAdd = slot.item.stackSize - slot.quantity; //16 - 5 = 11
            var quantityToAdd = Mathf.Clamp(quantity, 0, quantityCanAdd);
                
            var remainder = quantity - quantityCanAdd; // = 9
            
            slot.AddQuantity(quantityToAdd);
            if (remainder > 0) Add(item, remainder);
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].item == null) //this is an empty slot
                { 
                    var quantityCanAdd = item.stackSize - items[i].quantity; //16 - 5 = 11
                    var quantityToAdd = Mathf.Clamp(quantity, 0, quantityCanAdd);
                
                    var remainder = quantity - quantityCanAdd; // = 9
            
                    items[i].AddItem(item, quantityToAdd);
                    if (remainder > 0) Add(item, remainder);
                    break;
                }
            }
        }

        RefreshUI();
        return true;
    } 
    public bool Remove(ItemClass item, int quantity = 1)
    {
        // items.Remove(item);
        SlotClass temp = Contains(item);
        if (temp != null)
        {
            if (temp.quantity > 1)
                temp.SubQuantity(quantity);
            else
            {
                int slotToRemoveIndex = 0;

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].item == item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }

                items[slotToRemoveIndex].Clear();
            }
        }
        else
        {
            return false;
        }

        RefreshUI();
        return true;
    }
    public void UseSelected()
    {
        items[selectedSlotIndex + (hotbarSlots.Length * 3)].SubQuantity(1);
        RefreshUI();
    }
    public bool isFull()
    {
        foreach (var slot in items)
        {
            if (slot is null || slot.quantity < slot.item.stackSize || !slot.item.isStackable)
                return false;
        }

        return true;
        /*for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item is null || items[i].quantity < items[i].item.stackSize)
                return false;
        }
        return true;*/
    }
    public SlotClass Contains(ItemClass item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item == item /*&& items[i].item.isStackable && */)
                return items[i];
        }

        return null;
    }
    public bool Contains(ItemClass item, int quantity)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item == item && items[i].quantity >= quantity)
                return true;
        }

        return false;
    }
    #endregion Inventoy Utils

    #region Moving Stuff
    private bool BeginItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null || originalSlot.item == null)
            return false; //there is not item to move!

        movingSlot = new SlotClass(originalSlot);
        originalSlot.Clear();
        isMovingItem = true;
        RefreshUI();
        return true;
    }
    private bool BeginItemMove_Half()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null || originalSlot.item == null)
            return false; //there is not item to move!

        movingSlot = new SlotClass(originalSlot.item, Mathf.CeilToInt(originalSlot.quantity / 2f));
        originalSlot.SubQuantity(Mathf.CeilToInt(originalSlot.quantity / 2f));
        if (originalSlot.quantity == 0)
            originalSlot.Clear();

        isMovingItem = true;
        RefreshUI();
        return true;
    }
    private bool EndItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null)
        {
            Add(movingSlot.item, movingSlot.quantity);
            movingSlot.Clear();
        }
        else //clicked on a slot
        {
            if (originalSlot.item != null)
            {
                if (originalSlot.item == movingSlot.item && originalSlot.item.isStackable &&
                    originalSlot.quantity < originalSlot.item.stackSize) //they're the same item (they should stack)
                {
                    var quantityCanAdd = originalSlot.item.stackSize - originalSlot.quantity; // = 6
                    var quantityToAdd = Mathf.Clamp(movingSlot.quantity, 0, quantityCanAdd);
                    var remainder = movingSlot.quantity - quantityToAdd; // = 10

                    originalSlot.AddQuantity(quantityToAdd);
                    if (remainder == 0) movingSlot.Clear();
                    else
                    {
                        movingSlot.SubQuantity(quantityCanAdd);
                        RefreshUI();
                        return false;
                    }
                }
                else
                {
                    tempSlot = new SlotClass(originalSlot); //a = b
                    originalSlot.AddItem(movingSlot.item, movingSlot.quantity); //b = c
                    movingSlot.AddItem(tempSlot.item, tempSlot.quantity); //a = c
                    RefreshUI();
                    return true;
                }
            }
            else //place item as usual
            {
                originalSlot.AddItem(movingSlot.item, movingSlot.quantity);
                movingSlot.Clear();
            }
        }

        isMovingItem = false;
        RefreshUI();
        return true;
    }
    private bool EndItemMove_Single()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot is null)
            return false;
        if (originalSlot.item is not null && 
            (originalSlot.item != movingSlot.item || originalSlot.quantity >= originalSlot.item.stackSize))
            return false;

        movingSlot.SubQuantity(1);
        if (originalSlot.item != null && originalSlot.item == movingSlot.item)
            originalSlot.AddQuantity(1);
        else
            originalSlot.AddItem(movingSlot.item, 1);

        if (movingSlot.quantity < 1)
        {
            isMovingItem = false;
            movingSlot.Clear();
        }
        else
            isMovingItem = true;

        RefreshUI();
        return true;
    }
    private SlotClass GetClosestSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 32)
                return items[i];
        }
        return null;
    }
    #endregion
}
