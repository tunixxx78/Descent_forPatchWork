using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<GameObject> heroesInGame;
    public List<GameObject> enemysInGame;
    public float enemyHordHealth, enemyHordStrenght;

    public bool plrCanAttack, enemyCanAttack, battleIsOn, plrIsAttacking;
    public float attackForce = 1;

    public GameObject QuestLorePanel, currentMission;
    public int currentAreaMissions, currentMissionInQuest = 0, currentMissionIndex = 0, activePlayer, round = 0;

    public Transform[] lootSpawnPoints;
    public GameObject[] lootObjects;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);

        if(GameManager.gm == null)
        {
            GameManager.gm = this;
        }
        else
        {
            if(GameManager.gm != this)
            {
                Destroy(GameManager.gm.gameObject);
                GameManager.gm = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);

        plrCanAttack = false;
        enemyCanAttack = false;

        //for filling the QuestLorePanel information

        QuestLorePanel = GameObject.Find("MapPanel").transform.GetChild(currentMissionInQuest).GetChild(3).gameObject;
        QuestLorePanel.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("MapPanel").transform.GetChild(0).GetComponent<Quest>().questImage;
        QuestLorePanel.transform.GetChild(1).GetComponent<TMP_Text>().text = GameObject.Find("MapPanel").transform.GetChild(0).GetComponent<Quest>().questName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(enemysInGame.Count <= 0 && battleIsOn)
        {
            Debug.Log("PELAAJA ON VOITTANUT TAISTELUN!");
            battleIsOn = false;
            currentAreaMissions--;

            //test for showing next mission
            if(currentAreaMissions <= 0)
            {
                currentMissionInQuest++;
                currentMission.GetComponent<Quest>().missions[currentMissionInQuest].SetActive(true);

            }

            GameObject.Find("MapPanel").transform.GetChild(currentMissionIndex).GetChild(2).GetChild(9).gameObject.SetActive(true);

            // for instanciating lootObjects to map

            for(int i = 0; i < lootSpawnPoints.Length; i++)
            {
                GameObject lootInstance = Instantiate(lootObjects[i], lootSpawnPoints[i].position, Quaternion.identity);

                lootInstance.transform.SetParent(GameObject.Find("Canvas").transform);
                lootInstance.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            
        }
        if(heroesInGame.Count <= 0 && battleIsOn)
        {
            Debug.Log("PELAAJA ON HÄVINNYT TAISTELUN!");
            battleIsOn = false;
        }

    }
}
