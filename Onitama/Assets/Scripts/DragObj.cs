using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragObj : MonoBehaviour
{
    [SerializeField]
    int team;
    string cardName;
    [SerializeField]
    LayerMask layerMask;
    
    public Card card; // half-private
    Vector3 pos, startPos, delta;
    bool isDragging;
    public void WasUsed(Vector3 pos)
    {
        transform.position = pos;
        GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY;
        team *= -1;
    }
    public void SetCard(Card c)
    {
        card = c;
        try
        {
            GetComponent<SpriteRenderer>().sprite = c.sprite;
        }
        finally
        {

        }
    }
    private void Start()
    {
        Reset();
    }
    public void Reset()
    {
       startPos = transform.position;
       isDragging = false;
    }



    public void OnMouseDown()
    {
        pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        isDragging = true;
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0.5f);
    }
    private void Update()
    {
        if (isDragging)
        {
            delta = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - pos;
            transform.position += delta;
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }
    }

    IEnumerator CardMoveBack()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log(startPos.x + "  " + startPos.y + "  " + startPos.z);
        transform.position = startPos;
    }
    public void OnMouseUp()
    {

        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1f);
        StartCoroutine(CardMoveBack());
        isDragging = false;
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + transform.forward , transform.forward, 3f, layerMask);
        if (hit2.collider != null)
        {
            if (hit2.collider.gameObject.GetComponent<Cell>()!=null)
            {
                if (hit2.collider.gameObject.GetComponent<Cell>().figure != null)
                {
                    if (team == hit2.collider.gameObject.GetComponent<Cell>().figure.team)
                    {
                        hit2.collider.gameObject.GetComponent<Cell>().figure.Select(this);
                    }
                }
            }
        }

        transform.localScale = new Vector3(1f,1f,1f);
    }
}
