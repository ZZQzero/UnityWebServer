using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test2 : EventTrigger
{
    // Start is called before the first frame update
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Canvas canvas;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    public override void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        _rectTransform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
    }
    
    public override void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }



    public override void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = transform;
    }

    public override void OnDrop(PointerEventData eventData)
    {

    }
}
