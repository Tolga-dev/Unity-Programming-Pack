using System;
using Chest.ChestManagers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace PrimaryPlayer.PlayerComponentSkills.Inventory
{
    public class InventorySlot : MonoBehaviour, IDropHandler, IPointerDownHandler
    {
        public DraggableObject draggableObjectHolderForInvent;
        public bool isItFull = false;
        
        
        public void OnDrop(PointerEventData eventData)
        {
            if (isItFull == false)
            {
                Debug.Log("DROP");
                var dropped = eventData.pointerDrag;
                DraggableObject draggableObject = dropped.GetComponent<DraggableObject>();
                draggableObjectHolderForInvent = draggableObject;
                draggableObject.parentAfterDrag = transform;
                isItFull = true;
                
              
            }
        }

        public void OnPointerDown(PointerEventData eventData) // FULL PROBLEM
        {
            Debug.Log("MOUSE CLICKED TO INVENTORY SLOT");
            draggableObjectHolderForInvent = null;
            isItFull = false; 
            
        }
    }
}