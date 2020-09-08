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
    [SerializeField]
    Sprite targetRed, targetBlue;
    List<GameObject> targets;
    int teamTurn;
    private void Start()
    {
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
            }
        }
    }

    private void TurnManager_OnSelected(Card card, int x, int y)
    {
        for (int i = 0; i < field.Cells.GetLength(0); i++)
        {
            for (int j = 0; j < field.Cells.GetLength(1); j++)
            {
                field.Cells[i, j].isActive = false;
            }
        }
        foreach (int[] move in card.moves)
        {
            if (field.Cells[x + move[0], y + move[1]] != null)
            {
                field.Cells[x + move[0], y + move[1]].isActive = true;
                Vector3 pos = field.Cells[x + move[0], y + move[1]].transform.position;
                Instantiate((teamTurn == 1) ? targetRed : targetBlue, pos, Quaternion.identity, null);
            }
        }
    }
}
