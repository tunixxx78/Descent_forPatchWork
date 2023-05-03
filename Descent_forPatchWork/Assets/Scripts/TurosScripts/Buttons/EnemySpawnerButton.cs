using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemySpawnerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject bgImage, mapsController;
    bool canSpawn = false;

    public UnityEvent buttonClick;

    private void Awake()
    {
        if (buttonClick == null)
        {
            buttonClick = new UnityEvent();
        }

        mapsController = GameObject.Find("MapPanel");
    }

    private void Update()
    {
        if (GameManager.gm.villagerSelected)
        {
            //canSpawn = false;
            
            if (Input.GetMouseButtonUp(0))
            {
                if(GameManager.gm.round == 0)
                {
                    GameObject villagerInstance = Instantiate(mapsController.GetComponent<MapsController>().enemyBase[0], GameObject.Find("ExtraSpawnPoint").transform.position, Quaternion.identity);
                    villagerInstance.transform.SetParent(mapsController.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2));
                    GameManager.gm.enemysInGame.Add(villagerInstance);

                    if (villagerInstance.GetComponent<EnemyOne>().eB.enemyType == 1)
                    {
                        GameManager.gm.enemyHordHealth += villagerInstance.GetComponent<EnemyOne>().eB.enemyHealth;
                    }
                }
                if(GameManager.gm.round == 1)
                {
                    GameObject wolfInstance = Instantiate(mapsController.GetComponent<MapsController>().enemyBase[3], GameObject.Find("ExtraSpawnPoint").transform.position, Quaternion.identity);
                    wolfInstance.transform.SetParent(mapsController.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2));
                    GameManager.gm.enemysInGame.Add(wolfInstance);

                    if (wolfInstance.GetComponent<EnemyOne>().eB.enemyType == 1)
                    {
                        GameManager.gm.enemyHordHealth += wolfInstance.GetComponent<EnemyOne>().eB.enemyHealth;
                    }
                }
                
            }
        }
        if (GameManager.gm.lonerSelected)
        {
            //canSpawn = false;

            if (Input.GetMouseButtonUp(0))
            {
                if (GameManager.gm.round == 0)
                {
                    GameObject lonerInstance = Instantiate(mapsController.GetComponent<MapsController>().enemyBase[1], GameObject.Find("ExtraSpawnPoint").transform.position, Quaternion.identity);
                    lonerInstance.transform.SetParent(mapsController.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2));
                    GameManager.gm.enemysInGame.Add(lonerInstance);
                }
                if(GameManager.gm.round == 1)
                {
                    GameObject lonerInstance = Instantiate(mapsController.GetComponent<MapsController>().enemyBase[4], GameObject.Find("ExtraSpawnPoint").transform.position, Quaternion.identity);
                    lonerInstance.transform.SetParent(mapsController.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2));
                    GameManager.gm.enemysInGame.Add(lonerInstance);
                }
                
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(GameManager.gm.enemyIsAttacking == false)
        {
            if (this.gameObject.CompareTag("Villager"))
            {
                if(bgImage.activeSelf == true)
                {
                    this.bgImage.SetActive(false);
                    GameManager.gm.villagerSelected = false;
                    //canSpawn = true;
                }
                else
                {
                    this.bgImage.SetActive(true);
                    GameManager.gm.villagerSelected = true;
                    //canSpawn = true;
                }
            }
            if (this.gameObject.CompareTag("Loner"))
            {
                if (bgImage.activeSelf == true)
                {
                    this.bgImage.SetActive(false);
                    GameManager.gm.lonerSelected = false;
                    //canSpawn = true;
                }
                else
                {
                    this.bgImage.SetActive(true);
                    GameManager.gm.lonerSelected = true;
                    //canSpawn = true;
                }
            }
        }
    }

    public void AddThisSpawnerToList()
    {
        GameManager.gm.enemySpawnersIngame.Add(this.gameObject);
    }

    public void SetButtonImages()
    {
        if(GameManager.gm.round == 0)
        {
            GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(14).GetChild(0).GetComponent<Image>().sprite = FindObjectOfType<MapsController>().enemyTokens[0];
            GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(14).GetChild(1).GetComponent<Image>().sprite = FindObjectOfType<MapsController>().enemyTokens[1];
        }
        if (GameManager.gm.round == 1)
        {
            GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(14).GetChild(0).GetComponent<Image>().sprite = FindObjectOfType<MapsController>().enemyTokens[3];
            GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(14).GetChild(1).GetComponent<Image>().sprite = FindObjectOfType<MapsController>().enemyTokens[4];
        }

    }

    
}
