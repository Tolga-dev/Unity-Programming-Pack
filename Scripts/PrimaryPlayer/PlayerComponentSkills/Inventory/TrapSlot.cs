using PrimaryPlayer.PlayerComponentManagers.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PrimaryPlayer.PlayerComponentSkills.Inventory
{
    public class TrapSlot : MonoBehaviour, IDropHandler
    {
        public InventoryManager manager;
        public Transform throwingItemPlace;
        public int Force;
        public float DetectionDistance = 0.1f;
        public void OnDrop(PointerEventData eventData)
        {
            var dropped = eventData.pointerDrag;
            
            DraggableObject draggableObject = dropped.GetComponent<DraggableObject>();
            
            var item = draggableObject.itemWc;
            Rigidbody itemRg = item.gameObject.AddComponent<Rigidbody>();
            
            item.transform.position = throwingItemPlace.position;
            ItemWorldControllerHelper controllerHelper = item.gameObject.AddComponent<ItemWorldControllerHelper>();
            controllerHelper.rigidbody = itemRg;
            controllerHelper.detectionDistance = DetectionDistance;
            
            item.gameObject.SetActive(true);
            
            Vector3 cameraSight = throwingItemPlace.forward;
            itemRg.AddForce(new Vector3(cameraSight.x*Force, Force, cameraSight.z*Force));

            var textObject = item.transform.GetChild(0).GetChild(0);
            var text = textObject.GetComponent<TMP_Text>();
            text.text = draggableObject.itemWc.amount.ToString();
            
            
            Destroy(dropped);
            manager.RemoveItem(item.itemId);
        }
        
    }
}