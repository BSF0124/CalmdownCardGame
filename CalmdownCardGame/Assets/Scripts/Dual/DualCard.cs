using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DualCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private RectTransform cardImage;

    public Canvas canvas;
    private Vector2 initalPosition;
    private Vector2 dragOffset;
    private bool isSelected = false;

    private float duration = 0.2f;
    public int cardID;
    public CardRarity cardRarity;
    public CardType cardType;
    public int attackPower;
    public int life;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        cardImage = transform.GetChild(0).GetComponent<RectTransform>();

        // cardRarity = CardDataManager.instance.allCards[cardID].cardRarity;
        // cardType = CardDataManager.instance.allCards[cardID].cardType;
        // attackPower = CardDataManager.instance.allCards[cardID].attackPower;

        // switch(cardRarity)
        // {
        //     case CardRarity.N:
        //         life = 1;
        //         break;
        //     case CardRarity.R:
        //         life = 2;
        //         break;
        //     case CardRarity.SR:
        //         life = 3;
        //         break;
        //     case CardRarity.Null:
        //         life = -1;
        //         break;
        // }
    }

    private void OnEnable() 
    {
        initalPosition = rectTransform.anchoredPosition;
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
        if(!DualManager.instance.isDraging)
        {
            if(isSelected)
                Deselect();
                
            else
                Select();
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

    public void Select()
    {
        DualManager.instance.SelectCard(this);
        isSelected = true;
        rectTransform.DOAnchorPos(initalPosition + new Vector2(0, 50), duration);
    }

    public void Deselect()
    {
        isSelected = false;
        rectTransform.DOAnchorPos(initalPosition, duration);
        DualManager.instance.DeselectCard(this);
    }
}
