using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameplayButtons : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent buttonClick;
    void Start()
    {
        if (buttonClick == null)
        {
            buttonClick = new UnityEvent();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.gameObject.CompareTag("DiceMenuButton"))
        {
            GameObject DiceMenu = GameObject.Find("DiceMenu").transform.GetChild(0).gameObject;
            DiceMenu.SetActive(!DiceMenu.activeInHierarchy);
        }
    }
}
