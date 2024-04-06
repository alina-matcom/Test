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
        if (card is UnitCard unitCard)
        {
            unitTypeImage.sprite = Resources.Load<Sprite>(unitCard.type.ToString().ToLower());
            kindBorderImage.sprite = Resources.Load<Sprite>("card-border-" + unitCard.kind.ToString().ToLower());
            kindBannerImage.sprite = Resources.Load<Sprite>(unitCard.kind.ToString().ToLower());
            powerDisplay.SetPower(unitCard.power);
        }
    }
}
