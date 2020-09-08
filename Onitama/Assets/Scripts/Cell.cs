﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    int x, y;
    public int team;
    public Figure figure;// private set
    public bool isActive;

    public void SetCoordinates(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
    public void SetFigure(Figure f)
    {
        if (figure == null)
        {
            figure = f;
            figure.gameObject.transform.position = transform.position;
            figure.GetComponent<Figure>().OnSelected += Cell_OnSelected;
        }
        else
        {
            figure.GetComponent<Figure>().OnSelected -= Cell_OnSelected;
            Destroy(figure.gameObject);
            figure = f;
            figure.gameObject.transform.position = transform.position;
            figure.GetComponent<Figure>().OnSelected += Cell_OnSelected;
        }
    }
    public int GetFigureTeam()
    {
        if (figure == null)
            return 0;
        if (figure.team == 1)
        {
            return 1;
        }
        else if(figure.team == -1)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    public delegate void InfoForTurnManager(Card card, int x, int y, int t);
    public event InfoForTurnManager OnSelected;
    public void Cell_OnSelected(Card card)
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/fieldHighlighted");
        OnSelected(card, x, y, figure.team);
    }
}

