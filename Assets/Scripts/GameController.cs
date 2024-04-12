using UnityEngine;

public class GameController : Singleton<GameController>
{
    public HandManager playerHandManager;
    public DeckController playerDeck;
    public HandManager enemyHandManager;
    public DeckController enemyDeck;

    public int playerWinnedRounds = 0;
    public int enemyWinnedRounds = 0;
    public int round = 0;
    public int currentTurn = 0;
    public bool playerPassed = false;
    public bool enemyPassed = false;

    public delegate void HighlightHandler(BoardSlot slot, int player);
    public static event HighlightHandler OnHighlight;
    void Start()
    {
        HandCardDisplay.OnPlayCard += PlayCard;
        Slot.OnSlotSelected += PlaceCard;

        playerDeck.Draw(10);
        enemyDeck.Draw(10);
        BoardController.Instance.UpdateTurnIndicator(currentTurn);
    }

    public void OnDestroy()
    {
        HandCardDisplay.OnPlayCard -= PlayCard;
    }

    public void PlayCard(CardDisplay card)
    {
        if (CardManager.Instance.selectedCard == card)
        {
            return;
        }
        CardManager.Instance.selectedCard = card;
        OnHighlight?.Invoke(card.card.GetBoardSlot(), currentTurn);
    }

    public void PlaceCard(Slot slot)
    {
        if (!CardManager.Instance.selectedCard) return;

        BoardController.Instance.PlayCard(CardManager.Instance.selectedCard.card, slot, currentTurn);
        playerHandManager.RemoveCard(CardManager.Instance.selectedCard);
        CardManager.Instance.selectedCard = null;
        OnHighlight?.Invoke(BoardSlot.None, currentTurn);
        NextTurn();
    }

    public bool CheckRoundWinner()
    {
        int playerScore = BoardController.Instance.GetEnemyScore();
        int enemyScore = BoardController.Instance.GetEnemyScore();

        if (playerPassed && enemyPassed)
        {
            if (playerScore < enemyScore)
            {
                if (++playerWinnedRounds < 2) DeclareRoundWinner(0);
                else DeclareGameWinner(0);
            }
            else
            {
                if (++enemyWinnedRounds < 2) DeclareRoundWinner(1);
                else DeclareGameWinner(0);
            }

            return true;
        }

        return false;
    }

    public void Forfeit()
    {
        Debug.Log("yep");
        if (currentTurn == 0) playerPassed = true;
        else enemyPassed = true;
        NextTurn();
    }

    public void NextTurn()
    {
        if (CheckRoundWinner()) return;

        if (playerPassed && currentTurn == 1) return;
        if (enemyPassed && currentTurn == 0) return;

        currentTurn ^= 1;
        playerHandManager.Hide(currentTurn == 1);
        enemyHandManager.Hide(currentTurn == 0);
        BoardController.Instance.UpdateTurnIndicator(currentTurn);
    }

    public void NextRound(int winner)
    {
        round++;
        currentTurn = winner;

        playerDeck.Draw(2);
        enemyDeck.Draw(2);
    }

    public void DeclareRoundWinner(int winner)
    {
        Debug.Log("Player " + winner + " won this round!");
    }

    public void DeclareGameWinner(int winner)
    {
        Debug.Log("Player " + winner + " wins!!");
    }
}
