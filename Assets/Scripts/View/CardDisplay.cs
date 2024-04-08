using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public Text nameText;
    public Text descText;
    public PowerDisplay powerDisplay;
    public Image unitTypeImage;
    public Image kindBorderImage;
    public Image kindBannerImage;

    protected void Start()
    {
        SetCard(card);
    }

    public void SetCard(Card newCard)
    {
        card = newCard;

        kindBorderImage.sprite = Resources.Load<Sprite>("card-border-" + card.kind.ToString().ToLower());
        kindBannerImage.sprite = Resources.Load<Sprite>(card.kind.ToString().ToLower());

        if (card is UnitCard unitCard)
        {
            unitTypeImage.sprite = Resources.Load<Sprite>(unitCard.type.ToString().ToLower());
            powerDisplay.SetPower(unitCard.power);
            powerDisplay.gameObject.SetActive(true);
        }
        else
        {
            if (card is FieldCard)
            {
                unitTypeImage.sprite = Resources.Load<Sprite>("field");
            }
            else if (card is BuffCard)
            {
                unitTypeImage.sprite = Resources.Load<Sprite>("buff");
            }
            powerDisplay.gameObject.SetActive(false);
        }
    }

    public void SetPower(int power)
    {
        if (card.kind == CardKind.Gold) return;
        if (card is UnitCard unitCard)
        {
            unitCard.power = power;
            powerDisplay.SetPower(power);
        }
    }
}
