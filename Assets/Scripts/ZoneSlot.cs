using System.Collections;
using System.Linq;
using UnityEngine;

public class ZoneSlot : Slot
{
  public CardDisplay cardPrefab;
  public GameObject slot;
  private readonly float cardWidth = 114f;
  private readonly float zoneWidth = 600f;

  public override void PlayCard(Card card)
  {
    if (card is UnitCard unitCard)
    {
      CardDisplay cardDisplay = Instantiate(cardPrefab, slot.transform);
      cardDisplay.SetCard(unitCard);
      cardDisplay.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
      cardDisplay.transform.localPosition = new Vector3(0, 0, 0);
      _highlight.SetActive(false);
      StartCoroutine(AdjustCardPositions());
    }
  }

  public int GetRowPower()
  {
    return slot.transform
      .GetChild(0)
      .GetComponents<CardDisplay>()
      .Sum(card => (card.card as UnitCard).power);
  }

  IEnumerator AdjustCardPositions()
  {
    yield return new WaitForSeconds(0.01f);

    int cardCount = slot.transform.childCount;
    float totalCardsWidth = cardCount * cardWidth;
    bool fits = totalCardsWidth - zoneWidth < 0;
    float spacing = fits ? 10f : (zoneWidth - cardCount * cardWidth) / cardCount;
    float startX = (zoneWidth - (totalCardsWidth + spacing * (cardCount - 1))) / 2f;

    for (int i = 0; i < cardCount; i++)
    {
      Transform card = slot.transform.GetChild(i);
      float xPosition = startX + i * (cardWidth + spacing);
      card.localPosition = new Vector3(xPosition - zoneWidth / 2f, 0, (-i - 1) * 10);
    }
  }
}