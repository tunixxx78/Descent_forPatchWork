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

    private void Update()
    {
        GameObject heroStats = currentObj.transform.Find("HeroStatsPanel").gameObject;
        heroStats.transform.GetChild(0).GetComponent<TMP_Text>().text = hb.plrHealth.ToString();
        heroStats.transform.GetChild(1).GetComponent<TMP_Text>().text = hb.plrStrength.ToString();
        heroStats.transform.GetChild(2).GetComponent<TMP_Text>().text = hb.plrLevel.ToString();

        if(hb.plrHealth <= 0)
        {
            GameManager.gm.heroesInGame.Remove(this.gameObject);
            Destroy(this.gameObject);
        }

        for(int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
        {
            if(this.hb.plrIndex == i)
            {
                GameObject thisPlrStats = GameObject.Find("PlayerPanels");
                thisPlrStats.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = this.hb.heroImages[0];
                thisPlrStats.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = this.hb.plrName;
                thisPlrStats.transform.GetChild(i).GetChild(2).GetChild(2).GetComponent<TMP_Text>().text = this.hb.plrHealth.ToString();
                thisPlrStats.transform.GetChild(i).GetChild(3).GetChild(2).GetComponent<TMP_Text>().text = this.hb.plrStrength.ToString();
                thisPlrStats.transform.GetChild(i).GetChild(4).GetChild(2).GetComponent<TMP_Text>().text = this.hb.plrLevel.ToString();

                thisPlrStats.transform.GetChild(i).gameObject.SetActive(true);
            }   
        }
    }
}