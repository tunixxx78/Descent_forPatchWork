using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] float tempHealth = 3;

    public void OnPointerDown(BaseEventData eventData)
    {
        PointerEventData pointerData = (PointerEventData)eventData;

        if (gameObject.CompareTag("Enemy") && GameManager.gm.plrCanAttack == true)
        {
            SFXHolder.sH.sword.Play();

            Debug.Log("HIIRTÄ PAINETTU");
            if (this.gameObject.GetComponent<EnemyOne>().eB.enemyHealth - GameManager.gm.attackForce >= 0)
            {
                this.gameObject.GetComponent<EnemyOne>().eB.enemyHealth -= GameManager.gm.attackForce;
                this.gameObject.GetComponent<EnemyOne>().SetEnemyStrength();

                //for bossFight

                GameManager.gm.tempBossHealth -= (int)GameManager.gm.attackForce;

                if (this.gameObject.GetComponent<EnemyOne>().eB.enemyType == 1)
                {
                    GameManager.gm.enemyHordHealth -= (GameManager.gm.attackForce - this.gameObject.GetComponent<EnemyOne>().eB.enemyShield);
                }
            }
            else if(this.gameObject.GetComponent<EnemyOne>().eB.enemyHealth - GameManager.gm.attackForce < 0)
            {
                tempHealth = GameManager.gm.attackForce - (GameManager.gm.attackForce - this.gameObject.GetComponent<EnemyOne>().eB.enemyHealth);
                this.gameObject.GetComponent<EnemyOne>().eB.enemyHealth = 0;

                if (this.gameObject.GetComponent<EnemyOne>().eB.enemyType == 1)
                {
                    GameManager.gm.enemyHordHealth -= tempHealth;
                }
            }

            /*
            if(this.gameObject.GetComponent<EnemyOne>().eB.enemyType == 1)
            {
                //if((GameManager.gm.attackForce - this.gameObject.GetComponent<EnemyOne>().eB.enemyShield) >= this.gameObject.GetComponent<EnemyOne>().eB.enemyShield)
                //{
                    if(this.gameObject.GetComponent<EnemyOne>().eB.enemyHealth - GameManager.gm.attackForce >= 0)
                    {
                        GameManager.gm.enemyHordHealth -= (GameManager.gm.attackForce - this.gameObject.GetComponent<EnemyOne>().eB.enemyShield);
                    }
                    else if (this.gameObject.GetComponent<EnemyOne>().eB.enemyHealth - GameManager.gm.attackForce < 0)
                    {
                        GameManager.gm.enemyHordHealth -= tempHealth;
                        //GameManager.gm.enemyHordHealth -= (GameManager.gm.attackForce - this.gameObject.GetComponent<EnemyOne>().eB.enemyShield);
                    }

                    
                //}
                
            }
            */
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
                if (GameManager.gm.activeEnemy == 3)
                {
                    SFXHolder.sH.sword.Play();
                }

                this.gameObject.GetComponent<HeroOne>().hb.plrHealth -= (GameManager.gm.attackForce - this.gameObject.GetComponent<HeroOne>().hb.plrShield);
            }
            

        }
    }

}