using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private GameObject _hoverHighlight;
    [SerializeField] protected GameObject _highlight;

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
        _hoverHighlight.SetActive(true);
    }

    void OnMouseExit()
    {
        _hoverHighlight.SetActive(false);
    }
}
