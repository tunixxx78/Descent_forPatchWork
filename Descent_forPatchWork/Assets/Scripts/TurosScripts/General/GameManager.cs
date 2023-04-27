using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<Sprite> BGImages;
    public List<GameObject> heroesInGame;
    public List<GameObject> enemysInGame;
    public List<GameObject> enemyBossesInGame;
    public float enemyHordHealth, enemyHordStrenght;

    public bool plrCanAttack, enemyCanAttack, battleIsOn, plrIsAttacking, enemyIsAttacking, villagerSelected, bossIsSpawned, mergeHorde, lootIsOn;
    public float attackForce = 1;

    public GameObject QuestLorePanel, currentMission;
    public int currentAreaMissions, currentMissionInQuest = 0, currentMissionIndex = 0, activePlayer, round = 0, activeEnemy;

    public Transform[] lootSpawnPoints;
    public List<GameObject> lootObjects;      //GameObject[] lootObjects;
    public List<Sprite> rewardImages;


    public List<GameObject> missionsInAreaMap;
    public List<GameObject> enemySpawnersIngame;

    public GameObject InventorySlotBase;

    public GameObject hordePanel;
    public GameObject enemyPanel;
    public GameObject BossInfoPanel;

    [SerializeField] GameObject characterHolder;

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
        lootIsOn = false;

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
        //enemyHordStrenght = enemyHordStrenght / 5;

        //Debug.Log(enemyHordStrenght);

        if(enemysInGame.Count <= 0 && battleIsOn && bossIsSpawned == false)
        {
            characterHolder = GameObject.Find("CharacterHolder");
            characterHolder.SetActive(false);

            BossInfoPanel.SetActive(true);

            GameObject bossInstance = Instantiate(maps.enemyBase[2], maps.bossEnemySpawnPoint.position, Quaternion.identity);
            bossInstance.transform.SetParent(maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(16));
            GameManager.gm.enemyBossesInGame.Add(bossInstance);
            maps.enemyTwoPanel.SetActive(true);
            bossIsSpawned = true;
            
        }
        if(enemysInGame.Count <= 0 && enemyBossesInGame.Count <= 0 && battleIsOn)
        {
            Debug.Log("PELAAJA ON VOITTANUT TAISTELUN!");
            battleIsOn = false;
            bossIsSpawned = false;
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

            for (int s = 0; s < enemySpawnersIngame.Count; s++)
            {
                enemySpawnersIngame[s].gameObject.SetActive(false);
            }

            // for instanciating lootObjects to map

            for (int i = 0; i < lootSpawnPoints.Length; i++)
            {
                GameObject lootInstance = Instantiate(lootObjects[i], lootSpawnPoints[i].position, Quaternion.identity);

                lootInstance.transform.SetParent(GameObject.Find("LootSpotHolder").transform);
                lootInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            lootIsOn = true;
        }

        if(lootIsOn && GameObject.Find("LootSpotHolder").transform.childCount <= 0)
        {
            lootIsOn = false;
            characterHolder.SetActive(false);
            GameObject.Find("BattleResultPanel").transform.GetChild(6).gameObject.SetActive(true);
            GameObject.Find("BattleResultPanel").transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = rewardImages[round];
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
        enemyCanAttack = false;
        enemyIsAttacking = false;

        for(int i = 0; i < enemysInGame.Count; i++)
        {
            enemysInGame[i].transform.GetChild(1).gameObject.SetActive(false);
        }

        Debug.Log("GAMEMANAGER MERGE FUNCTIO SAAVUTETTU");

        GameObject temp = GameObject.Find("TempHolder");
        temp.transform.GetChild(0).GetComponent<EnemyOne>().eB.enemyHealth = temp.transform.GetChild(0).GetComponent<EnemyOne>().eB.enemyHealth + temp.transform.GetChild(1).GetComponent<EnemyOne>().eB.enemyHealth;
        temp.transform.GetChild(0).GetComponent<Image>().sprite = temp.transform.GetChild(0).GetComponent<EnemyOne>().eB.enemyImages[1];

        enemysInGame.Remove(temp.transform.GetChild(1).gameObject);
        Destroy(temp.transform.GetChild(1).gameObject);

        temp.transform.GetChild(0).gameObject.transform.SetParent(maps.gameObject.transform.GetChild(currentMissionIndex).GetChild(2));
    }

    public void SetupForNextRound()
    {
        characterHolder.SetActive(true);

        for (int s = 0; s < enemySpawnersIngame.Count; s++)
        {
            enemySpawnersIngame[s].gameObject.SetActive(true);
        }

        GameObject.Find("BattleResultPanel").transform.GetChild(6).GetChild(0).gameObject.SetActive(true);
        GameObject.Find("BattleResultPanel").transform.GetChild(6).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("BattleResultPanel").transform.GetChild(6).gameObject.SetActive(false);


    }
}
