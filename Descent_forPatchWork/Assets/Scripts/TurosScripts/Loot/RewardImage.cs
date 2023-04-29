using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardImage : MonoBehaviour
{
    MapsController  maps;

    public void ShowMissionResult()
    {
        maps = FindObjectOfType<MapsController>();

        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load($"MissionInfo/End/{GameManager.gm.round}");
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).gameObject.SetActive(true);
        maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(2).gameObject.SetActive(true);
    }
}
