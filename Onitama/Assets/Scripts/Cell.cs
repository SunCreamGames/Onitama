using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    int x, y;
    public int team;
    public Figure figure;// private set
    public bool isActive;

    public event WinDelegate OnWin;
    public delegate void WinDelegate();
    public void SetCoordinates(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
    public void SetFigure(Figure f)
    {
        if(f.team == -1 * team)
        {
            OnWin();
        }
        if (figure == null)
        {
            figure = f;
            figure.gameObject.transform.position = transform.position;
            figure.GetComponent<Figure>().OnSelected += Cell_OnSelected;
        }
        else
        {
            if (figure.IsMaster) {
                OnWin();
            }
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
    public delegate void InfoForTurnManager(DragObj card, int x, int y, int t);
    public event InfoForTurnManager OnSelected;
    public void Cell_OnSelected(DragObj card)
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/fieldHighlighted");
        OnSelected(card, x, y, figure.team);
    }
}

