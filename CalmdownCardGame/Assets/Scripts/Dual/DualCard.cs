using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DualCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public DualManager dualManager;

    private RectTransform rectTransform;
    private RectTransform cardImage;
    private RectTransform statusPanel;

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
        rectTransform = GetComponent<RectTransform>();
        cardImage = transform.GetChild(0).GetComponent<RectTransform>();
        statusPanel = transform.GetChild(1).GetComponent<RectTransform>();
    }

    public void Init(int cardID)
    {
        this.cardID = cardID;

        cardRarity = CardDataManager.instance.allCards[cardID].cardRarity;
        cardType = CardDataManager.instance.allCards[cardID].cardType;
        attackPower = CardDataManager.instance.allCards[cardID].attackPower;

        switch(cardRarity)
        {
            case CardRarity.N:
                life = 1;
                break;
            case CardRarity.R:
                life = 2;
                break;
            case CardRarity.SR:
                life = 3;
                break;
            case CardRarity.Null:
                life = -1;
                break;
        }
        cardImage.GetComponent<Image>().sprite = CardDataManager.instance.allCards[cardID].cardSprite;
    }

    private void OnEnable() 
    {
        initalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!dualManager.isDraging)
        {
            cardImage.DOScale(new Vector3(1.1f, 1.1f, 1.1f), duration).OnComplete(() =>
            {
                statusPanel.localScale = Vector3.zero;
                statusPanel.gameObject.SetActive(true);
                statusPanel.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
            });
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!dualManager.isDraging)
        {
            cardImage.DOScale(Vector3.one, duration).OnComplete(() =>
            {
                statusPanel.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    statusPanel.gameObject.SetActive(false);
                });
            });
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!dualManager.isDraging)
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
        dualManager.isDraging = true;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out var localPointerPosition);
        dragOffset = localPointerPosition - rectTransform.anchoredPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dualManager.isDraging = false;
        rectTransform.DOAnchorPos(initalPosition, duration);
    }

    public void Select()
    {
        dualManager.SelectCard(this);
        isSelected = true;
        rectTransform.DOAnchorPos(initalPosition + new Vector2(0, 50), duration);
    }

    public void Deselect()
    {
        isSelected = false;
        rectTransform.DOAnchorPos(initalPosition, duration);
        dualManager.DeselectCard(this);
    }
}
