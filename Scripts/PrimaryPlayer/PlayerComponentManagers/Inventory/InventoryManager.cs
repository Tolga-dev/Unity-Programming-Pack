using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Chest.ChestManagers;
using Chest.ChestTypes;
using PrimaryPlayer.GameEngine;
using PrimaryPlayer.PlayerComponentManagers.Attack;
using PrimaryPlayer.PlayerComponentSkills.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public static class Extensions
{
    public static IList<T> EnsureNotNull<T>(this IList<T> list)
    {
        return list ?? new List<T>();
    }
}

namespace PrimaryPlayer.PlayerComponentManagers.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        
        [Header("Lists")]
        public List<InventorySlot> onGameInventorySlots = new List<InventorySlot>();
        public int onGameIsIndex = 0;

        public List<InventorySlot> inventorySlots = new List<InventorySlot>();
        public List<ItemWorldController> itemListInInv = new List<ItemWorldController>();
        public int inventItemIdAssign = 0;
        
        [Header("Components")]
        public Transform inventoryCanvas;
        
        [Header("Controllers")]
        public InventoryManager inventor;
        private KeyCodeManager _keyCodeManager;
        private PlayerManager.PlayerManager _playerManager;
        
        [Header("Player Public Parameters")]
        private readonly int _maxCanCarryStack = 15;
        public GameObject slotPrefab;
        public Color onSelectedColor = Color.red;
        public Color onUnSelectedColor = Color.white;

        [Header("Player Chest Parameters")]
        private RaycastHit _chestRay;
        public Transform chestRayStartPos;
        public LayerMask _chestMask;
        public bool chestUi = false;
        public Canvas currentChest;
        public float characterMaxChestRayDistance;
        
        private void Start()
        {
            _playerManager = GetComponent<PlayerManager.PlayerManager>();
            _keyCodeManager = _playerManager.InputKeyCodeManager;
            inventor = _playerManager.inventor;
            onGameInventorySlots[onGameIsIndex].GetComponent<Image>().color = onSelectedColor;
        }
        private void Update()
        {
            if (chestUi == false && UnityEngine.Input.GetMouseButtonDown(1)) // Using 1 for right-click
            {
                Debug.Log("clicked");
                if (Physics.Raycast(chestRayStartPos.position, _playerManager.transform.forward, 
                        out _chestRay,  characterMaxChestRayDistance, _chestMask))
                {
                    Debug.Log("Did Hit");
                    var chestController = _chestRay.transform.gameObject.GetComponent<ChestController>();
                    Debug.DrawRay(chestRayStartPos.position, _playerManager.transform.forward * _chestRay.distance, Color.yellow);

                    if (chestController != null)
                    {
                        Debug.Log("Chest Hit");
                        UnLock_Cursor();
                        chestUi = true;
                        currentChest = chestController.chestCanvas;
                        currentChest.gameObject.SetActive(true);
                        GetComponent<AttackManager>().enabled = false;
                    }
                    else
                    {
                        Debug.Log("not chest");
                    }
                        
                    
                }

            }

            if (UnityEngine.Input.GetKeyDown(_keyCodeManager.Keycode.Inventory))
            {
                if (chestUi)
                {
                    currentChest.gameObject.SetActive(false);
                    Lock_Cursor();
                    GetComponent<AttackManager>().enabled = true;

                    chestUi = false;
                    // save items
                }
                else
                {
                    if (inventoryCanvas.gameObject.activeSelf)
                    {
                        Lock_Cursor();
                        inventoryCanvas.gameObject.SetActive(false);
                        GetComponent<AttackManager>().enabled = true;
                    }
                    else
                    {
                        GetComponent<AttackManager>().enabled = false;
                        inventoryCanvas.gameObject.SetActive(true);
                        UnLock_Cursor();
                    }
                }
            }
            
            var pos = UnityEngine.Input.mouseScrollDelta.y;
                
            switch (pos)
            {
                case > 0:
                {
                    onGameInventorySlots[onGameIsIndex].GetComponent<Image>().color = onUnSelectedColor;
                    if (onGameIsIndex == 0)
                        onGameIsIndex = onGameInventorySlots.Count-1;
                    else
                        onGameIsIndex--;
                    onGameInventorySlots[onGameIsIndex].GetComponent<Image>().color = onSelectedColor;
                    break;
                }
                case < 0:
                {
                    onGameInventorySlots[onGameIsIndex].GetComponent<Image>().color = onUnSelectedColor;
                    if (onGameIsIndex >= (onGameInventorySlots.Count - 1))
                        onGameIsIndex = 0;
                    else
                        onGameIsIndex++;
                    onGameInventorySlots[onGameIsIndex].GetComponent<Image>().color = onSelectedColor;
                    break;
                }
                default:
                {
                    if (inventorySlots[onGameIsIndex].isItFull)
                    {
                        var itemType = inventorySlots[onGameIsIndex].draggableObjectHolderForInvent
                            .itemWc.ItemController.GetItem().itemTypes;
                    
                        switch (itemType) 
                        {
                            case ItemTypes.Gun:
                                SetAnimatorToAttack();
                                _playerManager.attackManager.CurrentState = _playerManager.attackManager.PlayerRifleState;
                                break;
                            case ItemTypes.Ammo:
                                SetAnimatorToMovement();
                                break;
                            case ItemTypes.Bandage:
                                SetAnimatorToMovement();
                                break;
                            case ItemTypes.Drink:
                                SetAnimatorToMovement();
                                break;
                            case ItemTypes.Grenade:
                                SetAnimatorToMovement();
                                _playerManager.attackManager.CurrentState = _playerManager.attackManager.PlayerGrenadeState;
                                break;
                            default:
                                SetAnimatorToMovement();
                                break;
                        }
                    }
                    else
                    {
                        SetAnimatorToMovement();
                        _playerManager.attackManager.CurrentState = _playerManager.attackManager.PlayerPunchState;
                        
                    }

                    break;
                }
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (!other.CompareTag("Item")) return;
            
            var itemsWorldController = other.GetComponent<ItemWorldController>();
            
            if (itemsWorldController == null) return;
           
            if (IsBagFull()) return;

            inventor.AddItem(ref itemsWorldController);
            
            Destroy(other.gameObject);
            
        }
        
        // adding items
        private void AddItem(ref ItemWorldController itemWc)
        {
            Debug.Log(itemWc.ItemController.GetItem());
            itemWc.ItemController.GetItemType();
            

            var tupleSamples = GetNewItemParameterValues(ref itemWc);
            
            var newItemAmount = tupleSamples.Item1;
            var maxAmount = tupleSamples.Item2;
            var itemWcInBag = tupleSamples.Item3;
            
            
            if (itemWc.ItemController.GetItem().IsStackable())
            {
                if (itemWcInBag == null)
                {
                    if (newItemAmount <= maxAmount) // not the bag but not enough // bandaglarda ve sadece 1 olursa orda problem birlesmio
                    {
                        AddingItemWithoutPartition(ref itemWc,newItemAmount);
                    }
                    else
                    {
                        Debug.Log("not in bag but enough amount");
                        AddingItemWithPartition(newItemAmount, maxAmount, ref itemWc);
                    }
                }
                else
                {
                    
                    var amountOfItem = itemWcInBag.amount + newItemAmount; //33
                    
                    if (amountOfItem <= maxAmount) // the bag but not enough 
                    {
                        Debug.Log("in bag but not enough amount");
                        itemWcInBag.amount = amountOfItem;
                    }
                    else // in the bag and exceeding
                    {
                        Debug.Log("in bag but enough amount");
                        newItemAmount = newItemAmount - (maxAmount - itemWcInBag.amount);  // newItem = 1 - ()
                        itemWcInBag.amount = maxAmount;
                        AddingItemWithPartition(newItemAmount, maxAmount, ref itemWc);
                          
                    }
                    
                }
                
            }
            else
            {
                for (var i = newItemAmount; i > 0; i--) // 33 
                {   
                    AddingItemWithoutPartition(ref itemWc,itemWc.ItemController.GetItem().canBeStackedMax);
                    Debug.Log("not in bag but not enough amount");
                }   
            }
            UpdateInventoryUi();

        }
        // removing items
        public void RemoveItem(int id)
        {
            RemoveItemByID(id);
        }

        private void RemoveItemByID(int id)
        {
            ItemWorldController itemToRemove = itemListInInv.Find(item => item.itemId == id);
            
            if (itemToRemove != null)
            {
                Debug.Log($"{itemToRemove.name} is deleted");
                itemListInInv.Remove(itemToRemove);
            }
            else
            {
                Debug.LogWarning("Item with ID " + id + " not found in itemList.");
            }
            
        }
        
        private bool IsBagFull() => (_maxCanCarryStack == itemListInInv.Count);

        private ItemWorldController IsThisItemAlreadyInBag(ItemTypes itemWcTypes)
        {
            foreach (var item in itemListInInv)
            {
                if (item.ItemController.GetItem().itemTypes == itemWcTypes && item.amount != item.ItemController.GetItem().canBeStackedMax)
                {
//                    Debug.Log( item.amount );
                    return item;
                }
            }
            return null;
        }

        private (int, int, ItemWorldController) GetNewItemParameterValues(ref ItemWorldController itemWc)
        {
            return (itemWc.amount, itemWc.ItemController.GetItem().canBeStackedMax, IsThisItemAlreadyInBag(itemWc.ItemController.GetItem().itemTypes));
        }
        
        private ItemWorldController PrepareNewItemToInventory(ref ItemWorldController itemWc, int amount)
        {
            
            var instantiate = Instantiate(itemWc.ItemController.GetItem().GetItemPrefab());
            ItemWorldController allocateNewItem = instantiate.AddComponent<ItemWorldController>(); 
            instantiate.SetActive(false);
            
            allocateNewItem.ItemController = itemWc.ItemController;
            allocateNewItem.itemId = inventItemIdAssign++;
            allocateNewItem.amount = amount;
            
            AddItemToInventoryUI(allocateNewItem);
            
            return allocateNewItem;
        }

        private void AddingItemWithoutPartition(ref ItemWorldController itemWc, int newItemAmount)
        {
            ItemWorldController allocateNewItem = PrepareNewItemToInventory(ref itemWc, newItemAmount);
            itemListInInv.Add(allocateNewItem);
        }
        
        private void AddingItemWithPartition(int newItemAmount, int maxAmount,ref ItemWorldController itemWc)
        {
            for (var i = newItemAmount; i > 0;) // 33 
            {
                ItemWorldController allocateNewItem = PrepareNewItemToInventory(ref itemWc, maxAmount);
                
                if (i < maxAmount) // 33 >= 32
                {
                    allocateNewItem.amount = i;
                }
                
                itemListInInv.Add(allocateNewItem); // 32
                
                i -= maxAmount; // 1
            }
        }
       
        private void AddItemToInventoryUI(ItemWorldController itemWc) // added parent 
        {
            foreach (var slot in inventorySlots)
            {
                if (slot.transform.childCount == 0)
                {   
                    DraggableObject draggableObject = slotPrefab.GetComponent<DraggableObject>();
                    draggableObject.itemWc = itemWc; // it is destroyed check add f  
                    
                    GameObject slotPrefabCreated = Instantiate(slotPrefab, slot.transform, true);
                    DraggableObject slotPrefabObjectDraggableObject = slotPrefabCreated.GetComponent<DraggableObject>();
                    slotPrefabObjectDraggableObject.isPrevParentChest = null; // it is destroyed check add f  
                    slotPrefabObjectDraggableObject.inventoryManager = this; // it is destroyed check add f  
                    
                    slotPrefabCreated.transform.localScale = Vector3.one;
                    slotPrefabCreated.GetComponent<Image>().sprite = itemWc.ItemController.GetItem().GetItemSprite();
                    
                    var childSlot = slotPrefabCreated.transform.GetChild(0).gameObject;
                    childSlot.GetComponent<TMP_Text>().text = itemWc.amount.ToString();
                    
                    slot.GetComponent<InventorySlot>().draggableObjectHolderForInvent = slotPrefabObjectDraggableObject;
                    
                    
                    slot.GetComponent<InventorySlot>().isItFull = true;
                    
                    break;
                }
                
            }
        }
       

        private void UpdateInventoryUi()
        {
            
            foreach (var slot in inventorySlots)
            {
                if (slot.transform.childCount == 0)
                    continue;
                var slotItem = slot.transform.GetChild(0);
                var draggableObject = slotItem.GetComponent<DraggableObject>();
                draggableObject.amountShower.text = draggableObject.itemWc.amount.ToString();
                
            }
            
        }
        
        
        private void Lock_Cursor() => Cursor.lockState = CursorLockMode.Locked;
        private void UnLock_Cursor() => Cursor.lockState = CursorLockMode.Confined;
        
        private void SetAnimatorToMovement()
        {
            SetAnimatorMovementWeightFull();
            SetAnimatorAttackWeightZero();
            
        }

        private void SetAnimatorToAttack()
        { 
            SetAnimatorMovementWeightZero();
            SetAnimatorAttackWeightFull();
        }

        private void SetAnimatorMovementWeightFull()
        {
            _playerManager.animator.SetLayerWeight(1, 1);
        }
        private void SetAnimatorMovementWeightZero()
        {
            _playerManager.animator.SetLayerWeight(1, 0);
        }
        private void SetAnimatorAttackWeightFull()
        {
            _playerManager.animator.SetLayerWeight(2, 1);
        }        
        private void SetAnimatorAttackWeightZero()
        {
            _playerManager.animator.SetLayerWeight(2, 0);
        }        
        
    }
    
    
}




