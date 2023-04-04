using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HeroButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
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

        
 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("BUTTON UP!");

        if (this.gameObject.CompareTag("Hero") && GameManager.gm.enemyCanAttack == false)
        {
            GameObject statsPanel = this.gameObject.transform.Find("HeroStatsPanel").gameObject;

            if (statsPanel.activeSelf == false)
            {
                statsPanel.SetActive(true);
                GameManager.gm.plrCanAttack = true;
                GameManager.gm.attackForce = this.gameObject.GetComponent<HeroOne>().hb.plrStrength;
            }
            else
            {
                statsPanel.SetActive(false);
                GameManager.gm.plrCanAttack = false;
                GameManager.gm.attackForce = 0;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //this.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //this.gameObject.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
    }
}