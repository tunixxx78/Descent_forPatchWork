using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleSystem : MonoBehaviour
{
    public void OnPointerDown(BaseEventData eventData)
    {
        PointerEventData pointerData = (PointerEventData)eventData;

        if (gameObject.CompareTag("Enemy") && GameManager.gm.plrCanAttack == true)
        {
            Debug.Log("HIIRTÄ PAINETTU");
            this.gameObject.GetComponent<EnemyOne>().eB.enemyHealth -= GameManager.gm.attackForce;
            if(this.gameObject.GetComponent<EnemyOne>().eB.enemyType == 1)
            {
                GameManager.gm.enemyHordHealth -= GameManager.gm.attackForce;
            }
            
        }
        if(gameObject.CompareTag("Hero") && GameManager.gm.enemyCanAttack == true)
        {
            Debug.Log("HIIRTÄ PAINETTU");
            this.gameObject.GetComponent<HeroOne>().hb.plrHealth -= GameManager.gm.attackForce;

        }
    }

}