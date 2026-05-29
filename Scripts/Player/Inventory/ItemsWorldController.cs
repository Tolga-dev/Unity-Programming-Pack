using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Player.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class ItemsWorldController : MonoBehaviour
{
    public static ItemsWorldController SpawnItemsWorldController(Vector3 pos, Item item)
    {
        
        GameObject spawnedItem = Instantiate(item.GetItemPrefab(), pos, Quaternion.identity);
        
        spawnedItem.GetComponent<ItemsWorldController>().item = item;
        
        ItemsWorldController itemsWorldController = spawnedItem.transform.GetComponent<ItemsWorldController>();
        
        itemsWorldController.SetItem(item);
        return itemsWorldController;
        
    }
    
    public Item item;
    public TextMeshProUGUI uiText;
 
    // private Image _image;

    private void Awake()
    {
        // _image = GetComponent<Image>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
    //    _image.sprite = item.GetItemSprite();
        if (item.amount > 1)
            uiText.SetText(item.amount.ToString());
        else
            uiText.SetText("");
    }

    public void SetTransform(ItemsWorldController itemsWorldController)
    {
        Debug.Log(itemsWorldController.item.ItemTransform.rotation);
        itemsWorldController.GetComponent<Transform>().position = itemsWorldController.item.ItemTransform.position;       
        itemsWorldController.GetComponent<Transform>().rotation = itemsWorldController.item.ItemTransform.rotation;       
        itemsWorldController.GetComponent<Transform>().localScale = itemsWorldController.item.ItemTransform.localScale;
        Debug.Log(itemsWorldController.transform.rotation);
    }
    
    public Item GetItem()
    {
        Debug.Log(item.itemTypes + " Get item");
        return item;
    }

    public ItemsWorldController GetItemWorldController()
    {
        return this;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public static ItemsWorldController DropItem(Vector3 dropPosition, Item item)
    {
        ItemsWorldController itemsWorldController = SpawnItemsWorldController(dropPosition * 1.2f , item);
        
        return itemsWorldController;
    }


}
