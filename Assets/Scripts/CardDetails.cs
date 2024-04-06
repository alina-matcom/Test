public class CardDetails : Singleton<CardDetails>
{
    public CardDisplay cardDisplay;

    public void Show(Card card)
    {
        cardDisplay.SetCard(card);
        cardDisplay.gameObject.SetActive(true);
    }
}
