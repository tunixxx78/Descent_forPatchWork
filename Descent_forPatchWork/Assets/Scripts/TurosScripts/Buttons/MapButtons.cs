using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent buttonClick;
    [SerializeField] Animator mapAnimator;
    [SerializeField] string animationTrigger;

    [SerializeField] bool isClosableButton;

    MapsController maps;

    GameObject mapPanel, missionIntroPanel;

    private void Awake()
    {
        if (buttonClick == null)
        {
            buttonClick = new UnityEvent();
        }

        mapPanel = GameObject.Find("MapPanel");
        mapAnimator = mapPanel.GetComponent<Animator>();
        maps = FindObjectOfType<MapsController>();

        FindObjectOfType<VideoController2>().SetStartMissionClip(0);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("BUTTON DOWN!");

        if (this.gameObject.CompareTag("Map"))
        {
            SavingSystem.savingSystem.LoadGame();
            GameManager.gm.currentAreaMissions = DataHolder.dataHolder.currenAreaMissionIndex;

            SFXHolder.sH.button.Play();

            mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(7).gameObject.SetActive(false);

            maps.mapsButton.SetActive(false);
            mapAnimator.SetBool(animationTrigger, true);
            GameManager.gm.QuestLorePanel.SetActive(true);

            
            GameManager.gm.currentMission = mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).gameObject;

            //for setting up mainQuest mission icon

            GameObject.Find("QuestLorePanel").transform.GetChild(2).GetComponent<Image>().sprite = mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(0).GetChild(1).GetChild(GameManager.gm.currentMissionInQuest).GetComponent<Image>().sprite;
            GameObject.Find("QuestLorePanel").transform.GetChild(2).GetComponent<Image>().SetNativeSize();
            GameObject.Find("QuestLorePanel").transform.GetChild(2).GetComponent<Image>().transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            for (int i = GameObject.Find("QuestLorePanel").transform.GetChild(5).GetChild(0).transform.childCount - 1; i > 0; i--)
            {
                Destroy(GameObject.Find("QuestLorePanel").transform.GetChild(5).GetChild(0).GetChild(i).gameObject);
            }
        }

        if (this.gameObject.CompareTag("WorldMapButton"))
        {
            SFXHolder.sH.button.Play();

            //GameManager.gm.currentAreaMissions = this.gameObject.GetComponent<BlockInformation>().blockImages.Length;
            GameObject.Find("AreaMapPanel").transform.GetChild(0).GetComponent<Image>().sprite = this.gameObject.GetComponent<BlockInformation>().RealAreaMap; 

            //mapAnimator.SetTrigger(animationTrigger);
            mapAnimator.SetBool(animationTrigger, true);
            mapAnimator.SetBool("WorldMapReveal_", false);
            GetComponent<BlockInformation>().setAreaImageInfo();

            maps.SeteRealArea();

            int subMissionIndex = 0;

            Debug.Log(GameObject.Find("QuestLorePanel").transform.GetChild(5).GetChild(0).transform.childCount);

            // for clearing AreaMaps sidePanel

            for (int i = GameObject.Find("QuestLorePanel").transform.GetChild(5).GetChild(0).transform.childCount - 1; i > 0; i--)
            {
                Destroy(GameObject.Find("QuestLorePanel").transform.GetChild(5).GetChild(0).GetChild(i).gameObject);
            }


            //for setting areaMaps missions to sidePanel
            // something hardCoded, needs to be fixed!!

            for (int i = 0; i < mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(1).GetChild(1).transform.childCount; i++)
            {
                GameObject obj = new GameObject();
                obj.transform.SetParent(GameManager.gm.QuestLorePanel.transform.GetChild(5).GetChild(0));
                obj.name = subMissionIndex.ToString();
                obj.tag = "SideQuest";
                //obj.AddComponent<Image>().sprite = this.gameObject.GetComponent<BlockInformation>().blockImages[i];
                //obj.GetComponent<Image>().SetNativeSize();
                //obj.GetComponent<Image>().transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                obj.AddComponent<Image>().transform.localScale = new Vector3(1, 1, 1);
                obj.GetComponent<Image>().sprite = this.gameObject.GetComponent<BlockInformation>().blockImages[i];
                obj.AddComponent<QuestLorePanelButton>();

                GameManager.gm.missionsInAreaMap.Add(obj);

                subMissionIndex++;
            }

            //for hiding missions from side panel

            for(int j = GameManager.gm.missionsInAreaMap.Count - 1; j > 0; j--)
            {
                GameManager.gm.missionsInAreaMap[j].gameObject.SetActive(false);
            }

            
        }

        if (this.gameObject.CompareTag("AreaMapButton"))
        {
            GameObject.FindGameObjectWithTag("BattleEvent").GetComponent<BattleEvent>().InitializeBattleEvent();
            if (GameManager.gm.round != 3)
            {
                if (isClosableButton)
                {
                    this.gameObject.transform.parent.gameObject.SetActive(false);
                }

                SFXHolder.sH.button.Play();

                //mapAnimator.SetTrigger(animationTrigger);
                mapAnimator.SetBool(animationTrigger, true);

                GetComponentInParent<BlockInformation>().SetBattleImageInfo();
                //GetComponent<BlockInformation>().SetBattleImageInfo();

                maps.SetRealBattleMap();
                GameManager.gm.QuestLorePanel.SetActive(false);
            }

            if(GameManager.gm.round == 3)
            {
                if (isClosableButton)
                {
                    this.gameObject.transform.parent.gameObject.SetActive(false);
                }

                SFXHolder.sH.button.Play();

                //mapAnimator.SetTrigger(animationTrigger);
                mapAnimator.SetBool(animationTrigger, true);

                GetComponentInParent<BlockInformation>().SetBattleImageInfo();

                //This needs to be changed for real functionality!!!
                //maps.SetRealBattleMap();
                maps.SetBossFightScene();

                GameManager.gm.QuestLorePanel.SetActive(false);

                GameManager.gm.bossLoot = true;
            }

            
        }

        if (this.gameObject.CompareTag("BackMapButton"))
        {

            GameManager.gm.round++;
            GameManager.gm.currentAreaMissions--;

            GameManager.gm.SetupForNextRound();
            SFXHolder.sH.button.Play();

            MusicHolder.mH.MusicOff(0);

            GameManager.gm.hordePanel.SetActive(true);
            GameManager.gm.enemyPanel.SetActive(true);

            mapAnimator.SetBool(animationTrigger, false);

            //for saving plrData for this session.

            for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
            {
                DataHolder.dataHolder.SetData(GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrIndex, GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrName,
                GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrHealth,
                GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength,
                GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrLevel,
                GameManager.gm.round,
                GameManager.gm.currentAreaMissions);

                
                /*
                for (int j = 0; j < GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hbi.cardItems.Count - 1; j++)
                {
                    GameObject lootInst = GameObject.Find("CollectedLootPanel").transform.GetChild(0).gameObject;
                    Debug.Log(lootInst);
                    lootInst.transform.SetParent(GameObject.Find("InventoryHolder").transform);
                }
                */
            }

            //Moves saved data to savingSystem
            DataHolder.dataHolder.TakeCareOfSaving();

            // Moves collected loot to inventoryHolder

            int moveIndex = GameObject.Find("CollectedLootPanel").transform.childCount;

            for (int j = 0; j < moveIndex; j++)
            {
                Debug.Log("Collected Loot folder child count is: " + GameObject.Find("CollectedLootPanel").transform.childCount);

                GameObject lootInst = GameObject.Find("CollectedLootPanel").transform.GetChild(0).gameObject;
                lootInst.transform.SetParent(GameObject.Find("InventoryHolder").transform);

                Debug.Log("Round: " + j + " fo moving loot items");
            }
            

            //for turning off player stat panels

            for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
            {
                GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.thisHeroIsAttacking = false;
                Destroy(GameManager.gm.heroesInGame[i]);
            }

            GameManager.gm.plrIsAttacking = false;

            for (int j = 0; j < GameManager.gm.enemysInGame.Count; j++)
            {
                Destroy(GameManager.gm.enemysInGame[j]);
            }

            GameManager.gm.heroesInGame.Clear();
            GameManager.gm.enemysInGame.Clear();

            GameObject PlrStats = GameObject.Find("PlayerPanels");

            for (int i = 0; i < PlrStats.transform.childCount; i++)
            {
                PlrStats.transform.GetChild(i).gameObject.SetActive(false);
                Debug.Log("PITÄISI KÄÄNTÄÄ JUTTUJA POIS PÄÄLTÄ");
            }

            maps.enemyHordPanel.SetActive(false);
            maps.enemyTwoPanel.SetActive(false);
            maps.mapPiecesBattle.Clear();
            GameManager.gm.QuestLorePanel.SetActive(true);

            mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(9).gameObject.SetActive(false);

            //for clearing lootListing in hierarchy
            /*
            for (int i = mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(9).GetChild(5).childCount; i > 0; i--)
            {
                Destroy(mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(9).GetChild(5).GetChild(i - 1).gameObject);
            }
            */

            

            // for seting up areaMap

            GameObject areaMap = GameObject.Find("MapPanel").transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(1).gameObject;
            //GameObject areaMap = currentObject.transform.Find("AreaMapPanel").gameObject;
            GameObject areaMapChild = areaMap.transform.GetChild(1).gameObject;
            int amountOfPieces = areaMapChild.transform.childCount;

            int whatToHide = (GameManager.gm.round);

            // Some issues here with area 2, needs to be fixed


            if(GameManager.gm.round == 4)
            {
                whatToHide = GameManager.gm.round - 4;
            }

            for (int j = 0; j < amountOfPieces; j++)
            {
                if (j <= whatToHide)
                {
                    areaMapChild.transform.GetChild(j).gameObject.SetActive(true);
                }

            }

            // for showing missions in sidePanel

            for (int s = 0; s < GameManager.gm.missionsInAreaMap.Count; s++)
            {
                if(s <= whatToHide)
                {
                    GameManager.gm.missionsInAreaMap[s].gameObject.SetActive(true);
                }
                
            }



            GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Image>().sprite = GameManager.gm.BGImages[0];

            if(GameManager.gm.round == 2)
            {
                GameManager.gm.canSpawnEnemys = false;
            }
            else { GameManager.gm.canSpawnEnemys = true; }

            maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(2).gameObject.SetActive(false);
            maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).gameObject.SetActive(false);
            

        }


        if (this.gameObject.CompareTag("ExitMapButton"))
        {

            GameManager.gm.currentAreaMissions--;
            

            if (GameManager.gm.round != 3)
            {
                SFXHolder.sH.button.Play();

                MusicHolder.mH.MusicOff(0);

                mapAnimator.SetTrigger(animationTrigger);
                mapAnimator.SetBool("AreaMapReveal_", false);
                mapAnimator.SetBool("BattleMapReveal_", false);
                maps.mapPiecesArea.Clear();
                maps.mapPiecesBattle.Clear();

                //for saving plrData for this session.

                for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                {
                    int currentRound = GameManager.gm.round + 1;

                    Debug.Log("TULOSTELLAAN MUUNNELTUA ROUND LUKUA JOKA ON: " + currentRound);

                    DataHolder.dataHolder.SetData(i, GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrName,
                    GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrHealth,
                    GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength,
                    GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrLevel,
                    currentRound,
                    GameManager.gm.currentAreaMissions);

                    DataHolder.dataHolder.TakeCareOfSaving();

                    for (int j = 0; j < GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hbi.cardItems.Count - 1; j++)
                    {
                        if (GameObject.Find("CollectedLootPanel").transform.childCount != 0)
                        {
                            GameObject lootInst = GameObject.Find("CollectedLootPanel").transform.GetChild(0).gameObject;
                            Debug.Log(lootInst);
                            lootInst.transform.SetParent(GameObject.Find("InventoryHolder").transform);
                        }

                    }

                }

                for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                {
                    GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.thisHeroIsAttacking = false;
                    Destroy(GameManager.gm.heroesInGame[i]);
                }

                GameManager.gm.plrIsAttacking = false;

                for (int j = 0; j < GameManager.gm.enemysInGame.Count; j++)
                {
                    Destroy(GameManager.gm.enemysInGame[j]);
                }

                GameManager.gm.heroesInGame.Clear();
                GameManager.gm.enemysInGame.Clear();

                GameManager.gm.enemyHordHealth = 0;
                GameManager.gm.enemyHordStrenght = 0;

                maps.mapsButton.SetActive(true);
                maps.enemyHordPanel.SetActive(false);
                maps.enemyTwoPanel.SetActive(false);

                //for turning off player stat panels

                GameObject PlrStats = GameObject.Find("PlayerPanels");

                for (int i = 0; i < PlrStats.transform.childCount; i++)
                {
                    PlrStats.transform.GetChild(i).gameObject.SetActive(false);
                }


                mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(9).gameObject.SetActive(false);

                //for clearing lootListing in hierarchy

                for (int i = mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(9).GetChild(5).childCount; i > 0; i--)
                {
                    Destroy(mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(9).GetChild(5).GetChild(i - 1).gameObject);
                }
            }

            

            if (GameManager.gm.round == 3)
            {
                SFXHolder.sH.button.Play();

                MusicHolder.mH.MusicOff(0);

                mapAnimator.SetTrigger(animationTrigger);
                mapAnimator.SetBool("AreaMapReveal_", false);
                mapAnimator.SetBool("BattleMapReveal_", false);
                maps.mapPiecesArea.Clear();
                maps.mapPiecesBattle.Clear();

                //for saving plrData for this session.

                for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
                {
                    GameManager.gm.currentAreaMissions = 4;
                    int currentRound = GameManager.gm.round + 1;

                    Debug.Log("TULOSTELLAAN MUUNNELTUA ROUND LUKUA JOKA ON: " + currentRound);
                    Debug.Log("TULOSTELLAAN MUUNNELTUA areaMission LUKUA JOKA ON: " + GameManager.gm.currentAreaMissions);

                    DataHolder.dataHolder.SetData(i, GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrName,
                    GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrHealth,
                    GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrStrength,
                    GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hb.plrLevel,
                    currentRound,
                    GameManager.gm.currentAreaMissions);

                    DataHolder.dataHolder.TakeCareOfSaving();

                    /*
                    for (int j = 0; j < GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hbi.cardItems.Count - 1; j++)
                    {
                        if (GameObject.Find("CollectedLootPanel").transform.childCount != 0)
                        {
                            GameObject lootInst = GameObject.Find("CollectedLootPanel").transform.GetChild(0).gameObject;
                            Debug.Log(lootInst);
                            lootInst.transform.SetParent(GameObject.Find("InventoryHolder").transform);
                        }

                    }
                    */

                }

                for (int k = 0; k < GameManager.gm.heroesInGame.Count; k++)
                {
                    GameManager.gm.heroesInGame[k].GetComponent<HeroOne>().hb.thisHeroIsAttacking = false;
                    Destroy(GameManager.gm.heroesInGame[k]);
                }

                GameManager.gm.plrIsAttacking = false;

                for (int j = 0; j < GameManager.gm.enemysInGame.Count; j++)
                {
                    Destroy(GameManager.gm.enemysInGame[j]);
                }

                GameManager.gm.heroesInGame.Clear();
                GameManager.gm.enemysInGame.Clear();

                GameManager.gm.enemyHordHealth = 0;
                GameManager.gm.enemyHordStrenght = 0;
                GameManager.gm.missionsInAreaMap.Clear();

                maps.mapsButton.SetActive(true);
                maps.enemyHordPanel.SetActive(false);
                maps.enemyTwoPanel.SetActive(false);

                //for turning off player stat panels

                GameObject PlrStats = GameObject.Find("PlayerPanels");

                for (int l = 0; l < PlrStats.transform.childCount; l++)
                {
                    PlrStats.transform.GetChild(l).gameObject.SetActive(false);
                }


                mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(9).gameObject.SetActive(false);

                //for clearing lootListing in hierarchy

                for (int m = mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(9).GetChild(5).childCount; m > 0; m--)
                {
                    Destroy(mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(9).GetChild(5).GetChild(m - 1).gameObject);
                }

                GameManager.gm.tempBossHealth = 10000;
                GameManager.gm.round++;

                maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).GetChild(3).gameObject.SetActive(false);
                maps.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(2).GetChild(18).gameObject.SetActive(false);

                mapPanel.transform.GetChild(GameManager.gm.currentMissionIndex).GetChild(7).gameObject.SetActive(true);

                GameManager.gm.areaInfoLoaded = true;

               
            }

            
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("BUTTON UP!");
    }

}
