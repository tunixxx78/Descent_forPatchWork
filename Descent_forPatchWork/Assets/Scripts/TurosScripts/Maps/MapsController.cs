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
    [SerializeField] GameObject[] heroBase;

    [SerializeField] Transform[] enemySpawnPoints;
    [SerializeField] GameObject[] enemyBase;

    public GameObject mapsButton, enemyHordPanel, enemyTwoPanel;
    int heroNumIndex;

    private void Awake()
    {
        mapsButton = GameObject.Find("MapsButton");
        enemyHordPanel = GameObject.Find("EnemyHordeStats");
        enemyHordPanel.SetActive(false);
        enemyTwoPanel = GameObject.Find("EnemyPanel");
        enemyTwoPanel.SetActive(false);
        heroNumIndex = 0;
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

        for (int j = 0; j < amountOfPieces; j++)
        {
            if(j > whatToHide)
            {
                areaMapChild.transform.GetChild(j).gameObject.SetActive(false);
            }
            
        }
        /*
        for(int j = amountOfPieces - (whatToHide + 1); j >= 0; j--)
        {
            areaMapChild.transform.GetChild(j).gameObject.SetActive(false);
        }
        */
    }

    public void SetRealBattleMap()
    {
        MusicHolder.mH.MusicOff(1);

        heroNumIndex = 0;

        GameObject battleMap = currentObject.transform.Find("BattleMapPanel").gameObject;
        GameObject battleMapChild = battleMap.transform.GetChild(0).gameObject;
        int amountOfPieces = battleMapChild.transform.childCount;

        battleMapChild.transform.GetChild(0).GetComponent<Image>().sprite = mapPiecesBattle[0];

        /*
        for (int i = 0; i < amountOfPieces; i++)
        {
            battleMapChild.transform.GetChild(i).GetComponent<Image>().sprite = mapPiecesBattle[i];
        }
        */


        // for spawning heroes for fight

        for(int i = 0; i < SavingSystem.savingSystem.selectedHeroes.Count; i++) //for (int i = 0; i < heroBase.Length; i++)
        {

            Debug.Log("PITÄISI TULLA HERO NRO: " + SavingSystem.savingSystem.selectedHeroes[i]);

            var heroInstance = Instantiate(heroBase[SavingSystem.savingSystem.selectedHeroes[i]], heroSpawnPoints[SavingSystem.savingSystem.selectedHeroes[i]].position, Quaternion.identity);
            heroInstance.GetComponent<HeroOne>().hb.plrIndex = SavingSystem.savingSystem.selectedHeroes[i];

            if(GameManager.gm.round != 0)
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
                }
                if (SavingSystem.savingSystem.selectedHeroes[i] == 3)
                {
                    for (int j = 0; j < DataHolder.dataHolder.plrFourCardItems.Count; j++)
                    {
                        heroInstance.GetComponent<HeroOne>().hbi.cardItems.Add(DataHolder.dataHolder.plrFourCardItems[j]);
                    }
                }
            }
            
            heroInstance.transform.SetParent(battleMap.transform);
            heroInstance.transform.position = heroSpawnPoints[SavingSystem.savingSystem.selectedHeroes[i]].position;
            heroInstance.transform.localScale = new Vector3(1, 1, 1);
            GameManager.gm.heroesInGame.Add(heroInstance);

            heroNumIndex++;
        }

        //for spawning enemys for fight

        
        for(int e = 0; e < enemyBase.Length; e++)
        {
            var enemyInstance = Instantiate(enemyBase[e], enemySpawnPoints[e].position, Quaternion.identity);
            enemyInstance.transform.SetParent(battleMap.transform);
            enemyInstance.transform.position = enemySpawnPoints[e].position;
            enemyInstance.transform.localScale = new Vector3(1, 1, 1);

            // for adding enemytype 1 health to enemyHordePool

            if(enemyInstance.GetComponent<EnemyOne>().eB.enemyType == 1)
            {
                GameManager.gm.enemyHordHealth += enemyInstance.GetComponent<EnemyOne>().eB.enemyHealth;
            }
            
            //GameManager.gm.enemyHordStrenght += enemyInstance.GetComponent<EnemyOne>().eB.enemyStrength;
            GameManager.gm.enemysInGame.Add(enemyInstance);

            
        }

        enemyHordPanel.SetActive(true);
        enemyTwoPanel.SetActive(true);
        GameManager.gm.battleIsOn = true;
    }
}
