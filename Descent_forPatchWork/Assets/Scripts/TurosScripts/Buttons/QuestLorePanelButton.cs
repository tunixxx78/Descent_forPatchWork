using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.IO;

public class QuestLorePanelButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

        if (this.gameObject.CompareTag("QuestLore"))
        {
            GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(0).GetChild(1).GetChild(GameManager.gm.currentMissionInQuest).transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
        if (this.gameObject.CompareTag("SideQuest"))
        {
            GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(1).GetChild(1).GetChild(int.Parse(this.gameObject.name)).transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("BUTTON UP!");

        if (this.gameObject.CompareTag("QuestLore"))
        {
            GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(0).GetChild(1).GetChild(GameManager.gm.currentMissionInQuest).transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (this.gameObject.CompareTag("SideQuest"))
        {
            GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(1).GetChild(1).GetChild(int.Parse(this.gameObject.name)).transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
