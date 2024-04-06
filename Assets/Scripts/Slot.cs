using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private GameObject _hover;
    [SerializeField] protected GameObject _highlight;
    public virtual void PlayCard(Card card) { }

    public delegate void SlotSelectedHandler(Slot slot);
    public static event SlotSelectedHandler OnSlotSelected;

    public void OnMouseDown()
    {
        if (_highlight)
        {
            OnSlotSelected?.Invoke(this);
        }
    }

    public void Highlight()
    {
        _highlight.SetActive(true);
    }

    public void Unhighlight()
    {
        _highlight.SetActive(false);
    }

    void OnMouseEnter()
    {
        _hover.SetActive(true);
    }

    void OnMouseExit()
    {
        _hover.SetActive(false);
    }
}
