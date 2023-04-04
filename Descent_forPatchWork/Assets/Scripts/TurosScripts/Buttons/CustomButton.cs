using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent buttonClick;

    private void Awake()
    {
        if(buttonClick == null)
        {
            buttonClick = new UnityEvent();
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("BUTTON DOWN!");

        if (this.gameObject.CompareTag("WorldMapButton"))
        {
            GetComponent<BlockInformation>().setAreaImageInfo();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("BUTTON UP!");
    }
}
