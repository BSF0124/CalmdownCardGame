using System.Collections.Generic;
using UnityEngine;

public class DualManager : MonoBehaviour
{
    public static DualManager instance;

    private List<DualCard> playerDeck;
    private List<DualCard> opponentDeck;

    [HideInInspector] public DualCard selectedCard;
    [HideInInspector] public bool isDraging = false;

    public GameObject hand;

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
}
