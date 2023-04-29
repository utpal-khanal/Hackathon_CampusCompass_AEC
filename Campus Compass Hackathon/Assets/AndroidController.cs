using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AndroidController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public MoveOnPoints move;
    public bool upBotton = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void OnPointerDown(PointerEventData eventData)
    {
        if (upBotton)
        {
            move.upPressed = true;

        }
        else
        {
            move.downPressed = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (upBotton)
        {
            move.upPressed = false;

        }
        else
        {
            move.downPressed = false;
        }
    }
}
