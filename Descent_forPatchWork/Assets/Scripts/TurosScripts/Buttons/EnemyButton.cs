using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
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

        if (this.gameObject.CompareTag("Enemy") && GameManager.gm.plrCanAttack == false)
        {
            GameObject statsPanel = this.gameObject.transform.Find("EnemyStatsPanel").gameObject;

            if (statsPanel.activeSelf == false)
            {
                statsPanel.SetActive(true);
                GameManager.gm.enemyCanAttack = true;
                GameManager.gm.attackForce = this.gameObject.GetComponent<EnemyOne>().eB.enemyStrength;
            }
            else
            {
                statsPanel.SetActive(false);
                GameManager.gm.enemyCanAttack = false;
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
        //this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}