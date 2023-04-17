using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public void ShowInventory()
    {

        Debug.Log(GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(7).GetChild(4).GetChild(1).transform.childCount);

        // for clearing plr inventory panel
        if (GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(7).GetChild(4).GetChild(1).transform.childCount != 0)
        {
            Debug.Log("NYT OLISI TARKOITUS DELETOIDA JUTTUJA");

            for (int d = GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(7).GetChild(4).GetChild(1).transform.childCount - 1; d >= 0; d--)
            {
                Destroy(GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(7).GetChild(4).GetChild(1).GetChild(d).gameObject);
            }
        }

        // for adding plr inventory to panel

        for (int i = 0; i < GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hbi.cardItems.Count; i++)
        {
            GameObject inventoryInstance = Instantiate(GameManager.gm.InventorySlotBase, GameObject.Find("Canvas").transform.position, Quaternion.identity);
            inventoryInstance.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hbi.cardItems[i].GetComponent<Image>().sprite;
            inventoryInstance.transform.SetParent(GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(7).GetChild(4).GetChild(1));
            inventoryInstance.transform.localScale = new Vector3(2f, 2f, 2f);

        }

        for (int j = 0; j < GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hbi.weaponItems.Count; j++)
        {
            GameObject inventoryInstance = Instantiate(GameManager.gm.InventorySlotBase, GameObject.Find("Canvas").transform.position, Quaternion.identity);
            inventoryInstance.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.gm.heroesInGame[GameManager.gm.activePlayer].GetComponent<HeroOne>().hbi.weaponItems[j].GetComponent<Image>().sprite;
            inventoryInstance.transform.SetParent(GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(7).GetChild(4).GetChild(1));
            inventoryInstance.transform.localScale = new Vector3(2f, 2f, 2f);

        }
    }
}
