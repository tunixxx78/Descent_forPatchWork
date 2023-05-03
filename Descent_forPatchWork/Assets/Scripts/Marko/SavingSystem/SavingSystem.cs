using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
//using Unity.UI;
using System.Linq;

public class SavingSystem : MonoBehaviour
{

    public static SavingSystem savingSystem;


    public string[] saveSlots = new string[5];
    public string groupName = "Nameless Group";
    public int activeSaveSlot;
    public string activeScene;
    public SelectableHero[] partyHeroes = new SelectableHero[4];
    public List<int> selectedHeroes;  //selectedHeroes

    

    //MAKING SINGLETON
    private void Awake()
    {
        //if one is already, and it is not this 
        if(savingSystem != null && savingSystem != this)
        {
            //destroy this one (keep the one already existing)
            Destroy(this);
        }
        else
        {
            //make this one the singleton (no other earlier ones present)
            savingSystem = this;
            DontDestroyOnLoad(this);
        }
    }

    //gets DataHolder itself as parameter
    public void SaveGame(DataHolder gData)
    {
        //creates GameSavedData class
        //gets data from DataHolder, and saves each hero's data
        //to corresponding indexed hero in GameSavedData's herolist
        GameSavedData sData = new GameSavedData();

        sData.currentSceneName = activeScene;

        sData.savedHeroes[0].heroName   = gData.plrOneName;
        sData.savedHeroes[0].maxHealth  = (int)gData.plrOneHealth;
        sData.savedHeroes[0].strength   = (int)gData.plrOneStrength;
        sData.savedHeroes[0].level      = gData.plrOneLevel;

        sData.savedHeroes[1].heroName = gData.plrTwoName;
        sData.savedHeroes[1].maxHealth = (int)gData.plrTwoHealth;
        sData.savedHeroes[1].strength = (int)gData.plrTwoStrength;
        sData.savedHeroes[1].level = gData.plrTwoLevel;

        sData.savedHeroes[2].heroName = gData.plrThreeName;
        sData.savedHeroes[2].maxHealth = (int)gData.plrThreeHealth;
        sData.savedHeroes[2].strength = (int)gData.plrThreeStrength;
        sData.savedHeroes[2].level = gData.plrThreeLevel;

        sData.savedHeroes[3].heroName = gData.plrFourName;
        sData.savedHeroes[3].maxHealth = (int)gData.plrFourHealth;
        sData.savedHeroes[3].strength = (int)gData.plrFourStrength;
        sData.savedHeroes[3].level = gData.plrFourLevel;

        //IN PROGRESS
        //saving item lists.. for each hero..
        //foreach(GameObject go in gData.plrOneWeaponItems)
        //{
        //    //savedCombatItems is list of strings, supposed to take only the name of the item.
        //    sData.savedHeroes[0].savedCombatItems.Add(go.name); 
        //}

        string saveableData = JsonUtility.ToJson(sData, true);
        string filePath = Application.persistentDataPath + "/save" + activeSaveSlot.ToString() + ".json";
        Debug.Log("saving to: " + filePath);

        File.WriteAllText(filePath, saveableData);

    }

    //another way, gets arrays of attributes as parameters
    public void SaveGame2(string[] hNames, float[] hHealths, float[] hStrengths, int[]heroLevels, int round = 0, int areaMission = 0, bool hasStarted = false)
    {
        for(int x = 0; x < 4; x++)
        {
            Debug.Log(hNames[x]);
            Debug.Log(hHealths[x]);
            Debug.Log(hStrengths[x]);
            Debug.Log(heroLevels[x]);
        }

        GameSavedData sData = new GameSavedData();
        sData.saveSlot = this.activeSaveSlot;
        sData.currentSceneName = activeScene;
        sData.savedGroupName = groupName;
        sData.heroNumbers = selectedHeroes;
        sData.gameRound = round;

        // turo lisäsi
        sData.camI = areaMission;
        sData.isStarted = hasStarted;

        //loops each and saves to corresponding hero
        for (int i = 0; i < 4; i++)
        {
            SavedHero sHero = new SavedHero(); 
            sHero.heroName = hNames[i];
            sHero.maxHealth = (int)hHealths[i];
            sHero.strength = (int)hStrengths[i];
            sHero.level = heroLevels[i];

            sData.savedHeroes[i] = sHero;
        }
        string saveableData = JsonUtility.ToJson(sData, true);
        string filePath = Application.persistentDataPath + "/save" + activeSaveSlot.ToString() + ".json";
        Debug.Log("saving to: " + filePath);

        File.WriteAllText(filePath, saveableData);
    }

    
    public void SaveStartPhase()
    {
        GameSavedData gameSessionData = new GameSavedData();
        //all data that is going to be saved
        gameSessionData.currentSceneName = activeScene;
        gameSessionData.savedGroupName= groupName;
        gameSessionData.saveSlot= this.activeSaveSlot;
        //startsave takes selected heroes from list in loop, and creates
        //a savedHero(serializable) class for each
        //which are then saved to a list in GameSavedData class.
        for(int i = 0; i < partyHeroes.Length; i++)
        {
            SelectableHero hero = partyHeroes[i];
            if (hero != null) {
                //faster to use below..
                SavedHero sHero = new SavedHero();  //create serializable hero for saving data

                sHero.heroName = hero.heroName;
                sHero.heroRole = hero.heroRole;
                sHero.currentHealth = hero.currentHealth;
                sHero.maxHealth = hero.maxHealth;
                sHero.currentHealth = hero.currentHealth;
                sHero.currentActionPoints = hero.currentActionPoints;
                sHero.maxActionPoints = hero.maxActionPoints;
                sHero.strength = hero.plrStrength;
                sHero.shield = hero.plrShield;
                sHero.fate = hero.fate;
                sHero.exp = hero.exp;
                sHero.maxCombatItems = hero.maxCombatItems;
                sHero.maxItems = hero.maxItems;
                sHero.plrIndex = i;

                //this saves only cards' ids
                sHero.savedCombatItems = hero.combatItems;
                //if saving whole classes/objects..

                //foreach(CombatItem item in hero.combatItems)
                //{
                //    sHero.savedCombatItems.Add(item.id);    //save only IDs
                //}
                foreach (JourneyItem item in hero.journeyItems)
                {
                    sHero.savedJourneyItems.Add(item.id);
                }
                foreach (HiddenItem item in hero.hiddenItems)
                {
                    sHero.savedHiddenItems.Add(item.id);
                }
                
                gameSessionData.savedHeroes[i] = sHero;
            }

            gameSessionData.heroNumbers = selectedHeroes;
            
        }

        string saveableData = JsonUtility.ToJson(gameSessionData,true);
        string filePath = Path.Combine(Application.persistentDataPath,"save" + activeSaveSlot.ToString() +".json");
        Debug.Log("saving to: " + filePath);

        File.WriteAllText(filePath, saveableData);
    }



