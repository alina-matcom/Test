using UnityEngine;

public class Card : ScriptableObject
{
    public new string name;
    public string description;
    public CardKind kind;
    public string Image;
    public string Faction;
    public int owner;
    public virtual void Reset() { }
}
