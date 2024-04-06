using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Unit Card")]
public class UnitCard : Card
{
    public UnitType type;
    public int power;
    public string effect;
}