    //Sets the activaSaveSlot to proper button Id,
    //so DataHolder gets proper slot
    public void LoadLoadingSettings(int buttonId)
    {
        activeSaveSlot = buttonId;
        //get and set the scene, which has to be loaded.
        string filePath = Path.Combine(Application.persistentDataPath,"save" + activeSaveSlot.ToString() + ".json");
        if (File.Exists(filePath))
        {
            string savedData = File.ReadAllText(filePath);
            GameSavedData sData = JsonUtility.FromJson<GameSavedData>(savedData);
            activeScene = sData.currentSceneName;
            selectedHeroes = sData.heroNumbers;

        }
    }
    public void LoadGame()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "save" + activeSaveSlot.ToString() + ".json");
        if (File.Exists(filePath))
        {

            string savedData = File.ReadAllText(filePath);
           
            GameSavedData sData = JsonUtility.FromJson<GameSavedData>(savedData);
            groupName = sData.savedGroupName;
            Debug.Log("LOADING SCENENAME: " + sData.currentSceneName);
            activeScene = sData.currentSceneName;
            activeSaveSlot = sData.saveSlot;
            //sets gameRound
            GameManager.gm.round = sData.gameRound;
            //loop - load data for each hero,
            //from sData-class' heroes-array, and set it with SetData-method
            for (int i = 0; i < 4; i++)
            {
                DataHolder.dataHolder.SetData(i, sData.savedHeroes[i].heroName,
                    sData.savedHeroes[i].maxHealth,
                    sData.savedHeroes[i].strength,
                    sData.savedHeroes[i].level,
                    sData.gameRound,
                    sData.camI,
                    sData.isStarted);
            }

            //NoItemLists yet..
            //prolly need to search by stringnames on some other databank and add then..

        }
        
    }
          
        public void LoadSaveNames()
        {
        string filePath = Path.Combine(Application.persistentDataPath,"savedNames.json");
        if(File.Exists(filePath))
        {
            string savedNames = File.ReadAllText(filePath);

            SavedNamesData names = JsonUtility.FromJson<SavedNamesData>(savedNames);
            this.saveSlots[0] = names.save0;
            this.saveSlots[1] = names.save1;
            this.saveSlots[2] = names.save2;
            this.saveSlots[3] = names.save3;
            this.saveSlots[4] = names.save4;
        }
        else { Debug.Log("Not yet saved names file, no problem."); }
        
        }
        public void SaveSavedNames()
        {
        Debug.Log("saving savenames");
        SavedNamesData data = new SavedNamesData();

            data.save0 = saveSlots[0];
            data.save1 = saveSlots[1];
            data.save2 = saveSlots[2];
            data.save3 = saveSlots[3];
            data.save4 = saveSlots[4];

        string saveableNames = JsonUtility.ToJson(data,true);
        string filePath = Path.Combine(Application.persistentDataPath,"savedNames.json");
        File.WriteAllText(filePath, saveableNames);
        }

    private void DeleteSave(int saveSlot)
    {
        saveSlots[saveSlot] = "";
    }

}

//The Class which is actually saved in game save slots! ('the' save)
    [Serializable]
    class GameSavedData
    {
        public int saveSlot;
        public int gameRound;
    //turo lisäsi
        public int camI;
        public bool isStarted;

        public string savedGroupName;
        public string currentSceneName;

    //public List<SavedHero> savedHeroes = new List<SavedHero>();
    public List<int> heroNumbers;
    public SavedHero[]  savedHeroes = new SavedHero[4];
    
    }


    //Saveable list of names for LoadGame - buttons - if string is empty -> considered as free slot
    [Serializable]
    class SavedNamesData
    {
        public string  save0, save1, save2, save3, save4;
    }

//All data that is saved from heroes, and then given to GameSaveData,
//which saves heroes in list/array
[Serializable]
class SavedHero
{
    //public List<int> savedHeroNumbers;

    public int plrIndex;
    public string heroName;
    public string heroRole;

    public int currentHealth;
    public int maxHealth;

    public int currentActionPoints;
    public int maxActionPoints;

    public int level;

    public int strength;
    public int shield;
    public int fate;
    public int exp;

    public int maxCombatItems;
    public int maxItems;

    public List<string> savedCombatItems;
    public List<string> savedJourneyItems;
    public List<string> savedHiddenItems;
}
