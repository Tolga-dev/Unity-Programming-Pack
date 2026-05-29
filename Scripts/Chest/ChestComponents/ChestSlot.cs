using PrimaryPlayer.PlayerComponentSkills.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chest.ChestComponents
{
    public class ChestSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
    {
        public DraggableObject DraggableObject;
        public bool isItFull = false;
        
        public void OnDrop(PointerEventData eventData)
        {
            if (isItFull == false)
            {
                
                Debug.Log("Chest Drop");
                var dropped = eventData.pointerDrag;
                DraggableObject draggableObject = dropped.GetComponent<DraggableObject>();
                DraggableObject = draggableObject;
                draggableObject.parentAfterDrag = transform;
                isItFull = true;
                
                
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("MOUSE CLICKED TO chest SLOT");
            DraggableObject = null;
            isItFull = false; 

        }
    }
}