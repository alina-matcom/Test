using System.Collections;
using UnityEngine;

public class ZoneSlot : Slot
{
  public Zone zone;
  public CardDisplay cardPrefab;
  public GameObject slot;
  private readonly float cardWidth = 114f;
  private readonly float zoneWidth = 600f;

  void OnMouseDown()
  {
    if (_highlight.activeSelf)
    {
      CardDisplay card = CardManager.Instance.selectedCard;
      zone.AddCard(card.card as UnitCard);
      card.transform.localPosition = new Vector3(0, 0, 0);
      card.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
      card.transform.SetParent(slot.transform, false);
      CardManager.Instance.selectedCard = null;
      _highlight.SetActive(false);
      StartCoroutine(AdjustCardPositions());
    }
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