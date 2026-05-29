using System;
using Chest.ChestComponents;
using Chest.ChestManagers;
using PrimaryPlayer.PlayerComponentManagers.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace PrimaryPlayer.PlayerComponentSkills.Inventory
{
    public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [HideInInspector] public Transform parentAfterDrag;
        public Image image;
        public TMP_Text amountShower;
        public ItemWorldController itemWc;

        
        public ChestSlot isCurrentParentChest;
        public ChestSlot isPrevParentChest;
        
        public InventoryManager inventoryManager;
        public ChestController chestController;
        
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            
            parentAfterDrag = transform.parent;
            transform.SetParent(parentAfterDrag);
            
            transform.SetAsFirstSibling();
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // we will change herer 
            transform.SetParent(parentAfterDrag);
            
            image.raycastTarget = true;
            
            isCurrentParentChest = transform.parent.GetComponent<ChestSlot>();  // is chest?
            
            if (isCurrentParentChest != null) // from chest 
            {
                if (isPrevParentChest != null) // to chest
                {
                    Debug.Log("chest to chest gone!"); // no changes
                }
                else
                {
                    Debug.Log("chest to item  gone!"); // 
                    isPrevParentChest = isCurrentParentChest;
                    chestController = GetComponentInParent<ChestController>();
                }
            }
            else // from item
            {
                if (isPrevParentChest != null) // to chest
                {
                    Debug.Log("item to chest gone!");
                    isPrevParentChest = isCurrentParentChest;
                    chestController = GetComponentInParent<ChestController>();
                }
                else // to item
                {
                    Debug.Log("item to item gone!");
                }
            }
        }


    }
}