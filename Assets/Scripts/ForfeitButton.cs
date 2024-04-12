using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForfeitButton : MonoBehaviour
{
    public void OnMouseDown()
    {
        GameController.Instance.Forfeit();
    }
}
