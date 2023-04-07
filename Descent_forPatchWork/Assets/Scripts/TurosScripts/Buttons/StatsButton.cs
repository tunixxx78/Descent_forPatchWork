using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StatsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent buttonClick;
    [SerializeField] bool isNegative = false, isHero, isKillButton;
    [SerializeField] GameObject parentObject;

    private void Awake()
    {
        if(buttonClick == null)
        {
            buttonClick = new UnityEvent();
        }

        if (isHero)
        {
            parentObject = GetComponentInParent<HeroOne>().gameObject;
        }
        else
        {
            parentObject = GetComponentInParent<EnemyOne>().gameObject;
        }
        

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("BUTTON DOWN!");

        if (isHero)
        {
            if (isNegative)
            {
                if (this.gameObject.CompareTag("Health"))
                {
                    parentObject.GetComponent<HeroOne>().hb.plrHealth--;
                }
                if (this.gameObject.CompareTag("Strenght"))
                {
                    parentObject.GetComponent<HeroOne>().hb.plrStrength--;
                    GameManager.gm.attackForce = parentObject.GetComponent<HeroOne>().hb.plrStrength;
                }
                if (this.gameObject.CompareTag("Level"))
                {
                    parentObject.GetComponent<HeroOne>().hb.plrLevel--;
                }

            }
            else
            {
                if (this.gameObject.CompareTag("Health"))
                {
                    parentObject.GetComponent<HeroOne>().hb.plrHealth++;
                }
                if (this.gameObject.CompareTag("Strenght"))
                {
                    parentObject.GetComponent<HeroOne>().hb.plrStrength++;
                    GameManager.gm.attackForce = parentObject.GetComponent<HeroOne>().hb.plrStrength;
                }
                if (this.gameObject.CompareTag("Level"))
                {
                    parentObject.GetComponent<HeroOne>().hb.plrLevel++;
                }
            }
        }
        else
        {
            if (isNegative)
            {
                if (this.gameObject.CompareTag("Health"))
                {
                    parentObject.GetComponent<EnemyOne>().eB.enemyHealth--;
                    GameManager.gm.enemyHordHealth--;
                }
                if (this.gameObject.CompareTag("Strenght"))
                {
                    parentObject.GetComponent<EnemyOne>().eB.enemyStrength--;
                    GameManager.gm.enemyHordStrenght--;

                }

            }
            else
            {
                if (this.gameObject.CompareTag("Health"))
                {
                    parentObject.GetComponent<EnemyOne>().eB.enemyHealth++;
                    GameManager.gm.enemyHordHealth++;
                }
                if (this.gameObject.CompareTag("Strenght"))
                {
                    parentObject.GetComponent<EnemyOne>().eB.enemyStrength++;
                    GameManager.gm.enemyHordStrenght++;
                }

            }

            if (isKillButton)
            {
                GameManager.gm.enemyHordHealth -= GetComponentInParent<EnemyOne>().eB.enemyHealth;
                GameManager.gm.enemyHordStrenght -= GetComponentInParent<EnemyOne>().eB.enemyStrength;

                Destroy(parentObject);
            }
        }
        

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("BUTTON UP!");
    }
}
