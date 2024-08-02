using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DualManager : MonoBehaviour
{
    public static DualManager instance;

    public GameObject playerCardPrefab;
    public List<DualCard> playerDeck;
    private List<DualCard> opponentDeck;

    [HideInInspector] public DualCard selectedCard;
    [HideInInspector] public bool isDraging = false;

    public GameObject hand;
    public int currentHandIndex;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            ChangeHand();
    }

    private void Init()
    {
        currentHandIndex = -1;

        foreach(Transform child in hand.transform)
        {
            playerDeck.Add(child.GetComponent<DualCard>());
        }
    }

    public void Dual()
    {

    }

    private void FlipOppnentCard()
    {

    }

    public void SelectCard(DualCard card)
    {
        if(selectedCard != null && selectedCard != card)
            selectedCard.Deselect();

        selectedCard = card;
    }

    public void DeselectCard(DualCard card)
    {
        if(selectedCard == card)
        {
            selectedCard = null;
        }
    }

    public void SetHand()
    {
        if(currentHandIndex < playerDeck.Count-1)
        {
            foreach(Transform child in hand.transform)
            {
                child.gameObject.SetActive(false);
            }

            currentHandIndex++;
            int temp = playerDeck.Count-1 - currentHandIndex;
            if(temp >= 4)
            {
                RectTransform card1 = hand.transform.GetChild(currentHandIndex).GetComponent<RectTransform>();
                RectTransform card2 = hand.transform.GetChild(currentHandIndex+1).GetComponent<RectTransform>();
                RectTransform card3 = hand.transform.GetChild(currentHandIndex+2).GetComponent<RectTransform>();
                RectTransform card4 = hand.transform.GetChild(currentHandIndex+3).GetComponent<RectTransform>();
                RectTransform card5 = hand.transform.GetChild(currentHandIndex+4).GetComponent<RectTransform>();
                
                card1.anchoredPosition = new Vector2(-300, -175.9f);
                card2.anchoredPosition = new Vector2(-150, -155.9f);
                card3.anchoredPosition = new Vector2(0, -145.9f);
                card4.anchoredPosition = new Vector2(150, -155.9f);
                card5.anchoredPosition = new Vector2(300, -175.9f);

                card1.rotation = Quaternion.Euler(0, 0, 10);
                card2.rotation = Quaternion.Euler(0, 0, 5);
                card3.rotation = Quaternion.Euler(0, 0, 0);
                card4.rotation = Quaternion.Euler(0, 0, -5);
                card5.rotation = Quaternion.Euler(0, 0, -10);

                card1.gameObject.SetActive(true);
                card2.gameObject.SetActive(true);
                card3.gameObject.SetActive(true);
                card4.gameObject.SetActive(true);
                card5.gameObject.SetActive(true);

                currentHandIndex+=4;
            }

            else
            {
                switch(temp)
                {
                    case 0:
                        RectTransform card1 = hand.transform.GetChild(currentHandIndex).GetComponent<RectTransform>();
                        card1.anchoredPosition = new Vector2(0, -145.9f);
                        card1.rotation = Quaternion.Euler(0, 0, 0);
                        card1.gameObject.SetActive(true);
                        break;

                    case 1:
                        RectTransform card2 = hand.transform.GetChild(currentHandIndex).GetComponent<RectTransform>();
                        RectTransform card3 = hand.transform.GetChild(currentHandIndex+1).GetComponent<RectTransform>();
                        card2.anchoredPosition = new Vector2(-75, -155.9f);
                        card3.anchoredPosition = new Vector2(75, -155.9f);
                        card2.rotation = Quaternion.Euler(0, 0, 5);
                        card3.rotation = Quaternion.Euler(0, 0, -5);
                        card2.gameObject.SetActive(true);
                        card3.gameObject.SetActive(true);
                        currentHandIndex+=1;
                        break;

                    case 2:
                        RectTransform card4 = hand.transform.GetChild(currentHandIndex).GetComponent<RectTransform>();
                        RectTransform card5 = hand.transform.GetChild(currentHandIndex+1).GetComponent<RectTransform>();
                        RectTransform card6 = hand.transform.GetChild(currentHandIndex+2).GetComponent<RectTransform>();
                        card4.anchoredPosition = new Vector2(-150, -155.9f);
                        card5.anchoredPosition = new Vector2(0, -145.9f);
                        card6.anchoredPosition = new Vector2(150, -155.9f);
                        card4.rotation = Quaternion.Euler(0, 0, 5);
                        card5.rotation = Quaternion.Euler(0, 0, 0);
                        card6.rotation = Quaternion.Euler(0, 0, -5);
                        card4.gameObject.SetActive(true);
                        card5.gameObject.SetActive(true);
                        card6.gameObject.SetActive(true);
                        currentHandIndex+=2;
                        break;

                    case 3:
                        RectTransform card7 = hand.transform.GetChild(currentHandIndex).GetComponent<RectTransform>();
                        RectTransform card8 = hand.transform.GetChild(currentHandIndex+1).GetComponent<RectTransform>();
                        RectTransform card9 = hand.transform.GetChild(currentHandIndex+2).GetComponent<RectTransform>();
                        RectTransform card10 = hand.transform.GetChild(currentHandIndex+3).GetComponent<RectTransform>();
                        card7.anchoredPosition = new Vector2(-225, -175.9f);
                        card8.anchoredPosition = new Vector2(-75, -155.9f);
                        card9.anchoredPosition = new Vector2(75, -155.9f);
                        card10.anchoredPosition = new Vector2(225, -175.9f);
                        card7.rotation = Quaternion.Euler(0, 0, 10);
                        card8.rotation = Quaternion.Euler(0, 0, 5);
                        card9.rotation = Quaternion.Euler(0, 0, -5);
                        card10.rotation = Quaternion.Euler(0, 0, -10);
                        card7.gameObject.SetActive(true);
                        card8.gameObject.SetActive(true);
                        card9.gameObject.SetActive(true);
                        card10.gameObject.SetActive(true);
                        currentHandIndex+=3;
                        break;

                }

            }
        }

        else
        {
            if(currentHandIndex == playerDeck.Count-1 && currentHandIndex == 0)
            {
                RectTransform card1 = hand.transform.GetChild(currentHandIndex++).GetComponent<RectTransform>();
                card1.anchoredPosition = new Vector2(0, -145.9f);
                card1.Rotate(0, 0, 0);
                card1.gameObject.SetActive(true);
            }

            else
            {
                currentHandIndex = -1;
                SetHand();
            }
        }

    }

    public void ChangeHand()
    {
        SetHand();
    }
}
