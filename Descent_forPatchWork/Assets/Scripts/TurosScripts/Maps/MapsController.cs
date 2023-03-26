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
        GameObject areaMapChild = areaMap.transform.GetChild(0).gameObject;
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
    }
}
