using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Lider Card")]
public class LiderCard : Card
{
  int charges = 3;

  public virtual void ApplyEffect()
  {
    if (charges > 0) return;

    charges--;
  }

  public override BoardSlot GetBoardSlot()
  {
    return BoardSlot.None;
  }

  public override void Reset() { }
}