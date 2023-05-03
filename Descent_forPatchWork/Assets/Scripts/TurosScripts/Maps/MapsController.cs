using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapsController : MonoBehaviour
{
    public List<GameObject> missions = new List<GameObject>();
    public List<Sprite> mapPiecesWorld;
    public List<Sprite> mapPiecesArea;
    public List<Sprite> mapPiecesBattle;

    [SerializeField] int missionIndex = 0;
    bool worldBlocksAreChecked = false;
    GameObject currentObject;

    [SerializeField] Transform[] heroSpawnPoints;
    public GameObject[] heroBase;

    [SerializeField] Transform[] enemySpawnPoints;
    public GameObject[] enemyBase;

    public Sprite[] hordePanels;
    public Sprite[] bossPanels;
    public Sprite[] enemyTokens;

    public GameObject mapsButton, enemyHordPanel, enemyTwoPanel, maps;
    public Transform bossEnemySpawnPoint;
    int heroNumIndex;

    private void Awake()
    {
        mapsButton = GameObject.Find("MapsButton");
        enemyHordPanel = GameObject.Find("EnemyHordeStats");
        enemyHordPanel.SetActive(false);
        enemyTwoPanel = GameObject.Find("EnemyPanel");
        enemyTwoPanel.SetActive(false);
        heroNumIndex = 0;
        bossEnemySpawnPoint = GameObject.Find("BossSpawnPoint").transform;
        maps = GameObject.Find("MapPanel");
        
    }

    private void Start()
    {
        if (missionIndex == 0 && worldBlocksAreChecked == false)
        {
            currentObject = missions[missionIndex];
            int amountOfBlocks = currentObject.GetComponentInChildren<PieceCounter>().GetChildCount();
            GameObject thisMap = GameObject.Find("MapPieces");

            for (int i = 0; i < amountOfBlocks; i++)
            {
                mapPiecesWorld.Add(thisMap.transform.GetChild(i).GetComponent<Image>().sprite);
            }

            worldBlocksAreChecked = true;
        }

        // for adding enemy spawners to enemySpawnerList in gameManager

        for(int j = 0; j < GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(14).transform.childCount; j++)
        {
            GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(14).GetChild(j).GetComponent<EnemySpawnerButton>().AddThisSpawnerToList();
        }

        //FindObjectOfType<EnemySpawnerButton>().AddThisSpawnerToList();
    }


    public void SeteRealArea()
    {
        GameObject areaMap = currentObject.transform.Find("AreaMapPanel").gameObject;
        GameObject areaMapChild = areaMap.transform.GetChild(1).gameObject;
        int amountOfPieces = areaMapChild.transform.childCount;

        for(int i = 0; i < amountOfPieces; i++)
        {
            areaMapChild.transform.GetChild(i).GetComponent<Image>().sprite = mapPiecesArea[i];
        }

        //to hide not wanted missions in area

        int whatToHide = (GameManager.gm.round);
        if(GameManager.gm.round == 4)
        {
            whatToHide = GameManager.gm.round - 4;
        }

        for (int j = 0; j < amountOfPieces; j++)
        {
            if(j > whatToHide)
            {
                areaMapChild.transform.GetChild(j).gameObject.SetActive(false);
            }
            
        }
    }

    public void SetRealBattleMap()
    {
        //for setting spawners buttons
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(14).GetChild(0).GetComponent<EnemySpawnerButton>().SetButtonImages();
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(14).GetChild(1).GetComponent<EnemySpawnerButton>().SetButtonImages();

        //for setting right info window content
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load($"MissionInfo/Start/{GameManager.gm.round}");
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).gameObject.SetActive(true);
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(1).gameObject.SetActive(true);

        //For hiding cardViewButton

        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(5).gameObject.SetActive(false);
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).Find("BattleEvent").GetChild(0).gameObject.SetActive(false);



        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Image>().sprite = GameManager.gm.BGImages[1];

        MusicHolder.mH.MusicOff(1);

        heroNumIndex = 0;

        GameObject battleMap = currentObject.transform.Find("BattleMapPanel").gameObject;
        GameObject battleMapChild = battleMap.transform.GetChild(0).gameObject;
        GameObject characterHolder = battleMap.transform.GetChild(16).gameObject;
        int amountOfPieces = battleMapChild.transform.childCount;

        battleMapChild.transform.GetChild(0).GetComponent<Image>().sprite = mapPiecesBattle[0];

        // for setting battleTitleIcon

        battleMapChild.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load($"BattleTittleImages/{GameManager.gm.round}");


        // for spawning heroes for fight

        for (int i = 0; i < SavingSystem.savingSystem.selectedHeroes.Count; i++) //for (int i = 0; i < heroBase.Length; i++)
        {

            Debug.Log("PITÄISI TULLA HERO NRO: " + SavingSystem.savingSystem.selectedHeroes[i]);

            var heroInstance = Instantiate(heroBase[SavingSystem.savingSystem.selectedHeroes[i]], heroSpawnPoints[SavingSystem.savingSystem.selectedHeroes[i]].position, Quaternion.identity);
            heroInstance.GetComponent<HeroOne>().hb.plrIndex = SavingSystem.savingSystem.selectedHeroes[i];


            if(GameManager.gm.round != 0 || GameManager.gm.round == 0 && DataHolder.dataHolder.gameIsStarted)
            {

                heroInstance.GetComponent<HeroOne>().hb.plrName = DataHolder.dataHolder.GetName(SavingSystem.savingSystem.selectedHeroes[i]);
                Debug.Log(heroInstance.GetComponent<HeroOne>().hb.plrName);
                heroInstance.GetComponent<HeroOne>().hb.plrHealth = DataHolder.dataHolder.GetHealth(SavingSystem.savingSystem.selectedHeroes[i]);
                Debug.Log(heroInstance.GetComponent<HeroOne>().hb.plrHealth);
                heroInstance.GetComponent<HeroOne>().hb.plrStrength = DataHolder.dataHolder.GetStrenght(SavingSystem.savingSystem.selectedHeroes[i]);
                heroInstance.GetComponent<HeroOne>().hb.plrLevel = DataHolder.dataHolder.GetLevel(SavingSystem.savingSystem.selectedHeroes[i]);

                if(SavingSystem.savingSystem.selectedHeroes[i] == 0)
                {
                    if(DataHolder.dataHolder.plrOneCardItems.Count != 0)
                    {
                        for (int j = 0; j < DataHolder.dataHolder.plrOneCardItems.Count; j++)
                        {
                            heroInstance.GetComponent<HeroOne>().hbi.cardItems.Add(DataHolder.dataHolder.plrOneCardItems[j]);
                        }
                        for (int k = 0; k < DataHolder.dataHolder.plrOneWeaponItems.Count; k++)
                        {
                            heroInstance.GetComponent<HeroOne>().hbi.weaponItems.Add(DataHolder.dataHolder.plrOneWeaponItems[k]);
                        }
                    }
                       
                }
                if (SavingSystem.savingSystem.selectedHeroes[i] == 1)
                {
                    if(DataHolder.dataHolder.plrTwoCardItems.Count != 0)
                    {
                        for (int j = 0; j < DataHolder.dataHolder.plrTwoWeaponItems.Count; j++)
                        {
                            heroInstance.GetComponent<HeroOne>().hbi.weaponItems.Add(DataHolder.dataHolder.plrTwoWeaponItems[j]);
                        }
                        for (int k = 0; k < DataHolder.dataHolder.plrTwoCardItems.Count; k++)
                        {
                            heroInstance.GetComponent<HeroOne>().hbi.cardItems.Add(DataHolder.dataHolder.plrTwoCardItems[k]);
                        }
                    }
                    
                }
                if (SavingSystem.savingSystem.selectedHeroes[i] == 2)
                {
                    for (int j = 0; j < DataHolder.dataHolder.plrThreeCardItems.Count; j++)
                    {
                        heroInstance.GetComponent<HeroOne>().hbi.cardItems.Add(DataHolder.dataHolder.plrThreeCardItems[j]);
                    }
                    for (int k = 0; k < DataHolder.dataHolder.plrThreeWeaponItems.Count; k++)
                    {
                        heroInstance.GetComponent<HeroOne>().hbi.weaponItems.Add(DataHolder.dataHolder.plrThreeWeaponItems[k]);
                    }
                }
                if (SavingSystem.savingSystem.selectedHeroes[i] == 3)
                {
                    for (int j = 0; j < DataHolder.dataHolder.plrFourCardItems.Count; j++)
                    {
                        heroInstance.GetComponent<HeroOne>().hbi.cardItems.Add(DataHolder.dataHolder.plrFourCardItems[j]);
                    }
                    for (int k = 0; k < DataHolder.dataHolder.plrFourWeaponItems.Count; k++)
                    {
                        heroInstance.GetComponent<HeroOne>().hbi.weaponItems.Add(DataHolder.dataHolder.plrFourWeaponItems[k]);
                    }
                }
            }
            
            heroInstance.transform.SetParent(characterHolder.transform);
            heroInstance.transform.position = heroSpawnPoints[SavingSystem.savingSystem.selectedHeroes[i]].position;
            heroInstance.transform.localScale = new Vector3(1, 1, 1);
            GameManager.gm.heroesInGame.Add(heroInstance);

            heroNumIndex++;
        }

        DataHolder.dataHolder.gameIsStarted = true;

        //for spawning enemys for fight

        if (GameManager.gm.canSpawnEnemys)
        {
            if(GameManager.gm.round == 0)
            {
                for (int e = 0; e < enemySpawnPoints.Length; e++)
                {
                    Debug.Log("e:n arvo on: " + e);

                    if (e == 0 || e == 1 || e == 4)
                    {
                        var enemyInstance = Instantiate(enemyBase[0], enemySpawnPoints[e].position, Quaternion.identity);
                        enemyInstance.transform.SetParent(characterHolder.transform);
                        enemyInstance.transform.position = enemySpawnPoints[e].position;
                        enemyInstance.transform.localScale = new Vector3(1, 1, 1);

                        // for adding enemytype 1 health to enemyHordePool

                        if (enemyInstance.GetComponent<EnemyOne>().eB.enemyType == 1)
                        {
                            GameManager.gm.enemyHordHealth += enemyInstance.GetComponent<EnemyOne>().eB.enemyHealth;
                            GameManager.gm.enemyHordStrenght += enemyInstance.GetComponent<EnemyOne>().eB.enemyStrength;
                        }

                        //GameManager.gm.enemyHordStrenght += enemyInstance.GetComponent<EnemyOne>().eB.enemyStrength;
                        GameManager.gm.enemysInGame.Add(enemyInstance);
                    }
                    if(e == 2|| e == 3)
                    {
                        var enemyInstance = Instantiate(enemyBase[1], enemySpawnPoints[e].position, Quaternion.identity);
                        enemyInstance.transform.SetParent(characterHolder.transform);
                        enemyInstance.transform.position = enemySpawnPoints[e].position;
                        enemyInstance.transform.localScale = new Vector3(1, 1, 1);

                        // for adding enemytype 1 health to enemyHordePool

                        if (enemyInstance.GetComponent<EnemyOne>().eB.enemyType == 1)
                        {
                            GameManager.gm.enemyHordHealth += enemyInstance.GetComponent<EnemyOne>().eB.enemyHealth;
                            GameManager.gm.enemyHordStrenght += enemyInstance.GetComponent<EnemyOne>().eB.enemyStrength;
                        }

                        //GameManager.gm.enemyHordStrenght += enemyInstance.GetComponent<EnemyOne>().eB.enemyStrength;
                        GameManager.gm.enemysInGame.Add(enemyInstance);
                    }
                    

                    

                    enemyHordPanel.GetComponent<Image>().sprite = hordePanels[GameManager.gm.round];
                    enemyHordPanel.SetActive(true);
                }
            }

            if(GameManager.gm.round == 1)
            {
                for (int e = 0; e < enemySpawnPoints.Length; e++)
                {
                    Debug.Log("e:n arvo on: " + e);

                    if (e == 0 || e == 1 || e == 4)
                    {
                        var enemyInstance = Instantiate(enemyBase[3], enemySpawnPoints[e].position, Quaternion.identity);
                        enemyInstance.transform.SetParent(characterHolder.transform);
                        enemyInstance.transform.position = enemySpawnPoints[e].position;
                        enemyInstance.transform.localScale = new Vector3(1, 1, 1);

                        // for adding enemytype 1 health to enemyHordePool

                        if (enemyInstance.GetComponent<EnemyOne>().eB.enemyType == 1)
                        {
                            GameManager.gm.enemyHordHealth += enemyInstance.GetComponent<EnemyOne>().eB.enemyHealth;
                            GameManager.gm.enemyHordStrenght += enemyInstance.GetComponent<EnemyOne>().eB.enemyStrength;
                        }

                        //GameManager.gm.enemyHordStrenght += enemyInstance.GetComponent<EnemyOne>().eB.enemyStrength;
                        GameManager.gm.enemysInGame.Add(enemyInstance);
                    }
                    if (e == 2 || e == 3)
                    {
                        var enemyInstance = Instantiate(enemyBase[4], enemySpawnPoints[e].position, Quaternion.identity);
                        enemyInstance.transform.SetParent(characterHolder.transform);
                        enemyInstance.transform.position = enemySpawnPoints[e].position;
                        enemyInstance.transform.localScale = new Vector3(1, 1, 1);

                        // for adding enemytype 1 health to enemyHordePool

                        if (enemyInstance.GetComponent<EnemyOne>().eB.enemyType == 1)
                        {
                            GameManager.gm.enemyHordHealth += enemyInstance.GetComponent<EnemyOne>().eB.enemyHealth;
                            GameManager.gm.enemyHordStrenght += enemyInstance.GetComponent<EnemyOne>().eB.enemyStrength;
                        }

                        //GameManager.gm.enemyHordStrenght += enemyInstance.GetComponent<EnemyOne>().eB.enemyStrength;
                        GameManager.gm.enemysInGame.Add(enemyInstance);
                    }
                    /*
                    var enemyInstance = Instantiate(enemyBase[3], enemySpawnPoints[e].position, Quaternion.identity);
                    enemyInstance.transform.SetParent(characterHolder.transform);
                    enemyInstance.transform.position = enemySpawnPoints[e].position;
                    enemyInstance.transform.localScale = new Vector3(1, 1, 1);

                    // for adding enemytype 1 health to enemyHordePool

                    if (enemyInstance.GetComponent<EnemyOne>().eB.enemyType == 1)
                    {
                        GameManager.gm.enemyHordHealth += enemyInstance.GetComponent<EnemyOne>().eB.enemyHealth;
                        GameManager.gm.enemyHordStrenght += enemyInstance.GetComponent<EnemyOne>().eB.enemyStrength;
                    }

                    //GameManager.gm.enemyHordStrenght += enemyInstance.GetComponent<EnemyOne>().eB.enemyStrength;
                    GameManager.gm.enemysInGame.Add(enemyInstance);
                    */
                    enemyHordPanel.GetComponent<Image>().sprite = hordePanels[GameManager.gm.round];
                    enemyHordPanel.SetActive(true);
                }
            }

            
        }


        GameObject.Find("/Canvas/MapPanel/TestMission/CardView").SetActive(true);
        //GameObject.Find("/Canvas/MapPanel/TestMission/CardViewButton").SetActive(true);
        
        //enemyTwoPanel.SetActive(true);
        GameManager.gm.battleIsOn = true;

        if(GameManager.gm.round != 2)
        {
            FindObjectOfType<DiceFunctionalityManager>().SetDiceEyeCommand(1);
        }
        
    }

    public void SetBossFightScene()
    {
        //For hiding enemySpawnerButtons
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(14).GetChild(0).gameObject.SetActive(false);
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(14).GetChild(1).gameObject.SetActive(false);

        //for setting right info window content
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load($"MissionInfo/Start/{GameManager.gm.round}");
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).gameObject.SetActive(true);
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(1).gameObject.SetActive(true);

        //For hiding cardViewButton

        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(5).gameObject.SetActive(false);
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).Find("BattleEvent").GetChild(0).gameObject.SetActive(false);



        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Image>().sprite = GameManager.gm.BGImages[1];

        MusicHolder.mH.MusicOff(1);

        heroNumIndex = 0;

        GameObject battleMap = currentObject.transform.Find("BattleMapPanel").gameObject;
        GameObject battleMapChild = battleMap.transform.GetChild(0).gameObject;
        GameObject characterHolder = battleMap.transform.GetChild(16).gameObject;
        int amountOfPieces = battleMapChild.transform.childCount;

        battleMapChild.transform.GetChild(0).GetComponent<Image>().sprite = mapPiecesBattle[0];

        // for setting battleTitleIcon

        battleMapChild.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load($"BattleTittleImages/{GameManager.gm.round}");


        // for spawning heroes for fight

        for (int i = 0; i < SavingSystem.savingSystem.selectedHeroes.Count; i++) //for (int i = 0; i < heroBase.Length; i++)
        {

            Debug.Log("PITÄISI TULLA HERO NRO: " + SavingSystem.savingSystem.selectedHeroes[i]);

            var heroInstance = Instantiate(heroBase[SavingSystem.savingSystem.selectedHeroes[i]], heroSpawnPoints[SavingSystem.savingSystem.selectedHeroes[i]].position, Quaternion.identity);
            heroInstance.GetComponent<HeroOne>().hb.plrIndex = SavingSystem.savingSystem.selectedHeroes[i];


            if (GameManager.gm.round != 0 || GameManager.gm.round == 0 && DataHolder.dataHolder.gameIsStarted)
            {

                heroInstance.GetComponent<HeroOne>().hb.plrName = DataHolder.dataHolder.GetName(SavingSystem.savingSystem.selectedHeroes[i]);
                Debug.Log(heroInstance.GetComponent<HeroOne>().hb.plrName);
                heroInstance.GetComponent<HeroOne>().hb.plrHealth = DataHolder.dataHolder.GetHealth(SavingSystem.savingSystem.selectedHeroes[i]);
                Debug.Log(heroInstance.GetComponent<HeroOne>().hb.plrHealth);
                heroInstance.GetComponent<HeroOne>().hb.plrStrength = DataHolder.dataHolder.GetStrenght(SavingSystem.savingSystem.selectedHeroes[i]);
                heroInstance.GetComponent<HeroOne>().hb.plrLevel = DataHolder.dataHolder.GetLevel(SavingSystem.savingSystem.selectedHeroes[i]);

                if (SavingSystem.savingSystem.selectedHeroes[i] == 0)
                {
                    if (DataHolder.dataHolder.plrOneCardItems.Count != 0)
                    {
                        for (int j = 0; j < DataHolder.dataHolder.plrOneCardItems.Count; j++)
                        {
                            heroInstance.GetComponent<HeroOne>().hbi.cardItems.Add(DataHolder.dataHolder.plrOneCardItems[j]);
                        }
                        for (int k = 0; k < DataHolder.dataHolder.plrOneWeaponItems.Count; k++)
                        {
                            heroInstance.GetComponent<HeroOne>().hbi.weaponItems.Add(DataHolder.dataHolder.plrOneWeaponItems[k]);
                        }
                    }

                }
                if (SavingSystem.savingSystem.selectedHeroes[i] == 1)
                {
                    if (DataHolder.dataHolder.plrTwoCardItems.Count != 0)
                    {
                        for (int j = 0; j < DataHolder.dataHolder.plrTwoWeaponItems.Count; j++)
                        {
                            heroInstance.GetComponent<HeroOne>().hbi.weaponItems.Add(DataHolder.dataHolder.plrTwoWeaponItems[j]);
                        }
                        for (int k = 0; k < DataHolder.dataHolder.plrTwoCardItems.Count; k++)
                        {
                            heroInstance.GetComponent<HeroOne>().hbi.cardItems.Add(DataHolder.dataHolder.plrTwoCardItems[k]);
                        }
                    }

                }
                if (SavingSystem.savingSystem.selectedHeroes[i] == 2)
                {
                    for (int j = 0; j < DataHolder.dataHolder.plrThreeCardItems.Count; j++)
                    {
                        heroInstance.GetComponent<HeroOne>().hbi.cardItems.Add(DataHolder.dataHolder.plrThreeCardItems[j]);
                    }
                    for (int k = 0; k < DataHolder.dataHolder.plrThreeWeaponItems.Count; k++)
                    {
                        heroInstance.GetComponent<HeroOne>().hbi.weaponItems.Add(DataHolder.dataHolder.plrThreeWeaponItems[k]);
                    }
                }
                if (SavingSystem.savingSystem.selectedHeroes[i] == 3)
                {
                    for (int j = 0; j < DataHolder.dataHolder.plrFourCardItems.Count; j++)
                    {
                        heroInstance.GetComponent<HeroOne>().hbi.cardItems.Add(DataHolder.dataHolder.plrFourCardItems[j]);
                    }
                    for (int k = 0; k < DataHolder.dataHolder.plrFourWeaponItems.Count; k++)
                    {
                        heroInstance.GetComponent<HeroOne>().hbi.weaponItems.Add(DataHolder.dataHolder.plrFourWeaponItems[k]);
                    }
                }
            }

            heroInstance.transform.SetParent(characterHolder.transform);
            heroInstance.transform.position = heroSpawnPoints[SavingSystem.savingSystem.selectedHeroes[i]].position;
            heroInstance.transform.localScale = new Vector3(1, 1, 1);
            GameManager.gm.heroesInGame.Add(heroInstance);

            heroNumIndex++;
        }
        GameObject.Find("/Canvas/MapPanel/TestMission/CardView").SetActive(true);

        GameManager.gm.battleIsOn = true;
    }
}
