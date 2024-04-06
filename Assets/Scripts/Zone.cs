using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int rowPower;
    public ZoneSlot unitsSlot;
    public Slot buffSlot;
    public PowerDisplay powerDisplay;

    public void Start()
    {
        powerDisplay.SetPower(0);
    }

    public void PlayCard(Card card)
    {
        if (card is UnitCard unitCard)
        {
            unitsSlot.PlayCard(unitCard);
            UpdateRowPower();
        }
        else
        {
            buffSlot.PlayCard(card);
        }
    }

    public void RemoveCard(UnitCard card)
    {
        rowPower -= card.power;
        powerDisplay.SetPower(rowPower);
    }

    public int GetRowPower()
    {
        return rowPower;
    }

    public void UpdateRowPower()
    {
        rowPower = unitsSlot.GetRowPower();
        powerDisplay.SetPower(rowPower);
    }
}
