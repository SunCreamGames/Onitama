using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    Figure[] figures;
    [SerializeField]
    BattleField field;
    Card card;
    DragObj dragObj;
    Figure figure;
    [SerializeField]
    LayerMask lMask;
    int teamTurn;
    public delegate void MyDelegate(int t, Card c, DragObj dragObj);
    public event MyDelegate OnTurnEnd;

    public event WinDelegate OnWin;
    public delegate void WinDelegate();
    private void Start()
    {
        teamTurn = 1;
        for (int i = 0; i < field.Cells.GetLength(0); i++)
        {
            for (int j = 0; j < field.Cells.GetLength(1); j++)
            {
                if(i==0)
                {
                    field.Cells[i, j].SetFigure(figures[j]);
                }
                else if (i == 4)
                {
                    field.Cells[i, j].SetFigure(figures[j + 5]);
                }
                field.Cells[i, j].OnSelected += TurnManager_OnSelected;
                field.Cells[i, j].OnWin += TurnManager_OnWin;
            }
        }
    }
    private void TurnManager_OnWin(int t)
    {
        for (int i = 0; i < field.Cells.GetLength(0); i++)
        {
            for (int j = 0; j < field.Cells.GetLength(1); j++)
            {
                field.Cells[i, j].gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        OnWin();

    }

    private void TurnManager_OnSelected(DragObj card, int x, int y, int team)
    {
        dragObj = card;
        this.card = card.card;
        figure = field.Cells[x, y].figure;
        for (int i = 0; i < field.Cells.GetLength(0); i++)
        {
            for (int j = 0; j < field.Cells.GetLength(1); j++)
            {
                field.Cells[i, j].isActive = false;
                if(!(x==i && y ==j))
                field.Cells[i, j].gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/field");
            }
        }
        foreach (string move in card.card.moves)
        {
            int a = Convert.ToInt32(move.Split(' ')[0]) * team;
            int b = Convert.ToInt32(move.Split(' ')[1]) * team;
            
            if (0 <= x + b && x + b < 5 && y + a >= 0 && y + a < 5)
            {

                if (field.Cells[x + b, y + a].GetFigureTeam() == team)
                {

                }
                else if (field.Cells[x + b, y + a].GetFigureTeam() == -team)
                {
                    field.Cells[x + b, y + a].isActive = true;
                    field.Cells[x + b, y + a].gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/fieldAttack");
                }
                else
                {
                    field.Cells[x + b, y + a].isActive = true;
                    field.Cells[x + b, y + a].gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/fieldActive");
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, lMask);
            if (hit2D.collider != null)
            {
                if (hit2D.collider.GetComponent<Cell>() != null && hit2D.collider.GetComponent<Cell>().isActive)
                {
                    foreach (Cell cell in field.Cells)
                    {
                        if (cell.figure == figure)
                        {
                            cell.figure.OnSelected -= cell.Cell_OnSelected;
                            cell.figure = null;
                        }
                    }
                    hit2D.collider.GetComponent<Cell>().SetFigure(figure);
                    for (int i = 0; i < field.Cells.GetLength(0); i++)
                    {
                        for (int j = 0; j < field.Cells.GetLength(1); j++)
                        {
                            field.Cells[i, j].isActive = false;
                            field.Cells[i, j].gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/field");
                        }
                    }
                    OnTurnEnd(teamTurn, card, dragObj);
                    dragObj.Reset();
                    teamTurn *= -1;
                }
            }
        }
    }
}
