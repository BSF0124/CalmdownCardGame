using UnityEngine;

public class Dictionary : MonoBehaviour
{
    public GameObject cardPrefab;

    public CardRarity cardRarityOption;
    public CardType cardTypeOption;

    public bool isOptionChecked;

    void Start()
    {
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
            if(PlayerDataManager.instance.playerData.cardOwnerships[i].quantity == 0)
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int rand = Random.Range(0, 29);
            PlayerDataManager.instance.AddCard(rand, 1);
            print(rand);
        }
    }
}
