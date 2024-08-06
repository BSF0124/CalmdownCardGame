using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class OppoenetCardData
{
    public int cardID;
    public CardRarity cardRarity;
    public CardType cardType;
    public int attackPower;
    public int life;
    public Sprite cardSprite;

    public OppoenetCardData(int cardID)
    {
        this.cardID = cardID;
        cardRarity = CardDataManager.instance.allCards[cardID].cardRarity;
        cardType = CardDataManager.instance.allCards[cardID].cardType;
        attackPower = CardDataManager.instance.allCards[cardID].attackPower;
        cardSprite = CardDataManager.instance.allCards[cardID].cardSprite;
    }
}

public class OpponentCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public DualManager dualManager;
    public Tracker tracker;
    public Sprite backImage;

    private RectTransform rectTransform;
    private RectTransform cardImage;
    private Vector2 initalPosition;
    private Vector2 dragOffset;
    private float duration = 0.2f;

    public List<OppoenetCardData> opponentCards = new List<OppoenetCardData>();
    public List<int> opponentDeck = new List<int>();
    
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
            StartCoroutine(Flip(Random.Range(0, opponentCards.Count)));
    }

    public void Init()
    {

        rectTransform = GetComponent<RectTransform>();
        initalPosition = rectTransform.anchoredPosition;
        cardImage = transform.GetChild(0).GetComponent<RectTransform>();

        for(int i=0; i<Random.Range(1, 20); i++)
        {
            opponentDeck.Add(Random.Range(0, 30));
        }
        
        foreach(int item in opponentDeck)
        {
            opponentCards.Add(new OppoenetCardData(item));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!dualManager.isDraging)
            cardImage.DOScale(new Vector3(1.1f, 1.1f, 1.1f), duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!dualManager.isDraging)
            cardImage.DOScale(Vector3.one, duration);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!dualManager.isDraging && !dualManager.isSequenceRunning)
        {
            if(tracker.gameObject.activeSelf)
                tracker.PanelOff();
            else
                tracker.PanelOn();
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

    public IEnumerator Flip(int index)
    {
        Image image = cardImage.GetComponent<Image>();
        yield return rectTransform.DORotate(new Vector3(0, 90, 0), 0.5f);
        yield return new WaitForSeconds(1);
        image.sprite = image.sprite==backImage? opponentCards[index].cardSprite : backImage;
        yield return rectTransform.DORotate(Vector3.zero, 0.5f);
    }
}
