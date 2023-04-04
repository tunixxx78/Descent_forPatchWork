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

        if (eB.enemyHealth <= 0)
        {
            GameManager.gm.enemysInGame.Remove(this.gameObject);
            GameManager.gm.enemyHordHealth -= this.eB.enemyHealth;
            GameManager.gm.enemyHordStrenght -= this.eB.enemyStrength;
            Destroy(this.gameObject);
        }
    }
}
