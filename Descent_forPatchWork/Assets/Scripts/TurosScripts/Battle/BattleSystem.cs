using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleSystem : MonoBehaviour
{
    public void OnPointerDown(BaseEventData eventData)
    {
        PointerEventData pointerData = (PointerEventData)eventData;

        if (gameObject.CompareTag("Enemy") && GameManager.gm.canAttack == true)
        {
            Debug.Log("HIIRTÄ PAINETTU");
            this.gameObject.GetComponent<EnemyOne>().eB.enemyHealth -= GameManager.gm.attackForce;
        }
    }

}