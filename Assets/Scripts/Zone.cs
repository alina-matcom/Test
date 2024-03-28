using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public List<UnitCard> cards = new();
    public int rowPower;
    public PowerDisplay powerDisplay;

    public void Start()
    {
        powerDisplay.SetPower(0);
    }

    public void AddCard(UnitCard card)
    {
        cards.Add(card);
        rowPower += card.power;
        powerDisplay.SetPower(rowPower);
    }

    public void RemoveCard(UnitCard card)
    {
        cards.Remove(card);
        rowPower -= card.power;
        powerDisplay.SetPower(rowPower);
    }
}
