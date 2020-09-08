using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragObj : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler, IEndDragHandler
{
    string cardName;
    public Card card;
    Vector2 pos;
    Vector3 delta;
    private void Start()
    {
       GetComponent<Image>().sprite = card.sprite;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        pos = eventData.position;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 0)), out hit))
        {
            Debug.Log("AAAAAA");
            if (hit.collider.CompareTag("Figure"))
            {
                if (card.team == hit.collider.gameObject.GetComponent<Figure>().team)
                {
                    hit.collider.gameObject.GetComponent<Figure>().Select(card);
                }
            }
        }
        transform.localScale = new Vector3(1f,1f,1f);

    }
}
