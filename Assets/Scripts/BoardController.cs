using System;
using Unity.VisualScripting;
using UnityEngine;

public class BoardController : Singleton<BoardController>
{
    public Zone playerMeleeZone;
    public Zone playerRangedZone;
    public Zone playerSiegeZone;
    public BuffSlot playerMeleeBuff;
    public BuffSlot playerRangedBuff;
    public BuffSlot playerSiegeBuff;
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
            case BoardSlot.PlayerMeleeBuff:
                playerMeleeBuff.Highlight();
                playerRangedBuff.Highlight();
                playerSiegeBuff.Highlight();
                break;
        }
    }

    public BoardSlot GetZoneForCard(Card card)
    {
        Unhighlight();

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
        else if (card is BuffCard)
        {
            return BoardSlot.PlayerMeleeBuff;
        }
        else if (card is FieldCard)
        {
            return BoardSlot.FieldZone;
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

        }
        else if (slot is BuffSlot)
        {
            slot.PlayCard(card);
            playerMeleeBuff.Unhighlight();
            playerRangedBuff.Unhighlight();
            playerSiegeBuff.Unhighlight();
        }

        UpdateScore();
    }

    public void UpdateScore()
    {

        playerMeleeZone.UpdateRowPower();
        playerRangedZone.UpdateRowPower();
        playerSiegeZone.UpdateRowPower();
        playerScore.SetPower(GetPlayerScore());
    }

    public int GetPlayerScore()
    {
        return
            playerMeleeZone.GetRowPower() +
            playerRangedZone.GetRowPower() +
            playerSiegeZone.GetRowPower();
    }

    public void Unhighlight()
    {
        playerMeleeZone.unitsSlot.Unhighlight();
        playerRangedZone.unitsSlot.Unhighlight();
        playerSiegeZone.unitsSlot.Unhighlight();
        playerMeleeBuff.Unhighlight();
        playerRangedBuff.Unhighlight();
        playerSiegeBuff.Unhighlight();
    }
}
