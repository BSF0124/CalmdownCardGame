using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DualManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject playerCardPrefab;
    public GameObject hand;

    [HideInInspector] public DualCard selectedCard;
    [HideInInspector] public bool isDraging = false;
    [HideInInspector] public bool isSequenceRunning = false;

    public List<DualCard> playerCards;
    private List<DualCard> opponentCards;
    public List<int> playerDeck;
    private List<int> opponentDeck;
    private int currentHandIndex;

    private void Start()
    {
        Init();
        StartCoroutine(ChangeHand());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isSequenceRunning)
            StartCoroutine(ChangeHand());

        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("Dual");
    }

    private void Init()
    {
        for(int i=0; i<Random.Range(1, 20); i++)
        {
            playerDeck.Add(Random.Range(0, 30));
        }

        foreach(int item in playerDeck)
        {
            GameObject playerCard = Instantiate(playerCardPrefab, hand.transform);
            DualCard dualCard = playerCard.GetComponent<DualCard>();
            dualCard.dualManager = transform.GetComponent<DualManager>();
            dualCard.canvas = canvas;
            dualCard.Init(item);
        }

        foreach(Transform child in hand.transform)
        {
            playerCards.Add(child.GetComponent<DualCard>());
        }
        currentHandIndex = -1;
    }

    public void Dual()
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
        if(currentHandIndex < playerCards.Count-1)
        {
            foreach(Transform child in hand.transform)
            {
                child.gameObject.SetActive(false);
            }

            currentHandIndex++;
            int temp = playerCards.Count-1 - currentHandIndex;
            if(temp >= 4)
            {
                Vector2[] positions = new Vector2[]
                {
                    new Vector2(-300, -175.9f),
                    new Vector2(-150, -155.9f),
                    new Vector2(0, -145.9f),
                    new Vector2(150, -155.9f),
                    new Vector2(300, -175.9f)
                };
                float[] rotations = new float[] { 10, 5, 0, -5, -10 };

                for (int i = 0; i < 5; i++)
                {
                    RectTransform card = hand.transform.GetChild(currentHandIndex + i).GetComponent<RectTransform>();
                    card.anchoredPosition = positions[i];
                    card.rotation = Quaternion.Euler(0, 0, rotations[i]);
                    card.gameObject.SetActive(true);
                }

                currentHandIndex += 4;
            }

            else
            {
                switch(temp)
                {
                    case 0:
                        SetCard(currentHandIndex, new Vector2(0, -145.9f), 0);
                        break;

                    case 1:
                        SetCards(
                        new int[] { currentHandIndex, currentHandIndex + 1 },
                        new Vector2[] { new Vector2(-75, -155.9f), new Vector2(75, -155.9f) },
                        new float[] { 5, -5 }
                        );
                        currentHandIndex += 1;
                        break;

                    case 2:
                        SetCards(
                        new int[] { currentHandIndex, currentHandIndex + 1, currentHandIndex + 2 },
                        new Vector2[] { new Vector2(-150, -155.9f), new Vector2(0, -145.9f), new Vector2(150, -155.9f) },
                        new float[] { 5, 0, -5 }
                        );
                        currentHandIndex += 2;
                        break;

                    case 3:
                        SetCards(
                        new int[] { currentHandIndex, currentHandIndex + 1, currentHandIndex + 2, currentHandIndex + 3 },
                        new Vector2[] { new Vector2(-225, -175.9f), new Vector2(-75, -155.9f), new Vector2(75, -155.9f), new Vector2(225, -175.9f) },
                        new float[] { 10, 5, -5, -10 }
                        );
                        currentHandIndex += 3;
                        break;

                }

            }
        }

        else
        {
            if(currentHandIndex == playerCards.Count-1 && currentHandIndex == 0)
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

    IEnumerator ChangeHand()
    {
        isSequenceRunning = true;
        RectTransform handPosition = hand.GetComponent<RectTransform>();
        
        if(playerCards.Count > 5)
        {
            yield return handPosition.DOAnchorPosY(-200, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }

        SetHand();
        yield return handPosition.DOAnchorPosY(236, 0.5f)
        .OnComplete(()=> isSequenceRunning = false);
    }

    private void SetCard(int index, Vector2 position, float rotation)
    {
        RectTransform card = hand.transform.GetChild(index).GetComponent<RectTransform>();
        card.anchoredPosition = position;
        card.rotation = Quaternion.Euler(0, 0, rotation);
        card.gameObject.SetActive(true);
    }
    private void SetCards(int[] indices, Vector2[] positions, float[] rotations)
    {
        for (int i = 0; i < indices.Length; i++)
        {
            SetCard(indices[i], positions[i], rotations[i]);
        }
    }
}
