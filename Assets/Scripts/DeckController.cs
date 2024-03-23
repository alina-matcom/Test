using UnityEngine;

public class DeckController : MonoBehaviour
{
    public delegate void DeckClickedHandler(Card card);
    public static event DeckClickedHandler OnDeckClicked;
    public Deck deck;

    void Start()
    {
        deck.Reset();
    }


    void OnMouseDown()
    {
        Card drawnCard = deck.DrawRandomCard();
        if (!drawnCard) return;
        OnDeckClicked?.Invoke(drawnCard);
    }

}
