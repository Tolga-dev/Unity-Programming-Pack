using System;
using System.Collections;
using System.Collections.Generic;
using Player.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DrapDrog : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private bool onSelectableOnject = false;
    private UIManager _manager;
    private int MouseRightLeft;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _manager = canvas.GetComponent<UIManager>();
        
    }
    
    public void Update()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            MouseRightLeft = 1;
            //return true;
            Debug.Log("left");
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            MouseRightLeft = 0;
//            return false;
            Debug.Log("right");
        }
        
        //Debug.Log("No Input");
        //return false;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        
        if(MouseRightLeft == 1) //WhichInputClickFunc()
//            _manager.RemoveItemFromInventory(gameObject.GetComponent<ItemsWorldController>().item);
            _manager.RemoveItemFromInventory2(gameObject.GetComponent<ItemsWorldController>());
        if (MouseRightLeft == 0)
            _manager.UseItem(gameObject.GetComponent<ItemsWorldController>().item);

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnPointerDrag");
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = .6f;
        _canvasGroup.blocksRaycasts = false;
        Debug.Log("OnPointerBegin");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        Debug.Log("OnPointerEnd");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnd");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        onSelectableOnject = true;
        Debug.Log("The cursor entered the selectable UI element.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onSelectableOnject = false;
        Debug.Log("The cursor exited the selectable UI element.");
    }
 
    
    
        
}
