using UnityEngine;

public class CardDataManager : MonoBehaviour
{
    public static CardDataManager instance;
    
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

        LoadAllCards();
    }

    public CardData[] allCards;

    private void LoadAllCards()
    {
        allCards = Resources.LoadAll<CardData>("Cards");
    }

    public CardData GetcardByID(int id)
    {
        foreach (CardData card in allCards)
        {
            if(card.cardID == id)
            {
                return card;
            }
        }
        return null;
    }
}