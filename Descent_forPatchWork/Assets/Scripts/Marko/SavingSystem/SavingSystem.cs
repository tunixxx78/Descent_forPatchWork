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

        if (partyHeroes.Count >= 1)
        {
            gameSessionData.hero0Name = partyHeroes[0].name;
            gameSessionData.hero0Role = partyHeroes[0].role;
        }
        //if (partyHeroes[1])
        //{
        //    gameSessionData.hero1Name = partyHeroes[1].name;
        //    gameSessionData.hero1Role = partyHeroes[1].role;
        //}


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

    
    }


    //Saveable list of names for LoadGame - buttons
    [Serializable]
    class SavedNamesData
    {
        public string save1, save2, save3, save4, save0;
    }
