using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "NewDeck", menuName = "Deck")]
public class Deck : ScriptableObject
{
    public List<Card> originalCards = new List<Card>(); 
    public List<Card> cards = new List<Card>();

    public void Reset()
    {
        cards.Clear();
        foreach (Card card in originalCards)
        {
            card.Reset();
            cards.Add(card);
        }
    }

    public Card DrawRandomCard()
    {
        if (cards.Count == 0)
        {
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, cards.Count);
        Card drawnCard = cards[randomIndex];
        cards.RemoveAt(randomIndex);

        return drawnCard;
    }
}