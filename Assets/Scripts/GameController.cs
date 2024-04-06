using UnityEngine;

public class GameController : Singleton<GameController>
{
    public HandManager playerHandManager;
    public BoardController boardController;

    void Start()
    {
        DeckController.OnDeckClicked += DrawCard;
        HandCardDisplay.OnPlayCard += PlayCard;
        Slot.OnSlotSelected += PlaceCard;
    }

    public void OnDestroy()
    {
        DeckController.OnDeckClicked -= DrawCard;
        HandCardDisplay.OnPlayCard -= PlayCard;
    }

    public void DrawCard(Card card)
    {
        playerHandManager.AddCard(card);
    }

    public void PlayCard(CardDisplay card)
    {
        if (CardManager.Instance.selectedCard == card)
        {
            return;
        }
        CardManager.Instance.selectedCard = card;
        boardController.ShowPlayableSlots(card);
    }

    public void PlaceCard(Slot slot)
    {
        if (CardManager.Instance.selectedCard)
        {
            boardController.PlayCard(CardManager.Instance.selectedCard.card, slot);
            playerHandManager.RemoveCard(CardManager.Instance.selectedCard);
            CardManager.Instance.selectedCard = null;
        }
    }
}
