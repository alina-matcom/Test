using UnityEngine;

public class DeckController : MonoBehaviour
{
    public HandManager handManager;
    public Deck deck;

    void Start()
    {
        deck.Reset();
    }

    public void Draw(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Card drawnCard = deck.DrawRandomCard();
            if (!drawnCard) return;
            handManager.AddCard(drawnCard);
        }
    }
}
