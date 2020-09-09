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
        redTeam.Add(slots[0]);
        redTeam.Add(slots[1]);
        redTeam.Add(slots[2]);
        blueTeam.Add(slots[3]);
        blueTeam.Add(slots[4]);
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
                slot.GetComponent<DragObj>().enabled = false;
            }
            foreach (var slot in redTeam)
            {
                slot.GetComponent<DragObj>().enabled = true;
            }
            redTeam.Remove(currentSlot);
            blueTeam.Add(currentSlot);
        }
        else
        {
            foreach (var slot in redTeam)
            {
                slot.GetComponent<DragObj>().enabled = true;
            }
            foreach (var slot in redTeam)
            {
                slot.GetComponent<DragObj>().enabled = false;
            }
            redTeam.Remove(currentSlot);
            blueTeam.Add(currentSlot);
            redTeam.Add(currentSlot);
            blueTeam.Remove(currentSlot);
        }


    }
}
