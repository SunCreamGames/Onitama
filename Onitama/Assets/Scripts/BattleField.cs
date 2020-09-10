using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleField : MonoBehaviour
{
    [SerializeField]
    Button restartButton;
    public Cell[,] Cells;
    [SerializeField]
    Sprite s1;
    [SerializeField]
    Cell cell;
    private void Awake()
    {

        restartButton.enabled = false;
        restartButton.gameObject.SetActive(false);

        Cells = new Cell[5, 5];

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Cell newCell = Instantiate(cell, transform);
                Cells[i, j] = newCell;
                newCell.transform.position = new Vector3(-4 + 2 * j, -4 + 2 * i, 0);
                if (i == 0 && j == 2)
                {
                    newCell.GetComponent<Cell>().team = 1;
                }
                else if (i == 4 && j == 2)
                {
                    newCell.GetComponent<Cell>().team = -1;
                }
                else
                {
                    newCell.GetComponent<Cell>().team = 0;
                }
                newCell.GetComponent<Cell>().SetCoordinates(i, j);
                newCell.gameObject.layer = 10;
                newCell.OnWin += NewCell_OnWin;
            }
        }
    }

    private void NewCell_OnWin(int team)
    {
        restartButton.enabled = true;
        restartButton.gameObject.SetActive(true);
        restartButton.transform.GetChild(0).GetComponent<Text>().text = (team == 1) ? "RED WON\nClick to restart" : "BLUE WON\nClick to restart";
    }
}
