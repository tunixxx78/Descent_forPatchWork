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

    public GameObject mapsButton, enemyHordPanel;


    private void Awake()
    {
        mapsButton = GameObject.Find("MapsButton");
        enemyHordPanel = GameObject.Find("EnemyHordStats");
        enemyHordPanel.SetActive(false);
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

    }

    public void SetRealBattleMap()
    {
        GameObject battleMap = currentObject.transform.Find("BattleMapPanel").gameObject;
        GameObject battleMapChild = battleMap.transform.GetChild(0).gameObject;
        int amountOfPieces = battleMapChild.transform.childCount;

        for (int i = 0; i < amountOfPieces; i++)
        {
            battleMapChild.transform.GetChild(i).GetComponent<Image>().sprite = mapPiecesBattle[i];
        }

        // for spawning heroes for fight
        for (int i = 0; i < heroBase.Length; i++)
        {
            var heroInstance = Instantiate(heroBase[i], heroSpawnPoints[i].position, Quaternion.identity);
            heroInstance.transform.SetParent(battleMap.transform);
            heroInstance.transform.position = heroSpawnPoints[i].position;
            heroInstance.transform.localScale = new Vector3(1, 1, 1);
            GameManager.gm.heroesInGame.Add(heroInstance);
        }

        //for spawning enemys for fight
        for(int e = 0; e < enemyBase.Length; e++)
        {
            var enemyInstance = Instantiate(enemyBase[e], enemySpawnPoints[e].position, Quaternion.identity);
            enemyInstance.transform.SetParent(battleMap.transform);
            enemyInstance.transform.position = enemySpawnPoints[e].position;
            enemyInstance.transform.localScale = new Vector3(1, 1, 1);

            GameManager.gm.enemyHordHealth += enemyInstance.GetComponent<EnemyOne>().eB.enemyHealth;
            GameManager.gm.enemyHordStrenght += enemyInstance.GetComponent<EnemyOne>().eB.enemyStrength;
            GameManager.gm.enemysInGame.Add(enemyInstance);

        }

        enemyHordPanel.SetActive(true);
    }
}
