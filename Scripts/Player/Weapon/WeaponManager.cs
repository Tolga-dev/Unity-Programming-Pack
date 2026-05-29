using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Player.Inventory;
using Player.PlayerStates.Attack;
using Player.PlayerStates.Movement;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    
    private MovementManager movementManager; // player olacak ilerd 
    private AttackManager _attackManager; // player olacak ilerd 
    private ItemsWorldController _itemsWorldController;
    public GameObject SelectedItemParent;// parent olarak degistirelim daha sorna
    public GameObject Items;
    public GameObject OnSelectedItem;
    public GameObject RemovedItemsParent;
    public GameObject newGameObject;
    public List<ItemsWorldController> itemPrefabToShowPlayer = new List<ItemsWorldController>();
    
    public void Start()
    {
        Items = gameObject.transform.Find("Items").gameObject;
        SelectedItemParent = gameObject.transform.Find("SelectedItem").gameObject;
    }

    public void SetPlayer(MovementManager movementManager)
    {
        this.movementManager = movementManager;
    }
    public void SetPlayerAttackManager(AttackManager attackManager)
    {
        this._attackManager = attackManager;
    }
    public void SetPlayerItemWorld(ItemsWorldController itemsWorldController )
    {
        this._itemsWorldController = itemsWorldController;
    }
    public void AddToItemsForSelectedItems(Item item)
    {
        
        if (SelectedItemParent.transform.GetChild(0).GetComponent<ItemsWorldController>() != null)
        {
            if (SelectedItemParent.transform.GetChild(0).GetComponent<ItemsWorldController>().item.Id != item.Id)
            {

                foreach (var weaponController in itemPrefabToShowPlayer)
                {
                    if (weaponController.item.Id == item.Id )
                    {
                        
                        SelectedItemParent.transform.GetChild(0).gameObject.transform.parent = Items.transform;
                        weaponController.transform.parent = SelectedItemParent.gameObject.transform;
                        movementManager._attackManager.FirePlace = weaponController.gameObject.transform.Find("ShootingPoint").transform;
                        OnSelectedItem = weaponController.gameObject;
                    }
                }
            }
        }
        else
        {
            Debug.Log("bruhhhhhhhhhhh");
        }
    }

    public void DropItem(Vector3 dropPosition, ItemsWorldController item)
    {

        Debug.Log("hello");
        //SpawnItemsWorldController(dropPosition * 1.2f , item);
             
    }
    
    public void AddToItemsTransformForItems2(ItemsWorldController itemsWorldController)
    {
         
         
        itemsWorldController.gameObject.GetComponent<Collider>().enabled = false; // birakirken acilicak
        itemsWorldController.transform.Find("Canvas").gameObject.SetActive(false); // birakirken acilicak
        
        bool IsStackable = itemsWorldController.item.IsStackable();
        
        itemPrefabToShowPlayer.Add(itemsWorldController);
        
        foreach (ItemsWorldController itemWorld in itemPrefabToShowPlayer) // bura sikintili 2. gun eklenemiyor.
        {
            if (IsStackable && itemWorld.item.itemTypes == itemsWorldController.item.itemTypes)
            {
                if (itemWorld.item.Id != itemsWorldController.item.Id)
                {
                    Debug.Log("b1");
                    itemPrefabToShowPlayer.Remove(itemsWorldController);
                    Destroy(itemsWorldController.gameObject);
                    break;
                }
                
                Debug.Log("b2");
                itemWorld.gameObject.transform.parent = Items.transform;
                itemWorld.gameObject.GetComponent<Transform>().localPosition = itemWorld.item.GetItemTransform().position;       
                itemWorld.gameObject.GetComponent<Transform>().localRotation = itemWorld.item.GetItemTransform().rotation;       
                itemWorld.gameObject.GetComponent<Transform>().localScale = itemWorld.item.GetItemTransform().localScale;
                
                break;
            }
            else // id lazim id lazim
            {
                if (itemWorld.item.Id == itemsWorldController.item.Id)
                {
                    Debug.Log("b3");
                    Debug.Log(itemWorld.item.Id);
                    itemWorld.gameObject.transform.parent = Items.transform;
                    itemWorld.GetComponent<Transform>().localPosition = itemWorld.item.GetItemTransform().position;       
                    itemWorld.GetComponent<Transform>().localRotation = itemWorld.item.GetItemTransform().rotation;       
                    itemWorld.GetComponent<Transform>().localScale = itemWorld.item.GetItemTransform().localScale;
                    break;
                }
            }
            
            
        }
        
        if (SelectedItemParent.transform.childCount == 0)
        {
            OnSelectedItem = itemsWorldController.gameObject;
             
            itemsWorldController.gameObject.transform.parent = SelectedItemParent.gameObject.transform;

            Transform ShootingPoint = OnSelectedItem.transform.Find("ShootingPoint").transform;
            if(ShootingPoint != null)
                movementManager._attackManager.FirePlace = ShootingPoint;
            else
            {
                Debug.Log("Shooting Point Null!");
            }
        }
        
    }
    
    public void AddToItemsTransformForItems(Item item)
    {
        
        newGameObject = item.GetItemPrefab();
        GameObject TempItem = Instantiate(newGameObject,Items.transform);
        TempItem.GetComponent<Collider>().enabled = false;

        //foreach (Collider c in TempItem.GetComponents<Collider>()) { c.enabled = false; } if there is one more collider
        
        itemPrefabToShowPlayer.Add(TempItem.GetComponent<ItemsWorldController>());
        foreach (ItemsWorldController itemsWorldController in itemPrefabToShowPlayer)
        {
            if (itemsWorldController.item.Id == item.Id)
            {
                itemsWorldController.gameObject.transform.parent = Items.transform;
            }
        }
        
        if (SelectedItemParent.transform.childCount == 0)
        {
            Debug.Log("girildi");
            OnSelectedItem = TempItem;
    
            TempItem.gameObject.transform.parent = SelectedItemParent.gameObject.transform;
            movementManager._attackManager.FirePlace = OnSelectedItem.transform.Find("ShootingPoint").transform;
        }
    }
    

    public void RemoveItemFromSelectedTransform(Vector3 dropPosition, Item item)
    {
        
        itemPrefabToShowPlayer.Remove(SelectedItemParent.transform.GetChild(0).GetComponent<ItemsWorldController>());
        SelectedItemParent.transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = true;
        SelectedItemParent.transform.GetChild(0).gameObject.transform.position = dropPosition;
        SelectedItemParent.transform.GetChild(0).gameObject.transform.parent = RemovedItemsParent.transform;

        if (SelectedItemParent.transform.childCount == 0 && Items.transform.childCount > 0)
        {
            Debug.Log("girildi");
            
            Items.transform.GetChild(0).gameObject.transform.parent = SelectedItemParent.gameObject.transform;
            movementManager._attackManager.FirePlace = OnSelectedItem.transform.Find("ShootingPoint").transform;
        }
        

    }

    public void RemoveItemFromItemsTransform(Vector3 dropPosition, Item item)
    {
  
        
        foreach (Transform childs in Items.transform)
        {
                
                Debug.Log(childs);
            
            if (childs.GetComponent<ItemsWorldController>().item.Id == item.Id)
            {
                childs.gameObject.GetComponent<Collider>().enabled = true;
                itemPrefabToShowPlayer.Remove(childs.GetComponent<ItemsWorldController>());
                childs.gameObject.transform.position = dropPosition;
                childs.gameObject.transform.parent = RemovedItemsParent.transform;
                break;
            }
        }
         
    }
    
    public void RemoveItemFromPlayersTransform(Vector3 dropPosition, Item item)
    {
        
        if (OnSelectedItem.GetComponent<ItemsWorldController>().item.Id == item.Id)
        {
           // RemoveItemFromSelectedTransform(dropPosition * 1.2f, item);
  
        }
        else
        {
         //  RemoveItemFromItemsTransform(dropPosition * 1.2f, item);
 
        }
        
    }

    public void RemoveItemFromSelectedTransform2(Vector3 dropPosition, ItemsWorldController itemsWorldController)
    {
                
        itemPrefabToShowPlayer.Remove(itemsWorldController); 
        itemsWorldController.gameObject.transform.position = dropPosition;
        itemsWorldController.gameObject.transform.parent = RemovedItemsParent.transform;
        itemsWorldController.gameObject.GetComponent<Collider>().enabled = true;

        if (SelectedItemParent.transform.childCount == 0 && Items.transform.childCount > 0)
        {
            Debug.Log("girildi");
            OnSelectedItem = Items.transform.GetChild(0).gameObject;
            Items.transform.GetChild(0).gameObject.transform.parent = SelectedItemParent.gameObject.transform;
            Transform ShootingPoint = OnSelectedItem.transform.Find("ShootingPoint").transform;
            if(ShootingPoint != null)
                movementManager._attackManager.FirePlace = ShootingPoint;
            else
            {
                Debug.Log("Shooting Point Null!");
            }
             
        }

    }
    public void RemoveItemFromItemsTransform2(Vector3 dropPosition, ItemsWorldController itemsWorldController)
    {
                
        foreach (Transform childs in Items.transform)
        {
            if (childs.GetComponent<ItemsWorldController>().item.Id == itemsWorldController.item.Id)
            { 
                itemPrefabToShowPlayer.Remove(childs.GetComponent<ItemsWorldController>());
                itemsWorldController.gameObject.GetComponent<Collider>().enabled = true;
                childs.gameObject.transform.position = dropPosition;
                childs.gameObject.transform.parent = RemovedItemsParent.transform;
                break;
            }
        }
    }

    public void RemoveItemFromPlayersTransform2(Vector3 dropPosition, ItemsWorldController itemsWorldController)
    {
        
        if (OnSelectedItem.GetComponent<ItemsWorldController>().item.Id == itemsWorldController.item.Id)
        {
            Debug.Log("Selected Object Is Deleted");
            // RemoveItemFromSelectedTransform(dropPosition * 1.2f, item);
            RemoveItemFromSelectedTransform2(dropPosition, itemsWorldController);
        }
        else
        {
            Debug.Log("Itemsa Object Is Deleted");
            //  RemoveItemFromItemsTransform(dropPosition * 1.2f, item);
            RemoveItemFromItemsTransform2(dropPosition, itemsWorldController);
        }
        
    }
    public void ReloadGun(Item item) // mermi diye var sayiliyor
    {
        ItemsWorldController ammoItem = null;
        foreach (ItemsWorldController itemChild in itemPrefabToShowPlayer)
        {
            if (itemChild.item.itemTypes == item.itemTypes)
            {
                ammoItem = itemChild;
            }
        }

        if (OnSelectedItem.GetComponent<ItemsWorldController>().item.itemTypes == Item.ItemTypes.Gun && ammoItem != null)
        {
            
            ItemsWorldController itemsWorldController = SelectedItemParent.transform.GetChild(0).GetComponent<ItemsWorldController>();
            if ((itemsWorldController.item.AmmoCapacity - itemsWorldController.item.CurrentAmmoAmount) < item.amount)
            {
                item.amount -= (itemsWorldController.item.AmmoCapacity - itemsWorldController.item.CurrentAmmoAmount);
                itemsWorldController.item.CurrentAmmoAmount = itemsWorldController.item.AmmoCapacity;
                movementManager.inventory.RemoveItem(new Item { itemTypes = Item.ItemTypes.Ammo, amount = itemsWorldController.item.AmmoCapacity - itemsWorldController.item.CurrentAmmoAmount });
            
            }
            else
            {
                itemsWorldController.item.CurrentAmmoAmount += item.amount;
                item.amount = 0;
                movementManager.inventory.RemoveItem(item);
                Destroy(ammoItem);
            }
            
        }
    }

    public void Destroy(ItemsWorldController itemsWorldController)
    {
        Destroy(itemsWorldController.gameObject);
    }
}
