using UnityEngine;

public class BuffSlot : Slot
{
  public CardDisplay cardDisplay;
  public Card card;

  public override void PlayCard(Card card)
  {
    this.card = card;
    cardDisplay.SetCard(card);
    cardDisplay.gameObject.SetActive(true);
  }

  public void Clear()
  {
    card = null;
    cardDisplay.gameObject.SetActive(false);
  }
}