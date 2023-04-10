using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{

    public static SavingSystem savingSystem;


    public string[] saveSlots = new string[5];
    public string groupName = "Nameless Group";
    public int activeSaveSlot;
    public string activeScene;
    public List<SelectableHero> partyHeroes;
    //public SelectableHero[] partyHeroes = new SelectableHero[5];

    private void Awake()
    {
        if(savingSystem != null && savingSystem != this)
        {
            Destroy(this);
        }
        else
        {
            savingSystem = this;
            DontDestroyOnLoad(this);
        }
    }


    public void SaveGame()
    {
        GameSavedData gameSessionData = new GameSavedData();
        //all data that is going to be saved
        gameSessionData.currentSceneName = activeScene;
        gameSessionData.savedGroupName= groupName;

        for(int i = 0; i < partyHeroes.Count; i++)
        {
            SelectableHero hero = partyHeroes[i]; //faster to write below..
            SavedHero sHero = new SavedHero();  //create serializable hero for saving data

            sHero.heroName = hero.heroName;
            sHero.heroRole  = hero.heroRole;
            sHero.currentHealth = hero.currentHealth;
            sHero.maxHealth = hero.maxHealth;
            sHero.currentHealth= hero.currentHealth;
            sHero.currentActionPoints = hero.currentActionPoints;
            sHero.maxActionPoints = hero.maxActionPoints;
            sHero.atk = hero.atk;
            sHero.def =     hero.def;
            sHero.fate = hero.fate;
            sHero.exp = hero.exp;
            sHero.maxCombatItems = hero.maxCombatItems;
            sHero.maxItems = hero.maxItems;

            //save good itams..
            foreach(CombatItem item in hero.combatItems)
            {
                sHero.savedCombatItems.Add(item.id);    //save only IDs
            }
            foreach(JourneyItem item in hero.journeyItems)
            {
                sHero.savedJourneyItems.Add(item.id);
            }
            foreach(HiddenItem item in hero.hiddenItems)
            {
                sHero.savedHiddenItems.Add(item.id);
            }

            gameSessionData.savedHeroes.Add(sHero);

        }

        string saveableData = JsonUtility.ToJson(gameSessionData,true);
        string filePath = Application.persistentDataPath + "/save" + activeSaveSlot.ToString() + ".json";
        Debug.Log("saving to: " + filePath);

        File.WriteAllText(filePath, saveableData);
    }

        public void LoadGame(int saveName)
        {
        string filePath = Application.persistentDataPath + "/save" + saveName.ToString() + ".json";
        string savedData = File.ReadAllText(filePath);

        GameSavedData loadableData = JsonUtility.FromJson<GameSavedData>(savedData);
        this.groupName = loadableData.savedGroupName;
        this.activeScene = loadableData.currentSceneName;

            for(int i = 0; i < loadableData.heroRoles.Length; i++)  
            {
                SelectableHero newHero = new SelectableHero();
                newHero.heroName = loadableData.heroNames[i];
           
                newHero.heroRole = loadableData.heroRoles[i];
            newHero.name = newHero.heroRole; //showing rolename on Hierachy?

            this.partyHeroes.Add(newHero);
            }
        
        }

        public void LoadSaveNames()
        {
        string filePath = Application.persistentDataPath + "/savedNames.json";
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
        string filePath = Application.persistentDataPath + "/savedNames.json";
        File.WriteAllText(filePath, saveableNames);
        }

    private void DeleteSave(int saveSlot)
    {
        saveSlots[saveSlot] = "";
    }

}

    [Serializable]
    class GameSavedData
    {
        public string savedGroupName;
        public string currentSceneName;

    public string[] heroNames = new string[5];
    public string[] heroRoles = new string[5];

    public List<SavedHero> savedHeroes = new List<SavedHero>();
    
    }


    //Saveable list of names for LoadGame - buttons
    [Serializable]
    class SavedNamesData
    {
        public string save1, save2, save3, save4, save0;
    }

[Serializable]
class SavedHero
{
    public string heroName;
    public string heroRole;

    public int currentHealth;
    public int maxHealth;

    public int currentActionPoints;
    public int maxActionPoints;

    public int atk;
    public int def;
    public int fate;
    public int exp;

    public int maxCombatItems;
    public int maxItems;

    public List<string> savedCombatItems;
    public List<string> savedJourneyItems;
    public List<string> savedHiddenItems;
}
