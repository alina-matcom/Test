using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Field Card")]
public class FieldCard : SpecialCard
{
  public UnitType affectedType;
  public int powerReduction;
}