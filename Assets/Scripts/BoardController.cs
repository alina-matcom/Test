using System;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public ZoneSlot playerMeleeZone;
    public ZoneSlot playerRangedZone;
    public ZoneSlot playerSiegeZone;
    public Slot playerMeleeBuff;
    public Slot playerRangedBuff;
    public Slot playerSiegeBuff;

    public void Highlight(CardDisplay card)
    {
        BoardSlot slot = GetZoneForCard(card.card);

        switch (slot)
        {
            case BoardSlot.PlayerMeleeZone:
                playerMeleeZone.Highlight();
                break;
            case BoardSlot.PlayerRangedZone:
                playerRangedZone.Highlight();
                break;
            case BoardSlot.PlayerSiegeZone:
                playerSiegeZone.Highlight();
                break;
        }
    }

    public BoardSlot GetZoneForCard(Card card)
    {
        if (card is UnitCard unitCard)
        {
            Debug.Log(unitCard.type);
            return unitCard.type switch
            {
                UnitType.Melee => BoardSlot.PlayerMeleeZone,
                UnitType.Ranged => BoardSlot.PlayerRangedZone,
                UnitType.Siege => BoardSlot.PlayerSiegeZone,
                _ => throw new ArgumentException("Invalid card type"),
            };
        }
        throw new ArgumentException("Invalid card type");
    }
}
