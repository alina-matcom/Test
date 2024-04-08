using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int rowPower;
    public ZoneSlot unitsSlot;
    public BuffSlot buffSlot;
    public PowerDisplay powerDisplay;
    public UnitType rowType;

    public void Start()
    {
        powerDisplay.SetPower(0);
    }

    public void PlayCard(Card card)
    {
        if (card is UnitCard unitCard)
        {
            unitsSlot.PlayCard(unitCard);
        }
        else
        {
            buffSlot.PlayCard(card);
        }
    }

    public int GetRowPower()
    {
        return rowPower;
    }

    public void UpdateRowPower()
    {
        if (buffSlot.card)
        {
            unitsSlot.ApplyEffect(
                PowerModifier.Increment,
                (buffSlot.cardDisplay.card as BuffCard).powerIncrease
            );
        }

        rowPower = unitsSlot.GetRowPower();
        powerDisplay.SetPower(rowPower);
    }
}
