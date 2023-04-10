using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LootItemButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent buttonClick;

    [SerializeField] GameObject[] lootItems;

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

        if (this.gameObject.CompareTag("LootItem"))
        {
            GameObject lootInstance = Instantiate(lootItems[0], this.gameObject.transform.position, Quaternion.identity);
            
            lootInstance.transform.SetParent(GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(9).GetChild(5));
            lootInstance.transform.localScale = new Vector3(1f, 1f, 1f);

            GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hbi.cardItems.Add(lootInstance);

            DataHolder.dataHolder.SetLoot(GameManager.gm.activePlayer, lootInstance);

            Destroy(this.gameObject, 0.25f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("BUTTON UP!");
    }
}
