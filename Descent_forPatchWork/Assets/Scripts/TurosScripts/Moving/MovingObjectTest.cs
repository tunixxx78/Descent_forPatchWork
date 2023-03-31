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


    /*
    Camera main;
    float cameraDistance;

    private void Start()
    {
        main = Camera.main;
        cameraDistance = main.WorldToScreenPoint(transform.position).z;
    }

    private void OnMouseDrag()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance);
        Vector3 newPosition = main.ScreenToWorldPoint(screenPosition);

        transform.position = newPosition;
    }
    */
}
