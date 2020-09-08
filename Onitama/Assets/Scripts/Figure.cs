using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Figure : MonoBehaviour
{
    public bool IsMaster { private set; get; }
    public int team { private set; get; }
    public delegate void Selection(Card card);
    public event Selection OnSelected;
    public Figure(bool isMaster, int team)
    {
        IsMaster = isMaster;
        this.team = team;
    }

    public void Select(Card card)
    {
        OnSelected(card);
    }
}
    