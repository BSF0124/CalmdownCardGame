using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Tracker : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public DualManager dualManager;

    private RectTransform rectTransform;
    private Vector2 initalPosition = new Vector2(50, -50);
    private Vector2 dragOffset;
    private float duration = 0.2f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out mousePosition);
        rectTransform.anchoredPosition = mousePosition - dragOffset;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dualManager.isDraging = true;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out var localPointerPosition);
        dragOffset = localPointerPosition - rectTransform.anchoredPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dualManager.isDraging = false;
        initalPosition = rectTransform.anchoredPosition;
    }

    public void PanelOff()
    {
        dualManager.isSequenceRunning = true;
        rectTransform.DOAnchorPosX(-700, duration)
        .OnComplete(()=> 
        {
            gameObject.SetActive(false);
            dualManager.isSequenceRunning = false;
        }
        );
    }

    public void PanelOn()
    {
        dualManager.isSequenceRunning = true;
        gameObject.SetActive(true);
        rectTransform.DOAnchorPos(initalPosition, duration)
        .OnComplete(()=> dualManager.isSequenceRunning = false);
    }
}
