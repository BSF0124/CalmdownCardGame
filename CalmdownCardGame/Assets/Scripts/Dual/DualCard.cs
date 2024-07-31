using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DualCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private RectTransform cardImage;

    public Canvas canvas;
    private Vector2 initalPosition;
    private bool isDrag = false;
    
    private float duration = 0.2f;
    public int cardID;
    public CardRarity cardRarity;
    public CardType cardType;
    public int attackPower;
    public int life;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        cardImage = transform.GetChild(0).GetComponent<RectTransform>();
        initalPosition = rectTransform.anchoredPosition;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isDrag)
            cardImage.DOScale(new Vector3(1.1f, 1.1f, 1.1f), duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isDrag)
            cardImage.DOScale(Vector3.one, duration);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out mousePosition);
        rectTransform.anchoredPosition = mousePosition + new Vector2(rectTransform.rect.width/2, rectTransform.rect.height/2);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("Drag Begin");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("Drag End");
    }
}
