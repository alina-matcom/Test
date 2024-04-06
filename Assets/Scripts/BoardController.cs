using System;
using Unity.VisualScripting;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public Zone playerMeleeZone;
    public Zone playerRangedZone;
    public Zone playerSiegeZone;
    public Slot playerMeleeBuff;
    public Slot playerRangedBuff;
    public Slot playerSiegeBuff;
    public PowerDisplay playerScore;
    protected int playerScoreCounter;

    public void Start()
    {
        playerScore.SetPower(0);
    }

    public void ShowPlayableSlots(CardDisplay card)
    {
        BoardSlot slot = GetZoneForCard(card.card);

        switch (slot)
        {
            case BoardSlot.PlayerMeleeZone:
                playerMeleeZone.unitsSlot.Highlight();
                break;
            case BoardSlot.PlayerRangedZone:
                playerRangedZone.unitsSlot.Highlight();
                break;
            case BoardSlot.PlayerSiegeZone:
                playerSiegeZone.unitsSlot.Highlight();
                break;
        }
    }

    public BoardSlot GetZoneForCard(Card card)
    {
        if (card is UnitCard unitCard)
        {
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

    public void PlayCard(Card card, Slot slot)
    {
        if (card is UnitCard unitCard)
        {
            switch (unitCard.type)
            {
                case UnitType.Melee:
                    playerMeleeZone.PlayCard(unitCard);
                    break;

                case UnitType.Ranged:
                    playerRangedZone.PlayCard(unitCard);
                    break;

                case UnitType.Siege:
                    playerSiegeZone.PlayCard(unitCard);
                    break;
            }

            playerScore.SetPower(GetPlayerScore());
        }
        else
        {
            slot.PlayCard(card);
        }
    }

    public int GetPlayerScore()
    {
        return
            playerMeleeZone.GetRowPower() +
            playerRangedZone.GetRowPower() +
            playerSiegeZone.GetRowPower();
    }

    public void UpdatePlayerScore()
    {
        playerScore.SetPower(GetPlayerScore());
    }
}
