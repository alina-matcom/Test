using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<Card> cards = new();
    public CardDisplay cardPrefab; // Assign your card prefab in the Inspector
    public float handWidth = 1200f; // Total width of the hand space
    public float cardWidth = 170f; // Width of each card
    public int maxCardsInHand = 10;

    public void AddCard(Card card)
    {
        if (transform.childCount > maxCardsInHand)
        {
            return;
        }

        cards.Add(card);
        CardDisplay newCard = Instantiate(cardPrefab, transform);
        newCard.card = card;
        newCard.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        StartCoroutine(AdjustCardPositions());
    }

    public void RemoveCard(CardDisplay card)
    {
        cards.Remove(card.card);
        Destroy(card.gameObject);

        StartCoroutine(AdjustCardPositions());
    }

    IEnumerator AdjustCardPositions()
    {
        yield return new WaitForSeconds(0.01f);

        int cardCount = transform.childCount;
        float totalCardsWidth = cardCount * cardWidth;
        bool fits = totalCardsWidth - handWidth < 0;
        float spacing = fits ? 20f : (handWidth - cardCount * cardWidth) / cardCount;
        float startX = (handWidth - (totalCardsWidth + spacing * (cardCount - 1))) / 2f;

        for (int i = 0; i < cardCount; i++)
        {
            Transform card = transform.GetChild(i);
            float xPosition = startX + i * (cardWidth + spacing);
            card.localPosition = new Vector3(xPosition, 0, (-i - 1) * 10);
        }
    }
}