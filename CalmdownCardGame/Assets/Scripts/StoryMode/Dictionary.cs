using UnityEngine;

public class Dictionary : MonoBehaviour
{
    public GameObject cardPrefab;

    public CardRarity cardRarityOption;
    public CardType cardTypeOption;

    public bool isOptionChecked;

    void Start()
    {
        cardRarityOption = CardRarity.Null;
        cardTypeOption = CardType.All;
        SetCard();
        CardUpdate();
    }

    void SetCard()
    {
        for(int i=0; i<30; i++)
        {
            GameObject card = Instantiate(cardPrefab, transform);
            card.GetComponent<HoloCard>().Init(i);
        }
    }

    void CardUpdate()
    {
        for(int i=0; i<30; i++)
        {
            HoloCard holoCard = transform.GetChild(i).GetComponent<HoloCard>();

            if(PlayerDataManager.instance.playerData.cardOwnerships[i].quantity == 0)
                holoCard.gameObject.SetActive(false);

            else if(cardRarityOption != CardRarity.Null && holoCard.cardRarity != cardRarityOption)
                holoCard.gameObject.SetActive(false);
            
            else if(cardTypeOption != CardType.All && holoCard.cardType != cardTypeOption)
                holoCard.gameObject.SetActive(false);

            else
                holoCard.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int rand = Random.Range(0, 30);
            PlayerDataManager.instance.AddCard(rand, 1);
            print(rand);
        }
    }

    public void SetRarity(int num)
    {
        if(cardRarityOption == (CardRarity)num)
            cardRarityOption = CardRarity.Null;
        else
            cardRarityOption = (CardRarity)num;
        
        CardUpdate();
    }

    public void SetType(int num)
    {
        if(cardTypeOption == (CardType)num)
            cardTypeOption = CardType.All;
        else
            cardTypeOption = (CardType)num;

        CardUpdate();
    }
}
