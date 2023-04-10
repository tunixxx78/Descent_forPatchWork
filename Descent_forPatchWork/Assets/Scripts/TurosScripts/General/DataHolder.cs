using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static DataHolder dataHolder;

    string wantedName;
    float wantedHealth;
    float wantedStrength;
    int wantedLevel;

    public string plrOneName;
    public float plrOneHealth;
    public float plrOneStrength;
    public int plrOneLevel;
    //public int plrOneIndex;

    public string plrTwoName;
    public float plrTwoHealth;
    public float plrTwoStrength;
    public int plrTwoLevel;
    //public int plrOneIndex;

    public string plrThreeName;
    public float plrThreeHealth;
    public float plrThreeStrength;
    public int plrThreeLevel;
    //public int plrOneIndex;

    public string plrFourName;
    public float plrFourHealth;
    public float plrFourStrength;
    public int plrFourLevel;
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
    }

    public void SetData(int plrIndex, string name, float health, float strength, int level)
    {
        if(plrIndex == 0)
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
    }

    public void SetLoot(int plrIndex, GameObject item)
    {
        if(plrIndex == 0)
        {
            plrOneCardItems.Add(item);
        }
        if (plrIndex == 1)
        {
            plrTwoCardItems.Add(item);
        }
        if (plrIndex == 2)
        {
            plrThreeCardItems.Add(item);
        }
        if (plrIndex == 3)
        {
            plrFourCardItems.Add(item);
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
