public class CardSlot : Slot
{
  public CardDisplay slot;

  public void SetCard(CardDisplay card)
  {
    slot = card;
  }

  public void Clear()
  {
    slot = null;
  }

  void OnMouseDown()
  {
    if (_highlight.activeSelf)
    {
      SetCard(CardManager.Instance.selectedCard);
    }
  }
}