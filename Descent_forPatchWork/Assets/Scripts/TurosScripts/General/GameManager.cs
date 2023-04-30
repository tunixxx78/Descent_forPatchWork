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

    public bool plrCanAttack, enemyCanAttack, battleIsOn, plrIsAttacking, enemyIsAttacking, villagerSelected, lonerSelected, bossIsSpawned, mergeHorde, lootIsOn, canSpawnEnemys, bossBossFight, bossLoot, bossFightReward;
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

    public GameObject characterHolder;

    MapsController maps;


    public int tempBossHealth = 10000;

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
        canSpawnEnemys = true;
        bossBossFight = false;
        bossLoot = false;
        bossFightReward = false;

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

        if(bossBossFight == false)
        {

            //characterHolder = GameObject.Find("CharacterHolder");

            if (round != 3)
            {
                if (enemysInGame.Count <= 0 && battleIsOn && bossIsSpawned == false)
                {
                    //for setting bossFightTitleIcon

                    GameObject.Find("MapPanel").transform.GetChild(currentMissionIndex).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load($"BossTittleImages/{GameManager.gm.round}");
                    characterHolder = GameObject.Find("CharacterHolder");

                    if (GameManager.gm.canSpawnEnemys)
                    {

                        characterHolder.SetActive(false);

                        //BossInfoPanel.SetActive(true);

                        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load($"MissionInfo/Boss/{GameManager.gm.round}");
                        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).gameObject.SetActive(true);
                        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(1).gameObject.SetActive(true);

                        if (round == 0)
                        {
                            GameObject bossInstance = Instantiate(maps.enemyBase[2], maps.bossEnemySpawnPoint.position, Quaternion.identity);
                            bossInstance.transform.SetParent(maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(16));
                            GameManager.gm.enemyBossesInGame.Add(bossInstance);
                        }
                        if (round == 1)
                        {
                            GameObject bossInstance = Instantiate(maps.enemyBase[5], maps.bossEnemySpawnPoint.position, Quaternion.identity);
                            bossInstance.transform.SetParent(maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(16));
                            GameManager.gm.enemyBossesInGame.Add(bossInstance);
                        }

                        maps.enemyTwoPanel.SetActive(true);
                        maps.enemyTwoPanel.GetComponent<Image>().sprite = maps.bossPanels[round];
                        bossIsSpawned = true;
                    }


                }
                if (enemysInGame.Count <= 0 && enemyBossesInGame.Count <= 0 && battleIsOn)
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

                if (lootIsOn && GameObject.Find("LootSpotHolder").transform.childCount <= 0)
                {
                    lootIsOn = false;
                    characterHolder.SetActive(false);
                    GameObject.Find("BattleResultPanel").transform.GetChild(6).gameObject.SetActive(true);
                    GameObject.Find("BattleResultPanel").transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = rewardImages[round];
                }
            }

            if(round == 3 && bossLoot)
            {
                // for instanciating lootObjects to map

                for (int i = 0; i < lootSpawnPoints.Length; i++)
                {
                    GameObject lootInstance = Instantiate(lootObjects[i], lootSpawnPoints[i].position, Quaternion.identity);

                    lootInstance.transform.SetParent(GameObject.Find("LootSpotHolder").transform);
                    lootInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }

                bossLoot = false;
                lootIsOn = true;

                
            }
            if(round == 3)
            {
                

                if (lootIsOn && GameObject.Find("LootSpotHolder").transform.childCount <= 0)
                {
                    Debug.Log("BOSS LOOT OVER");

                    lootIsOn = false;
                    characterHolder.SetActive(false);

                    maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load($"MissionInfo/Boss/{GameManager.gm.round}");
                    maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).gameObject.SetActive(true);
                    maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(1).gameObject.SetActive(true);

                    GameObject bossInstance = Instantiate(maps.enemyBase[5], maps.bossEnemySpawnPoint.position, Quaternion.identity);
                    bossInstance.transform.SetParent(maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(16));
                    GameManager.gm.enemyBossesInGame.Add(bossInstance);

                    maps.enemyTwoPanel.SetActive(true);
                    maps.enemyTwoPanel.GetComponent<Image>().sprite = maps.bossPanels[round - 1];
                    bossIsSpawned = true;

                    tempBossHealth = (int)bossInstance.GetComponent<EnemyOne>().eB.enemyHealth;

                }

                if(tempBossHealth <= 0)
                {
                    bossFightReward = true;
                }

                // need some other condition!
                if(enemyBossesInGame.Count <= 0 && bossFightReward && battleIsOn)
                {
                    
                    Debug.Log("TULEE LIIKAA KAMAA");
                    maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load($"MissionInfo/End/{GameManager.gm.round}");
                    maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).gameObject.SetActive(true);
                    maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(3).gameObject.SetActive(true);

                    battleIsOn = false;
                    bossFightReward = false;
                    currentAreaMissions--;

                    if (currentAreaMissions <= 0)
                    {
                        currentMissionInQuest++;
                        currentMission.GetComponent<Quest>().missions[currentMissionInQuest].SetActive(true);

                        //maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(3).gameObject.SetActive(false);
                        //maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).gameObject.SetActive(false);
                        
                    }

                }
            }

            
        }
        else
        {
            Debug.Log("NYT PITÄISI OLLA SISÄLLÄ FIGHTISSA ALUEEN ISON BOSSIN KANSSA!");
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
        if (GameManager.gm.enemysInGame[GameManager.gm.activeEnemy].GetComponent<EnemyOne>().eB.enemyType == 1)
        {
            mergeHorde = false;
            enemyCanAttack = false;
            enemyIsAttacking = false;

            for (int i = 0; i < enemysInGame.Count; i++)
            {
                enemysInGame[i].transform.GetChild(5).gameObject.SetActive(false);
            }

            Debug.Log("GAMEMANAGER MERGE FUNCTIO SAAVUTETTU");

            GameObject temp = GameObject.Find("TempHolder");
            temp.transform.GetChild(0).GetComponent<EnemyOne>().eB.enemyHealth = temp.transform.GetChild(0).GetComponent<EnemyOne>().eB.enemyHealth + temp.transform.GetChild(1).GetComponent<EnemyOne>().eB.enemyHealth;
            temp.transform.GetChild(0).GetComponent<Image>().sprite = temp.transform.GetChild(0).GetComponent<EnemyOne>().eB.enemyImages[1];

            enemysInGame.Remove(temp.transform.GetChild(1).gameObject);
            Destroy(temp.transform.GetChild(1).gameObject);

            temp.transform.GetChild(0).gameObject.transform.SetParent(maps.gameObject.transform.GetChild(currentMissionIndex).GetChild(2));
        }
        
    }

    public void SetupForNextRound()
    {
        characterHolder.SetActive(true);

        for (int s = 0; s < enemySpawnersIngame.Count; s++)
        {
            enemySpawnersIngame[s].gameObject.SetActive(true);
        }

        if(round != 3)
        {
            GameObject.Find("BattleResultPanel").transform.GetChild(6).GetChild(0).gameObject.SetActive(true);
            GameObject.Find("BattleResultPanel").transform.GetChild(6).GetChild(1).gameObject.SetActive(false);
            GameObject.Find("BattleResultPanel").transform.GetChild(6).gameObject.SetActive(false);
        }
        


    }

}
