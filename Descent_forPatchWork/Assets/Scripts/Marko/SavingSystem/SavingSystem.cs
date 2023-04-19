using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Unity.UI;

public class SavingSystem : MonoBehaviour
{

    public static SavingSystem savingSystem;


    public string[] saveSlots = new string[5];
    public string groupName = "Nameless Group";
    public int activeSaveSlot;
    public string activeScene;
    public SelectableHero[] partyHeroes = new SelectableHero[4];
    

    //MAKING SINGLETON
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
        }

        string saveableData = JsonUtility.ToJson(gameSessionData,true);
        string filePath = Application.persistentDataPath + "/save" + activeSaveSlot.ToString() + ".json";
        Debug.Log("saving to: " + filePath);

        File.WriteAllText(filePath, saveableData);
    }

        public void LoadGame(int saveName)
        {
            //TODO NEW universal savepath(for macs and pcs)
            string filePath = Application.persistentDataPath + "/save" + saveName.ToString() + ".json";
            if (File.Exists(filePath))
            {
                        string savedData = File.ReadAllText(filePath);

                        GameSavedData loadableData = JsonUtility.FromJson<GameSavedData>(savedData);
                        this.groupName = loadableData.savedGroupName;
                        this.activeScene = loadableData.currentSceneName;

                //foreach (SavedHero sHero in loadableData.savedHeroes)
                for (int i = 0; i < loadableData.savedHeroes.Length; i++)
                {
                    SavedHero sHero = loadableData.savedHeroes[i];
                    if (sHero == null)
                    {

                        GameObject heroGameObject = new GameObject();

                        heroGameObject.AddComponent<SelectableHero>();
                        heroGameObject.AddComponent<MeshRenderer>();
                        //TODO add all other components(material, meshrenderer ..) too? or not?


                        SelectableHero hero = heroGameObject.GetComponent<SelectableHero>();
                        hero.name = sHero.heroRole; //role shows in hierarchy if this works
                        hero.heroName = sHero.heroName;
                        hero.heroRole = sHero.heroRole;
                        hero.currentHealth = sHero.currentHealth;
                        hero.maxHealth = sHero.maxHealth;
                        hero.currentActionPoints = sHero.currentActionPoints;
                        hero.maxActionPoints = sHero.maxActionPoints;

                        hero.plrStrength = sHero.strength;
                        hero.plrShield = sHero.shield;
                        hero.fate = sHero.fate;
                        hero.exp = sHero.exp;
                        hero.plrIndex = sHero.plrIndex;
                        hero.maxCombatItems = sHero.maxCombatItems;
                        hero.maxItems = sHero.maxItems;

                        foreach (string sCombatCID in sHero.savedCombatItems)
                        {
                            //TODO search for CombatItem by ID..
                        }
                        foreach (string sJourneyCID in sHero.savedJourneyItems)
                        {
                            //TODO journeycard by id
                        }
                        foreach (string hiddenCID in sHero.savedHiddenItems)
                        {
                            //TODO hidden cards by id..
                        }
                        Debug.Log("kokeillaan lisätä heroa");
                        partyHeroes[i] = hero;
                        Debug.Log("heroja on: " + partyHeroes.Length);
                    }
                }
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

    //public List<SavedHero> savedHeroes = new List<SavedHero>();
    public SavedHero[]  savedHeroes = new SavedHero[4];
    
    }


    //Saveable list of names for LoadGame - buttons
    [Serializable]
    class SavedNamesData
    {
        public string  save0, save1, save2, save3, save4;
    }

[Serializable]
class SavedHero
{
    public int plrIndex;
    public string heroName;
    public string heroRole;

    public int currentHealth;
    public int maxHealth;

    public int currentActionPoints;
    public int maxActionPoints;

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
