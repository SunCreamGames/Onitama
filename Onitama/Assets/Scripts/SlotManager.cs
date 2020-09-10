using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField]
    DragObj[] slots;
    [SerializeField]
    List<Card> cards;
    [SerializeField]
    Vector3 emptySpace;
    List<DragObj> redTeam, blueTeam;
    void Start()
    {
        redTeam = new List<DragObj>();
        blueTeam = new List<DragObj>();
        for (int i = 0; i < slots.Length; i++)
        {
            int o = Random.Range(0, cards.Count);
            slots[i].SetCard(cards[o]);
            cards.RemoveAt(o);
        }
        GetComponent<TurnManager>().OnTurnEnd += SlotManager_OnTurnEnd;
        GetComponent<TurnManager>().OnWin += SlotManager_OnWin1;
       redTeam.Add(slots[0]);
        redTeam.Add(slots[1]);
        redTeam.Add(slots[2]);
        blueTeam.Add(slots[3]);
        blueTeam.Add(slots[4]);

        slots[2].GetComponent<BoxCollider2D>().enabled = false;
        slots[2].GetComponent<SpriteRenderer>().color = Color.gray;
        slots[3].GetComponent<BoxCollider2D>().enabled = false;
        slots[3].GetComponent<SpriteRenderer>().color = Color.gray;
        slots[4].GetComponent<BoxCollider2D>().enabled = false;
        slots[4].GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void SlotManager_OnWin1()
    {
        foreach (var slot in slots)
        {
            slot.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void SlotManager_OnWin(int teamWinner)
    {
        foreach (DragObj dragObj in slots)
        {
            dragObj.GetComponent<DragObj>().enabled = false;
        }
    }

    private void SlotManager_OnTurnEnd(int t, Card c, DragObj currentSlot)
    {
        Vector3 tempEmptySpace = currentSlot.transform.position;
        currentSlot.WasUsed(emptySpace);
        emptySpace = tempEmptySpace;
        if(t == 1)
        {
            foreach (var slot in redTeam)
            {
                slot.GetComponent<BoxCollider2D>().enabled = false;
                slot.GetComponent<SpriteRenderer>().color = Color.gray;
                //slot.gameObject.SetActive(false);
            }
            foreach (var slot in blueTeam)
            {
                slot.GetComponent<BoxCollider2D>().enabled = true;
                slot.GetComponent<SpriteRenderer>().color = Color.white;
                //slot.gameObject.SetActive(true);
            }
            redTeam.Remove(currentSlot);
            blueTeam.Add(currentSlot);
        }
        else
        {
            foreach (var slot in redTeam)
            {
                slot.GetComponent<BoxCollider2D>().enabled = true;
                slot.GetComponent<SpriteRenderer>().color = Color.white;
                //   slot.gameObject.SetActive(true);
            }
            foreach (var slot in blueTeam)
            {
                slot.GetComponent<BoxCollider2D>().enabled = false;
                //   slot.gameObject.SetActive(false);
                slot.GetComponent<SpriteRenderer>().color = Color.gray;
            }
            redTeam.Add(currentSlot);
            blueTeam.Remove(currentSlot);
        }


    }
}
