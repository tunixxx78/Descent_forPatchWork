using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemySpawnerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject bgImage, mapsController;

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
            if (Input.GetMouseButtonUp(0))
            {
                GameObject villagerInstance = Instantiate(mapsController.GetComponent<MapsController>().enemyBase[0], GameObject.Find("ExtraSpawnPoint").transform.position, Quaternion.identity);
                villagerInstance.transform.SetParent(mapsController.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2));
                GameManager.gm.enemysInGame.Add(villagerInstance);

                if (villagerInstance.GetComponent<EnemyOne>().eB.enemyType == 1)
                {
                    GameManager.gm.enemyHordHealth += villagerInstance.GetComponent<EnemyOne>().eB.enemyHealth;
                }
            }
        }
        if (GameManager.gm.lonerSelected)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameObject lonerInstance = Instantiate(mapsController.GetComponent<MapsController>().enemyBase[1], GameObject.Find("ExtraSpawnPoint").transform.position, Quaternion.identity);
                lonerInstance.transform.SetParent(mapsController.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2));
                GameManager.gm.enemysInGame.Add(lonerInstance);

                /*
                if (lonerInstance.GetComponent<EnemyOne>().eB.enemyType == 3)
                {
                    GameManager.gm.enemyHordHealth += lonerInstance.GetComponent<EnemyOne>().eB.enemyHealth;
                }
                */
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
                }
                else
                {
                    this.bgImage.SetActive(true);
                    GameManager.gm.villagerSelected = true;
                }
            }
            if (this.gameObject.CompareTag("Loner"))
            {
                if (bgImage.activeSelf == true)
                {
                    this.bgImage.SetActive(false);
                    GameManager.gm.lonerSelected = false;
                }
                else
                {
                    this.bgImage.SetActive(true);
                    GameManager.gm.lonerSelected = true;
                }
            }
        }
    }

    public void AddThisSpawnerToList()
    {
        GameManager.gm.enemySpawnersIngame.Add(this.gameObject);
    }

    
}
