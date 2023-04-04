using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHordPanelController : MonoBehaviour
{
    TMP_Text hordHealthAmount, hordStrenghtAmount;

    private void Awake()
    {
        hordHealthAmount = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        hordStrenghtAmount = this.gameObject.transform.GetChild(1).GetChild(0).GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        hordHealthAmount.text = GameManager.gm.enemyHordHealth.ToString();
        hordStrenghtAmount.text = GameManager.gm.enemyHordStrenght.ToString();
    }
    private void Update()
    {
        hordHealthAmount.text = GameManager.gm.enemyHordHealth.ToString();
        hordStrenghtAmount.text = GameManager.gm.enemyHordStrenght.ToString();
    }
}
