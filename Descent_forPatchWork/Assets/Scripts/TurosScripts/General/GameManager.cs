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
    public List<GameObject> enemyBossesInGame;
    public float enemyHordHealth, enemyHordStrenght;

    public bool plrCanAttack, enemyCanAttack, battleIsOn, plrIsAttacking, enemyIsAttacking, villagerSelected, bossIsSpawned, mergeHorde;
    public float attackForce = 1;

    public GameObject QuestLorePanel, currentMission;
    public int currentAreaMissions, currentMissionInQuest = 0, currentMissionIndex = 0, activePlayer, round = 0, activeEnemy;

    public Transform[] lootSpawnPoints;
    public GameObject[] lootObjects;

    public List<GameObject> missionsInAreaMap;

    public GameObject InventorySlotBase;

    public GameObject hordePanel;
    public GameObject enemyPanel;
    public GameObject BossInfoPanel;
    MapsController maps;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);

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

        maps = FindObjectOfType<MapsController>();
        BossInfoPanel = GameObject.Find("StoryForBossPanel").gameObject;
        BossInfoPanel.SetActive(false);

        plrCanAttack = false;
        enemyCanAttack = false;
        bossIsSpawned = false;
        mergeHorde = false;

        //for filling the QuestLorePanel information

        QuestLorePanel = GameObject.Find("MapPanel").transform.GetChild(currentMissionInQuest).GetChild(3).gameObject;
        QuestLorePanel.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("MapPanel").transform.GetChild(0).GetComponent<Quest>().questImage;
        QuestLorePanel.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
        QuestLorePanel.transform.GetChild(1).GetComponent<TMP_Text>().text = GameObject.Find("MapPanel").transform.GetChild(0).GetComponent<Quest>().questName;

        enemyPanel = GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(10).gameObject;
        hordePanel = GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(6).gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(enemysInGame.Count <= 0 && battleIsOn && bossIsSpawned == false)
        {

            BossInfoPanel.SetActive(true);

            GameObject bossInstance = Instantiate(maps.enemyBase[2], maps.bossEnemySpawnPoint.position, Quaternion.identity);
            bossInstance.transform.SetParent(maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2));
            GameManager.gm.enemyBossesInGame.Add(bossInstance);
            maps.enemyTwoPanel.SetActive(true);
            bossIsSpawned = true;
            
        }
        if(enemysInGame.Count <= 0 && enemyBossesInGame.Count <= 0 && battleIsOn)
        {
            Debug.Log("PELAAJA ON VOITTANUT TAISTELUN!");
            battleIsOn = false;
            currentAreaMissions--;

            //test for showing next mission
            if (currentAreaMissions <= 0)
            {
                currentMissionInQuest++;
                currentMission.GetComponent<Quest>().missions[currentMissionInQuest].SetActive(true);

            }

            GameObject.Find("MapPanel").transform.GetChild(currentMissionIndex).GetChild(2).GetChild(9).gameObject.SetActive(true);
            enemyPanel.SetActive(false);
            hordePanel.SetActive(false);

            // for instanciating lootObjects to map

            for (int i = 0; i < lootSpawnPoints.Length; i++)
            {
                GameObject lootInstance = Instantiate(lootObjects[i], lootSpawnPoints[i].position, Quaternion.identity);

                lootInstance.transform.SetParent(GameObject.Find("Canvas").transform);
                lootInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }

        if(heroesInGame.Count <= 0 && battleIsOn)
        {
            Debug.Log("PELAAJA ON HÄVINNYT TAISTELUN!");
            battleIsOn = false;
        }

        if (mergeHorde)
        {
            MergeEnemyHorde();
        }

    }

    public void MergeEnemyHorde()
    {
        mergeHorde = false;

        Debug.Log("GAMEMANAGER MERGE FUNCTIO SAAVUTETTU");

        GameObject temp = GameObject.Find("TempHolder");
        temp.transform.GetChild(0).GetComponent<EnemyOne>().eB.enemyHealth = temp.transform.GetChild(0).GetComponent<EnemyOne>().eB.enemyHealth + temp.transform.GetChild(1).GetComponent<EnemyOne>().eB.enemyHealth;
        temp.transform.GetChild(0).GetComponent<Image>().sprite = temp.transform.GetChild(0).GetComponent<EnemyOne>().eB.enemyImages[1];

        enemysInGame.Remove(temp.transform.GetChild(1).gameObject);
        Destroy(temp.transform.GetChild(1).gameObject);

        temp.transform.GetChild(0).gameObject.transform.SetParent(maps.gameObject.transform.GetChild(currentMissionIndex).GetChild(2));
    }
}
