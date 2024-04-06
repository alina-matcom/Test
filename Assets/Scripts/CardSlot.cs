public class BuffSlot : Slot
{
  public CardDisplay slot;

  public override void PlayCard(Card card)
  {
    slot.SetCard(card);
  }

  public void Clear()
  {
    slot = null;
  }
}