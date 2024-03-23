using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public Text nameText;
    public Text descText;
    public Image powerImage;
    public Image unitTypeImage;
    public Image kindBorderImage;
    public Image kindBannerImage;
    private Vector3 originalScale;
    // private readonly float scaleFactor = 1.5f;
    private readonly float transitionDuration = 0.1f;

    public delegate void CardSelectedHandler(CardDisplay card);

    public static event CardSelectedHandler OnCardSelect;

    void Start()
    {
        if (card is UnitCard unitCard)
        {
            powerImage.sprite = Resources.Load<Sprite>(unitCard.power.ToString());
            unitTypeImage.sprite = Resources.Load<Sprite>(unitCard.type.ToString().ToLower());
            kindBorderImage.sprite = Resources.Load<Sprite>("card-border-" + unitCard.kind.ToString().ToLower());
            kindBannerImage.sprite = Resources.Load<Sprite>(unitCard.kind.ToString().ToLower());
            originalScale = transform.localScale;
        }
    }

    void OnMouseDown()
    {
        OnCardSelect?.Invoke(this);
    }

    void OnMouseEnter()
    {
        // transform.localScale = originalScale * scaleFactor;
        // StartCoroutine(SmoothScale(scaleFactor));
    }

    void OnMouseExit()
    {
        // StartCoroutine(SmoothScale(1f));
    }

    IEnumerator SmoothScale(float targetScaleFactor)
    {
        float elapsedTime = 0;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / transitionDuration;
            transform.localScale = Vector3.Lerp(originalScale, originalScale * targetScaleFactor, progress);

            yield return null;
        }

        // Ensure the final values are set correctly
        transform.localScale = originalScale * targetScaleFactor;
    }
}
