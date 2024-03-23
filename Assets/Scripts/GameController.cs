using DG.Tweening;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Card[] cards;
    public HandManager playerHandManager;
    public BoardController boardController;
    private Vector3 selectedCardOrigin;
    private readonly float animationTime = 0.3f;
    private readonly Vector3 finalPosition = new(7, 2, 0);
    private readonly float finalScale = 1.1f;
    private readonly float originalScale = 0.5f;

    void Start()
    {
        DeckController.OnDeckClicked += DrawCard;
        CardDisplay.OnCardSelect += SelectCard;
    }

    public void OnDestroy()
    {
        DeckController.OnDeckClicked -= DrawCard;
        CardDisplay.OnCardSelect -= SelectCard;
    }

    public void DrawCard(Card card)
    {
        playerHandManager.AddCard(card);
    }

    public void SelectCard(CardDisplay card)
    {
        if (CardManager.Instance.selectedCard == card)
        {
            return;
        }
        if (CardManager.Instance.selectedCard != null)
        {
            CardManager.Instance.selectedCard.transform.DOMove(selectedCardOrigin, animationTime);
            CardManager.Instance.selectedCard.transform.DOScale(originalScale, animationTime);
        }
        CardManager.Instance.selectedCard = card;
        selectedCardOrigin = card.transform.position;
        card.transform.DOMove(finalPosition, animationTime);
        CardManager.Instance.selectedCard.transform.DOScale(finalScale, animationTime);
        boardController.Highlight(card);
        // playerHandManager.RemoveCard(card);
    }

}
