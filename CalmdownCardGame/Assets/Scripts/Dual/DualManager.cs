using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DualManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject hand;
    public GameObject field;
    public Tracker tracker;
    public OpponentCard opponentCard;
    public GameObject playerCardPrefab;

    [HideInInspector] public DualCard selectedCard;
    [HideInInspector] public bool isDraging = false;
    [HideInInspector] public bool isSequenceRunning = false;

    public List<DualCard> playerCards;
    public List<int> playerDeck;
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

        if(Input.GetKeyDown(KeyCode.Return))
            StartCoroutine(Dual());
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

    public IEnumerator Dual()
{
    isSequenceRunning = true;  // 시퀀스가 실행 중임을 표시
    yield return null;

    if (selectedCard != null)
    {
        // 원래 위치와 회전값 저장
        RectTransform rect = selectedCard.transform.GetComponent<RectTransform>();
        Vector2 originalPosition = rect.anchoredPosition;
        Quaternion originalRotation = rect.rotation;

        // 현재 형제 인덱스 저장
        int siblingIndex = selectedCard.transform.GetSiblingIndex();
        bool isTrackerActive = tracker.gameObject.activeSelf;

        // 선택된 카드를 필드로 이동
        selectedCard.transform.SetParent(field.transform);
        yield return selectedCard.transform.GetComponent<RectTransform>().DORotate(Vector2.zero, 0.5f).WaitForCompletion();
        yield return selectedCard.transform.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f).WaitForCompletion();

        HideHand();
        if (isTrackerActive)
        {
            tracker.PanelOff();  // 트래커 패널 끄기
        }

        yield return new WaitForSeconds(1f);

        // 상대방 카드 뒤집기
        yield return StartCoroutine(opponentCard.Flip(Random.Range(0, opponentCard.opponentCards.Count)));

        // 여기서 승부 결과 로직을 추가할 수 있습니다
        // 예: 승리/패배를 결정하고 게임 상태를 업데이트

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(opponentCard.Flip(Random.Range(0, opponentCard.opponentCards.Count)));

        // 선택된 카드를 화면 밖으로 이동
        yield return selectedCard.transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -700f), 0.5f).WaitForCompletion();

        // 선택된 카드를 원래 상태로 복원
        selectedCard.transform.SetParent(hand.transform);
        selectedCard.transform.SetSiblingIndex(siblingIndex);
        rect.anchoredPosition = originalPosition;
        rect.rotation = originalRotation;
        selectedCard = null;
        ShowHand();
        if (isTrackerActive)
        {
            tracker.PanelOn();  // 트래커 패널 다시 켜기
        }
    }

    isSequenceRunning = false;  // 시퀀스 종료 표시
}


    // public IEnumerator Dual()
    // {
    //     isSequenceRunning = true;
    //     yield return null;

    //     if(selectedCard != null)
    //     {
    //         RectTransform rect = selectedCard.transform.GetComponent<RectTransform>();
    //         Vector2 _position = rect.rect.position;
    //         Quaternion _quaternion = rect.rotation;

    //         int siblingIndex = selectedCard.transform.GetSiblingIndex();
    //         bool isTrackerActivate = tracker.gameObject.activeSelf;

    //         selectedCard.transform.parent = field.transform;
    //         yield return selectedCard.transform.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f);
    //         yield return selectedCard.transform.GetComponent<RectTransform>().DORotate(Vector2.zero, 0.5f);
            
    //         HideHand();
    //         if(isTrackerActivate)
    //             tracker.PanelOff();
    //         yield return new WaitForSeconds(1f);

    //         yield return StartCoroutine(opponentCard.Flip(Random.Range(0, opponentCard.opponentCards.Count)));
    //         /*
    //         승부 결과
    //         */
    //         yield return new WaitForSeconds(1f);

    //         yield return selectedCard.transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -500f), 0.5f);
            
    //         selectedCard.transform.parent = hand.transform;
    //         selectedCard.transform.SetSiblingIndex(siblingIndex);
    //         rect.anchoredPosition = _position;
    //         rect.rotation = _quaternion;
    //         ShowHand();
    //         if(isTrackerActivate)
    //             tracker.PanelOn();
    //     }
        
    //     else
    //     {

    //     }
    //     isSequenceRunning = false;
    // }

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
        
        if(playerCards.Count > 5)
        {
            HideHand();
            yield return new WaitForSeconds(1f);
        }
        SetHand();

        ShowHand();
        yield return new WaitForSeconds(1f);

        isSequenceRunning = false;
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

    public void ShowHand()
    {
        RectTransform handPosition = hand.GetComponent<RectTransform>();
        handPosition.DOAnchorPosY(236, 0.5f);
    }
    public void HideHand()
    {
        RectTransform handPosition = hand.GetComponent<RectTransform>();
        handPosition.DOAnchorPosY(-200, 0.5f);
    }
}
