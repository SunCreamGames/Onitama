using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Figure : MonoBehaviour
{
    public bool IsMaster;
    public int team;
    public delegate void Selection(DragObj card);
    public event Selection OnSelected;

    public void Select(DragObj card)
    {
        OnSelected(card);
    }
}
    