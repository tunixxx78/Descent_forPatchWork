using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingObjectTest : MonoBehaviour
{

    Canvas mainCanvas;

    private void Awake()
    {
        GameObject tempCanvas = GameObject.Find("Canvas");
        mainCanvas = tempCanvas.GetComponent<Canvas>();
    }

    public void OnDrag(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)mainCanvas.transform, pointerData.position, mainCanvas.worldCamera, out pos);

        transform.position = mainCanvas.transform.TransformPoint(pos);
    }
}
