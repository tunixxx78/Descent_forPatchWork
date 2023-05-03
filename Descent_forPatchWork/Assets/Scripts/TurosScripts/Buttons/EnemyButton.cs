using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent buttonClick;
    [SerializeField] Sprite[] enemyImages;

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

        SFXHolder.sH.button.Play();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("BUTTON UP!");

        if (GetComponent<EnemyOne>().eB.thisEnemyIsAttacking && GameManager.gm.enemyIsAttacking || GetComponent<EnemyOne>().eB.thisEnemyIsAttacking == false && GameManager.gm.enemyIsAttacking == false && GameManager.gm.villagerSelected == false)
            {
                if (this.gameObject.CompareTag("Enemy") && GameManager.gm.plrCanAttack == false)
                {
                    if (this.gameObject.GetComponent<EnemyOne>().eB.enemyType == 1)
                    {
                        GameObject statsPanel = this.gameObject.transform.Find("EnemyStatsPanel").gameObject;

                        if (statsPanel.activeSelf == false)
                        {
                            statsPanel.SetActive(true);
                            GameManager.gm.enemyCanAttack = true;
                            GameManager.gm.attackForce = this.gameObject.GetComponent<EnemyOne>().eB.enemyStrength;

                            GetComponent<EnemyOne>().eB.thisEnemyIsAttacking = true;
                            GameManager.gm.enemyIsAttacking = true;
                            GameManager.gm.activeEnemy = 1;
                            this.gameObject.GetComponent<Image>().sprite = enemyImages[1];
                        
                    }
                        else
                        {
                            statsPanel.SetActive(false);
                            GameManager.gm.enemyCanAttack = false;
                            GameManager.gm.attackForce = 0;

                            GetComponent<EnemyOne>().eB.thisEnemyIsAttacking = false;
                            GameManager.gm.enemyIsAttacking = false;
                            GameManager.gm.activeEnemy = 0;
                            this.gameObject.GetComponent<Image>().sprite = enemyImages[0];
                        
                    }
                    }
                    if (this.gameObject.GetComponent<EnemyOne>().eB.enemyType == 2)
                    {
                        //GameObject enemyPanel = GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(10).gameObject;
                        GameObject statsPanel = this.gameObject.transform.Find("EnemyStatsPanel").gameObject;

                        if (statsPanel.activeSelf == false)
                        {
                            //enemyPanel.SetActive(true);
                            statsPanel.SetActive(true);
                            GameManager.gm.enemyCanAttack = true;
                            GameManager.gm.attackForce = this.gameObject.GetComponent<EnemyOne>().eB.enemyStrength;

                            GetComponent<EnemyOne>().eB.thisEnemyIsAttacking = true;
                            GameManager.gm.enemyIsAttacking = true;
                            GameManager.gm.activeEnemy = 2;
                            this.gameObject.GetComponent<Image>().sprite = enemyImages[1];
                        
                    }   
                        else
                        {
                            //enemyPanel.SetActive(false);
                            statsPanel.SetActive(false);
                            GameManager.gm.enemyCanAttack = false;
                            GameManager.gm.attackForce = 0;

                            GetComponent<EnemyOne>().eB.thisEnemyIsAttacking = false;
                            GameManager.gm.enemyIsAttacking = false;
                            GameManager.gm.activeEnemy = 0;
                            this.gameObject.GetComponent<Image>().sprite = enemyImages[0];
                        
                    }

                    }

                if (this.gameObject.GetComponent<EnemyOne>().eB.enemyType == 3)
                {
                    //GameObject enemyPanel = GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(10).gameObject;
                    GameObject statsPanel = this.gameObject.transform.Find("EnemyStatsPanel").gameObject;

                    if (statsPanel.activeSelf == false)
                    {
                        //enemyPanel.SetActive(true);
                        statsPanel.SetActive(true);
                        GameManager.gm.enemyCanAttack = true;
                        GameManager.gm.attackForce = this.gameObject.GetComponent<EnemyOne>().eB.enemyStrength;

                        GetComponent<EnemyOne>().eB.thisEnemyIsAttacking = true;
                        GameManager.gm.enemyIsAttacking = true;
                        GameManager.gm.activeEnemy = 2;
                        this.gameObject.GetComponent<Image>().sprite = enemyImages[1];
                        
                    }
                    else
                    {
                        //enemyPanel.SetActive(false);
                        statsPanel.SetActive(false);
                        GameManager.gm.enemyCanAttack = false;
                        GameManager.gm.attackForce = 0;

                        GetComponent<EnemyOne>().eB.thisEnemyIsAttacking = false;
                        GameManager.gm.enemyIsAttacking = false;
                        GameManager.gm.activeEnemy = 0;
                        this.gameObject.GetComponent<Image>().sprite = enemyImages[0];
                        
                    }

                }

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
