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
            gameSessionData.heroNames[i] = partyHeroes[i].heroName;
            gameSessionData.heroRoles[i]= partyHeroes[i].heroRole;
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
                this.partyHeroes[i].heroName = loadableData.heroNames[i];
                this.partyHeroes[i].heroRole= loadableData.heroRoles[i];
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

    public string hero0Name;
    public string hero0Role;

    public string hero1Name;
    public string hero1Role;

    public string[] heroNames = new string[5];
    public string[] heroRoles = new string[5];
    
    }


    //Saveable list of names for LoadGame - buttons
    [Serializable]
    class SavedNamesData
    {
        public string save1, save2, save3, save4, save0;
    }
