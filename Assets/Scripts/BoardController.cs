using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardController : Singleton<BoardController>
{
    public Zone playerMeleeZone;
    public Zone playerRangedZone;
    public Zone playerSiegeZone;
    public Zone enemyMeleeZone;
    public Zone enemyRangedZone;
    public Zone enemySiegeZone;
    public PowerDisplay playerScore;
    public PowerDisplay enemyScore;
    protected int playerScoreCounter;
    protected int enemyScoreCounter;

    public void Start()
    {
        playerScore.SetPower(0);
    }

    public void ShowPlayableSlots(CardDisplay card, bool isPlayer)
    {
        BoardSlot slot = GetZoneForCard(card.card);

        Dictionary<BoardSlot, Action> slotActions = new()
        {
            { BoardSlot.PlayerMeleeZone, () => (isPlayer ? playerMeleeZone : enemyMeleeZone).unitsSlot.Highlight() },
            { BoardSlot.PlayerRangedZone, () => (isPlayer ? playerRangedZone : enemyRangedZone).unitsSlot.Highlight() },
            { BoardSlot.PlayerSiegeZone, () => (isPlayer ? playerSiegeZone : enemySiegeZone).unitsSlot.Highlight() },
            { BoardSlot.Buff, () =>
                {
                    if (isPlayer)
                    {
                        playerMeleeZone.buffSlot.Highlight();
                        playerRangedZone.buffSlot.Highlight();
                        playerSiegeZone.buffSlot.Highlight();
                    }
                    else
                    {
                        enemyMeleeZone.buffSlot.Highlight();
                        enemyRangedZone.buffSlot.Highlight();
                        enemySiegeZone.buffSlot.Highlight();
                    }
                }
            }
        };

        // Execute the action associated with the current slot
        if (slotActions.TryGetValue(slot, out var action))
        {
            action.Invoke();
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
            return BoardSlot.Buff;
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
        }

        Unhighlight();
        UpdateScore();
    }

    public void UpdateZonesPower()
    {

        playerMeleeZone.UpdateRowPower();
        playerRangedZone.UpdateRowPower();
        playerSiegeZone.UpdateRowPower();
        enemyMeleeZone.UpdateRowPower();
        enemyRangedZone.UpdateRowPower();
        enemySiegeZone.UpdateRowPower();
    }

    public void UpdateScore()
    {
        UpdateZonesPower();
        playerScore.SetPower(GetPlayerScore());
        enemyScore.SetPower(GetPlayerScore());
    }

    public int GetPlayerScore()
    {
        return
            playerMeleeZone.GetRowPower() +
            playerRangedZone.GetRowPower() +
            playerSiegeZone.GetRowPower();
    }

    public int GetEnemyScore()
    {
        return
            enemyMeleeZone.GetRowPower() +
            enemyRangedZone.GetRowPower() +
            enemySiegeZone.GetRowPower();
    }

    public void Unhighlight()
    {
        playerMeleeZone.Unhighlight();
        playerRangedZone.Unhighlight();
        playerSiegeZone.Unhighlight();
        enemyMeleeZone.Unhighlight();
        enemyRangedZone.Unhighlight();
        enemySiegeZone.Unhighlight();
    }
}
