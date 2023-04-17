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
            SFXHolder.sH.sword.Play();

            Debug.Log("HIIRTÄ PAINETTU");
            this.gameObject.GetComponent<EnemyOne>().eB.enemyHealth -= GameManager.gm.attackForce;
            if(this.gameObject.GetComponent<EnemyOne>().eB.enemyType == 1)
            {
                if((GameManager.gm.attackForce - this.gameObject.GetComponent<EnemyOne>().eB.enemyShield) >= this.gameObject.GetComponent<EnemyOne>().eB.enemyShield)
                {
                    GameManager.gm.enemyHordHealth -= (GameManager.gm.attackForce - this.gameObject.GetComponent<EnemyOne>().eB.enemyShield);
                }
                
            }
            
        }
        if(gameObject.CompareTag("Hero") && GameManager.gm.enemyCanAttack == true)
        {
            Debug.Log("HIIRTÄ PAINETTU");
            if ((GameManager.gm.attackForce - this.gameObject.GetComponent<HeroOne>().hb.plrShield) >= this.gameObject.GetComponent<HeroOne>().hb.plrShield)
            {
                if(GameManager.gm.activeEnemy == 1)
                {
                    SFXHolder.sH.arrow.Play();
                }
                if(GameManager.gm.activeEnemy == 2)
                {
                    SFXHolder.sH.sword.Play();
                }

                this.gameObject.GetComponent<HeroOne>().hb.plrHealth -= (GameManager.gm.attackForce - this.gameObject.GetComponent<HeroOne>().hb.plrShield);
            }
            

        }
    }

}