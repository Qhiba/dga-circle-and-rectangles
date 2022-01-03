using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapArea : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 pointerPos = Camera.main.ScreenToWorldPoint(new Vector2(eventData.position.x, eventData.position.y));
        GameManager.Instance.MoveToPoint(pointerPos);
    }
}
