using System;
using System.Collections;
using System.Collections.Generic;
using Player.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    public GameObject selectedObject;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        Debug.Log(gameObject.name);
        selectedObject = gameObject;
        
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
        Debug.Log("The cursor entered the selectable UI element.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("The cursor exited the selectable UI element.");
    }
 
    
    
        
}
