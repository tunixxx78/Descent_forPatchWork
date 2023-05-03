using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static DataHolder dataHolder;

    public int roundToSave;
    public int currenAreaMissionIndex = 4;
    public bool gameIsStarted = false;

    string wantedName;
    float wantedHealth;
    float wantedStrength;
    int wantedLevel;

    public string plrOneName = "";
    public float plrOneHealth = 18;
    public float plrOneStrength = 1;
    public int plrOneLevel = 0;
    //public int plrOneIndex;

    public string plrTwoName = "";
    public float plrTwoHealth = 8;
    public float plrTwoStrength = 1;
    public int plrTwoLevel = 0;
    //public int plrOneIndex;

    public string plrThreeName = "";
    public float plrThreeHealth = 12;
    public float plrThreeStrength = 2;
    public int plrThreeLevel = 0;
    //public int plrOneIndex;

    public string plrFourName = "";
    public float plrFourHealth = 10;
    public float plrFourStrength = 2;
    public int plrFourLevel = 0;
    //public int plrOneIndex;

    public List<GameObject> plrOneWeaponItems;
    public List<GameObject> plrOneCardItems;
    public List<GameObject> plrOneGold;

    public List<GameObject> plrTwoWeaponItems;
    public List<GameObject> plrTwoCardItems;
    public List<GameObject> plrTwoGold;

    public List<GameObject> plrThreeWeaponItems;
    public List<GameObject> plrThreeCardItems;
    public List<GameObject> plrThreeGold;

    public List<GameObject> plrFourWeaponItems;
    public List<GameObject> plrFourCardItems;
    public List<GameObject> plrFourGold;

    private void Awake()
    {
        if (DataHolder.dataHolder == null)
        {
            DataHolder.dataHolder = this;
        }
        else
        {
            if (DataHolder.dataHolder != this)
            {
                Destroy(DataHolder.dataHolder.gameObject);
                DataHolder.dataHolder = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);

        //GameManager.gm.currentAreaMissions = currenAreaMissionIndex;
    }

    private void Start()
    {
        //marko testing load-game method:
        SavingSystem.savingSystem.LoadGame();
        //Invoke("GoOnInGame", 0.5f);
        //GameManager.gm.currentAreaMissions = currenAreaMissionIndex;
    }

    private void GoONInGame()
    {
        GameManager.gm.areaInfoLoaded = true;
    }
    

    public void SetData(int plrIndex, string name, float health, float strength, int level, int round, int areaMissionIndex, bool hasStarted)
    {

        

        if (plrIndex == 0)
        {
            plrOneName = name;
            plrOneHealth = health;
            plrOneStrength = strength;
            plrOneLevel = level;
        }

        if (plrIndex == 1)
        {
            plrTwoName = name;
            plrTwoHealth = health;
            plrTwoStrength = strength;
            plrTwoLevel = level;
        }

        if (plrIndex == 2)
        {
            plrThreeName = name;
            plrThreeHealth = health;
            plrThreeStrength = strength;
            plrThreeLevel = level;
        }

        if (plrIndex == 3)
        {
            plrFourName = name;
            plrFourHealth = health;
            plrFourStrength = strength;
            plrFourLevel = level;
        }

        roundToSave = round;
        currenAreaMissionIndex = areaMissionIndex;
        gameIsStarted = hasStarted;

    }

    public void TakeCareOfSaving()
    {
        
        string[] names = {plrOneName, plrTwoName, plrThreeName, plrFourName };
        float[] healths = {plrOneHealth, plrTwoHealth, plrThreeHealth, plrFourHealth };
        float[] strenghts = {plrOneStrength, plrTwoStrength, plrThreeStrength, plrFourStrength };
        int[] levels = {plrOneLevel, plrTwoLevel, plrThreeLevel, plrFourLevel };

        SavingSystem.savingSystem.SaveGame2(names, healths, strenghts, levels, roundToSave, currenAreaMissionIndex, gameIsStarted);

        Debug.Log(roundToSave + "JA" + currenAreaMissionIndex);

    }

    public void SetLoot(int plrIndex, int lootIndex, GameObject item)
    {
        if(plrIndex == 0)
        {
            if(lootIndex == 0)
            {
                plrOneWeaponItems.Add(item);
            }
            if(lootIndex == 1)
            {
                plrOneCardItems.Add(item);
            }
            
        }
        if (plrIndex == 1)
        {
            if (lootIndex == 0)
            {
                plrTwoWeaponItems.Add(item);
            }
            if (lootIndex == 1)
            {
                plrTwoCardItems.Add(item);
            }
        }
        if (plrIndex == 2)
        {
            if (lootIndex == 0)
            {
                plrThreeWeaponItems.Add(item);
            }
            if (lootIndex == 1)
            {
                plrThreeCardItems.Add(item);
            }
        }
        if (plrIndex == 3)
        {
            if (lootIndex == 0)
            {
                plrFourWeaponItems.Add(item);
            }
            if (lootIndex == 1)
            {
                plrFourCardItems.Add(item);
            }
        }

    }

    public string GetName(int plrIndex)
    {

        if (plrIndex == 0)
        {
            wantedName = plrOneName;
        }

        if (plrIndex == 1)
        {
            wantedName = plrTwoName;
        }

        if (plrIndex == 2)
        {
            wantedName = plrThreeName;
        }

        if (plrIndex == 3)
        {
            wantedName = plrFourName;
        }

        Debug.Log(wantedName);
        return wantedName;

        
    }

    public float GetHealth(int plrIndex)
    {
        if (plrIndex == 0)
        {
            wantedHealth = plrOneHealth;
        }

        if (plrIndex == 1)
        {
            wantedHealth = plrTwoHealth;
        }

        if (plrIndex == 2)
        {
            wantedHealth = plrThreeHealth;
        }

        if (plrIndex == 3)
        {
            wantedHealth = plrThreeHealth;
        }

        Debug.Log("Pelaajan Health on: " + wantedHealth);
        return wantedHealth;

        
    }

    public float GetStrenght(int plrIndex)
    {
        if (plrIndex == 0)
        {
            wantedStrength = plrOneStrength;
        }

        if (plrIndex == 1)
        {
            wantedStrength = plrTwoStrength;
        }

        if (plrIndex == 2)
        {
            wantedStrength = plrThreeStrength;
        }

        if (plrIndex == 3)
        {
            wantedStrength = plrFourStrength;
        }

        return wantedStrength;
    }

    public int GetLevel(int plrIndex)
    {
        if (plrIndex == 0)
        {
            wantedLevel = plrOneLevel;
        }

        if (plrIndex == 1)
        {
            wantedLevel = plrTwoLevel;
        }

        if (plrIndex == 2)
        {
            wantedLevel = plrThreeLevel;
        }

        if (plrIndex == 3)
        {
            wantedLevel = plrFourLevel;
        }

        return wantedLevel;
    }

    public void GetLoot(int plrIndex)
    {
        
    }
}
