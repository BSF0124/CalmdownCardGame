using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class OppnentCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public Tracker tracker;

    private RectTransform rectTransform;
    private RectTransform cardImage;
    private Vector2 initalPosition;
    private Vector2 tracker_initalPosition;
    private Vector2 dragOffset;
    private float duration = 0.2f;
    
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        initalPosition = rectTransform.anchoredPosition;
        cardImage = transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!DualManager.instance.isDraging)
            cardImage.DOScale(new Vector3(1.1f, 1.1f, 1.1f), duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!DualManager.instance.isDraging)
            cardImage.DOScale(Vector3.one, duration);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!DualManager.instance.isDraging && !DualManager.instance.isSequenceRunning)
        {
            tracker.PanelOnOff();
        }
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
        rectTransform.DOAnchorPos(initalPosition, duration);
    }
}
