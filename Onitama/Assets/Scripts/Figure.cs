using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Figure : MonoBehaviour
{
    public bool IsMaster;
    public int team;
    public delegate void Selection(Card card);
    public event Selection OnSelected;

    public void Select(Card card)
    {
        OnSelected(card);
    }
}
    