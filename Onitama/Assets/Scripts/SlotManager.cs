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
    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            int o = Random.Range(0, cards.Count);
            slots[i].SetCard(cards[o]);
            cards.RemoveAt(o);
            GetComponent<TurnManager>().OnTurnEnd += SlotManager_OnTurnEnd;
        }
    }

    private void SlotManager_OnTurnEnd(int t, Card c)
    {
    
        foreach (DragObj item in slots)
        {
            if(item.card.name == c.name)
            {
                Vector3 tempEmptySpace = item.transform.position;
                item.WasUsed(emptySpace);
                emptySpace = tempEmptySpace;
                Debug.Log($"  {item.card.name}  is now here : {item.transform.position.x} , {item.transform.position.y}");
                return;
            }
        }
    
    }
}
