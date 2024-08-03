using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Tracker : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler

{
    public Canvas canvas;

    private RectTransform rectTransform;
    private Vector2 initalPosition;
    private Vector2 dragOffset;
    private float duration = 0.2f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initalPosition = new Vector2(50, -50);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out mousePosition);
        rectTransform.anchoredPosition = mousePosition - dragOffset;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DualManager.instance.isDraging = true;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out var localPointerPosition);
        dragOffset = localPointerPosition - rectTransform.anchoredPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DualManager.instance.isDraging = false;
        initalPosition = rectTransform.anchoredPosition;
    }

    public void PanelOnOff()
    {
        DualManager.instance.isSequenceRunning = true;

        if(gameObject.activeSelf)
        {
            rectTransform.DOAnchorPosX(-700, duration)
            .OnComplete(()=> 
            {
                gameObject.SetActive(false);
                DualManager.instance.isSequenceRunning = false;
            }
            );
        }

        else
        {
            gameObject.SetActive(true);
            rectTransform.DOAnchorPos(initalPosition, duration)
            .OnComplete(()=> DualManager.instance.isSequenceRunning = false);
        }
    }
}
