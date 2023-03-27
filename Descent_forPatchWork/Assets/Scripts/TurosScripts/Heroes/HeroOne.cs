using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroOne : MonoBehaviour
{
    public HeroBase hb;
    TMP_Text heroName, health, strength, level;
    [SerializeField] GameObject currentObj;

    private void Awake()
    {
        GetComponent<Image>().sprite = hb.heroImages[0];
        heroName = GetComponentInChildren<TMP_Text>();
        heroName.text = hb.plrName;

        currentObj = this.gameObject;

        GameObject heroStats = currentObj.transform.Find("HeroStatsPanel").gameObject;
        heroStats.transform.GetChild(0).GetComponent<TMP_Text>().text = hb.plrHealth.ToString();
        heroStats.transform.GetChild(1).GetComponent<TMP_Text>().text = hb.plrStrength.ToString();
        heroStats.transform.GetChild(2).GetComponent<TMP_Text>().text = hb.plrLevel.ToString();
    }
}
