using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Unit Card")]
public class UnitCard : Card
{
    public UnitType type;
    public int originalPower;
    public int power;
    public string effect;

    public override void Reset()
    {
        base.Reset();
        power = originalPower;
    }
}