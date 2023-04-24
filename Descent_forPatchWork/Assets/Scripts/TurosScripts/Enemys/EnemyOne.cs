using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyOne : MonoBehaviour
{
    public EnemyBase eB;
    TMP_Text enmName, enmHealth, enmStrength, enmLevel;
    [SerializeField] GameObject currentObject;

    private void Awake()
    {
        GetComponent<Image>().sprite = eB.enemyImages[0];
        enmName = GetComponentInChildren<TMP_Text>();
        enmName.text = eB.EnemyName;

        currentObject = this.gameObject;

        GameObject enemyStats = currentObject.transform.Find("EnemyStatsPanel").gameObject;
        enemyStats.transform.GetChild(0).GetComponent<TMP_Text>().text = eB.enemyHealth.ToString();
        enemyStats.transform.GetChild(1).GetComponent<TMP_Text>().text = eB.enemyStrength.ToString();
        //enemyStats.transform.GetChild(2).GetComponent<TMP_Text>().text = eB.enemyLevel.ToString();
    }
    private void Update()
    {
        GameObject enemyStats = currentObject.transform.Find("EnemyStatsPanel").gameObject;
        enemyStats.transform.GetChild(0).GetComponent<TMP_Text>().text = eB.enemyHealth.ToString();
        enemyStats.transform.GetChild(1).GetComponent<TMP_Text>().text = eB.enemyStrength.ToString();
        //enemyStats.transform.GetChild(2).GetComponent<TMP_Text>().text = eB.enemyLevel.ToString();

        if(eB.enemyType == 2)
        {
            GameObject enemyPanel = GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(10).gameObject;
            enemyPanel.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = eB.enemyHealth.ToString();
            enemyPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = eB.enemyShield.ToString();
            enemyPanel.transform.GetChild(3).GetComponent<TMP_Text>().text = eB.enemyStrength.ToString();
            enemyPanel.transform.GetChild(4).GetComponent<TMP_Text>().text = eB.enemyMove.ToString();
        }

        if (eB.enemyHealth <= 0)
        {
            if(eB.enemyType == 1)
            {
                GameManager.gm.enemysInGame.Remove(this.gameObject);
                GameManager.gm.enemyHordHealth -= this.eB.enemyHealth;
                GameManager.gm.enemyHordStrenght -= this.eB.enemyStrength;
                Destroy(this.gameObject);
            }

            if(eB.enemyType == 2)
            {
                GameManager.gm.enemyBossesInGame.Remove(this.gameObject);
                Destroy(this.gameObject);
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && eB.enemyType == 1)
        {
            this.gameObject.transform.SetParent(GameObject.Find("TempHolder").transform);

            Debug.Log("SISÄLLÄ ENEMYJEN MERGEEMISESSÄ");

            GameManager.gm.mergeHorde = true;
            
        }
    }
}
