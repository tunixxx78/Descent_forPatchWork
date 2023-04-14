using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LootItemButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent buttonClick;

    [SerializeField] GameObject[] lootItems;
    [SerializeField] int lootIndex;

    private void Awake()
    {
        if(buttonClick == null)
        {
            buttonClick = new UnityEvent();
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("BUTTON DOWN!");

        int randItem = Random.Range(0, lootItems.Length);

        if (this.gameObject.CompareTag("LootItem"))
        {
            GameObject lootInstance = Instantiate(lootItems[randItem], this.gameObject.transform.position, Quaternion.identity);

            
            lootInstance.transform.SetParent(GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(9).GetChild(5));
            lootInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            if (GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hbi.weaponItems.Contains(lootInstance) == false && lootIndex == 0)
            {
                GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hbi.weaponItems.Add(lootInstance);
                DataHolder.dataHolder.SetLoot(GameManager.gm.activePlayer, lootIndex, lootInstance);
            }

            if (GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hbi.cardItems.Contains(lootInstance) == false && lootIndex == 1)
            {
                GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hbi.cardItems.Add(lootInstance);
                DataHolder.dataHolder.SetLoot(GameManager.gm.activePlayer, lootIndex, lootInstance);
            }
            

            

            Destroy(this.gameObject, 0.25f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("BUTTON UP!");
    }
}
