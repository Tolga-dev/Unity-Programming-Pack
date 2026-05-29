using System;
using System.Collections;
using System.Collections.Generic;
using Player.Inventory;
using Player.PlayerStates.Attack;
using Player.PlayerStates.Movement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Inventory inventory;
    private Transform InventoryContainer;
    private Transform Items;
    public TextMeshProUGUI uiText;
    private MovementManager movementManager; // player olacak ilerde
    
    private void Awake()
    {
        
        InventoryContainer = transform.Find("Inventory");
        Items = InventoryContainer.Find("Items");

    }

    /*
    public void RemoveItemFromInventory(Item item)
    {
        Debug.Log(item.amount);
        inventory.RemoveItem(item);
        foreach (ItemsWorldController itemsWorldController in movementManager._weaponManager.itemPrefabToShowPlayer)
        {
            if (itemsWorldController.item.Id == item.Id)
            {
                ItemsWorldController.DropItem(movementManager.transform.position, itemsWorldController.item);
                movementManager._weaponManager.RemoveItemFromPlayersTransform(movementManager.transform.position, item);
                break;
                
            }
        }
    }
*/
    public void RemoveItemFromInventory2(ItemsWorldController itemsWorldController)
    { 
        inventory.RemoveItem(itemsWorldController.item);
        foreach (ItemsWorldController itemsWorld in movementManager._weaponManager.itemPrefabToShowPlayer)
        {
            if (itemsWorldController.item.Id == itemsWorld.item.Id)
            {
                //movementManager._weaponManager.DropItem(movementManager.transform.position * 1.2f, itemsWorldController);
                movementManager._weaponManager.RemoveItemFromPlayersTransform2(movementManager.transform.position * 1.2f, itemsWorld);
                break;
                
            }
        }
    }

    public void UseItem(Item item)
    {
        inventory.UseItem(item);
    }

    public void SetPlayer(MovementManager movementManager)
    {
        this.movementManager = movementManager;
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged; 
        ReloadInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        ReloadInventoryItems();
    }
    private void ReloadInventoryItems()
    {
        foreach (Transform child in InventoryContainer)
        {
            if(child == Items) continue;
            Destroy(child.gameObject);
        }
        int pos_x = 0;
        int pos_y = 0;
        float itemSlotCellSize = 30f;
        
        foreach (Item item in inventory.GetItemList())
        {

            RectTransform itemSlotRectTransform = Instantiate(Items,InventoryContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(pos_x * itemSlotCellSize, pos_y * itemSlotCellSize);
            
            Image image = itemSlotRectTransform.GetComponent<Image>();
            ItemsWorldController itemsWorldController =  itemSlotRectTransform.GetComponent<ItemsWorldController>();
            
            itemsWorldController.item.itemTypes = item.itemTypes;
            itemsWorldController.item.amount = item.amount;
            itemsWorldController.item.itemPref = item.GetItemPrefab();
            itemsWorldController.item.Id = item.Id;
            itemsWorldController.item.Damage = item.Damage;
            itemsWorldController.item.Recoil = item.Recoil;
            itemsWorldController.item.Reload = item.Reload;
            itemsWorldController.item.CurrentAmmoAmount = item.CurrentAmmoAmount;
            itemsWorldController.item.AmmoCapacity = item.AmmoCapacity;
            itemsWorldController.item.ItemTransform = item.GetItemTransform();
            image.sprite = item.GetItemSprite();

            uiText = itemSlotRectTransform.transform.Find("amount").GetComponent<TextMeshProUGUI>();
            if (item.amount >= 1)
                uiText.SetText(item.amount.ToString());
            else
                uiText.SetText("");
            
            pos_x++;
            if (pos_x >= 1)
            {
                pos_x = 0;
                pos_y++;
            }

            Debug.Log(item.itemTypes + " Reload Inventory");                
        }
    }

    public void OpenInventory()
    {
        movementManager.gameObject.GetComponent<AttackManager>().enabled = false;
        InventoryContainer.gameObject.SetActive(true);

    }

    public void CloseInventory()
    {
        movementManager.gameObject.GetComponent<AttackManager>().enabled = true;
        InventoryContainer.gameObject.SetActive(false);

    }
    
    
    
  
}
