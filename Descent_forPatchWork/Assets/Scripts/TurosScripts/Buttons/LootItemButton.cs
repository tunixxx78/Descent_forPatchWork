using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootItemButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent buttonClick;
    public LootItem[] loot;

    [SerializeField] GameObject[] lootItems;
    [SerializeField] int lootIndex, numberOftLootItems;

    private void Awake()
    {
        if(buttonClick == null)
        {
            buttonClick = new UnityEvent();
        }

        

    }
    private void Start()
    {
        for (int i = 0; i < loot.Length; i++)
        {
            int index = i + 1;

            Debug.Log("indeksi numero on: " + index);

            this.gameObject.transform.GetChild(0).GetChild(index).GetComponent<Image>().sprite = loot[i].lootItemImage;
            this.gameObject.transform.GetChild(0).GetChild(index).gameObject.SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("BUTTON DOWN!");

        

        int randItem = Random.Range(0, lootItems.Length);

        if (this.gameObject.CompareTag("LootItem"))
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

            //For adding all the loot in player inventory

            for(int i = 0; i < loot.Length; i++)
            {
                GameObject itemInstance = new GameObject();
                itemInstance.AddComponent<Image>();
                itemInstance.GetComponent<Image>().sprite = loot[i].lootItemImage;
                itemInstance.GetComponent<Image>().SetNativeSize();

                itemInstance.transform.SetParent(GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(9).GetChild(5));
                itemInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                for (int j = 0; j < GameManager.gm.heroesInGame.Count; j++)
                {
                    if (GameManager.gm.heroesInGame[j].GetComponent<HeroOne>().hb.plrIndex == GameManager.gm.activePlayer && /*GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hbi.weaponItems.Contains(lootInstance) == false &&*/ lootIndex == 0)
                    {
                        Debug.Log("SISÄLLÄ lootissa");

                        GameManager.gm.heroesInGame[j].GetComponent<HeroOne>().hbi.weaponItems.Add(itemInstance);
                        DataHolder.dataHolder.SetLoot(GameManager.gm.heroesInGame[j].GetComponent<HeroOne>().hb.plrIndex, lootIndex, itemInstance);
                    }

                    if (GameManager.gm.heroesInGame[j].GetComponent<HeroOne>().hb.plrIndex == GameManager.gm.activePlayer && /*GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hbi.cardItems.Contains(lootInstance) == false &&*/ lootIndex == 1)
                    {
                        Debug.Log("SISÄLLÄ lootissa");

                        GameManager.gm.heroesInGame[j].GetComponent<HeroOne>().hbi.cardItems.Add(itemInstance);
                        DataHolder.dataHolder.SetLoot(GameManager.gm.heroesInGame[j].GetComponent<HeroOne>().hb.plrIndex, lootIndex, itemInstance);
                    }
                }
            }

        }

        gameObject.tag = "CLICKED";
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("BUTTON UP!");
    }

    public void DestroyThisPanel()
    {
        Destroy(this.gameObject, 0.25f);
    }
}
