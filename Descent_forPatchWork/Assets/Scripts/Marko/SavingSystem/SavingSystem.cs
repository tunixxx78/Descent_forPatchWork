using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    public string[] saveSlots = new string[5];
    public string groupName = "Nameless Group";
    public int activeSaveSlot;
    public string activeScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream stream = File.Create(Application.persistentDataPath + "/" + activeSaveSlot + ".dat"))
        {
            GameSavedData data = new GameSavedData();
            data.savedGroupName = groupName;
            data.currentSceneName = activeScene;

            bf.Serialize(stream, data);
        }
    }

        public void Load(int saveName)
        {
            if (File.Exists(Application.persistentDataPath + "/save" + saveName + ".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream stream = File.Open(Application.persistentDataPath + "/"
                    + saveName + ".dat", FileMode.Open))
                {
                    GameSavedData data = (GameSavedData)bf.Deserialize(stream);
                    //stream.Close(); //inside using-sentence, closes automatically at end

                    groupName = data.savedGroupName;
                    activeSaveSlot = saveName;
                    activeScene = data.currentSceneName;

                }

            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void LoadSaveNames()
        {
            if (File.Exists(Application.persistentDataPath + "/" + "savedNames" + ".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + "savedNames" + ".dat", FileMode.Open);
                SavedNameData data = (SavedNameData)bf.Deserialize(file);
                file.Close();

                saveSlots[0] = data.save0;
                saveSlots[1] = data.save1;
                saveSlots[2] = data.save2;
                saveSlots[3] = data.save3;
                saveSlots[4] = data.save4;
            }
        }
        public void SaveSavedNames()
        {
            Debug.Log("saving savenames");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(
                Application.persistentDataPath + "/" + "savedNames" + ".dat");

            SavedNameData data = new SavedNameData();
            data.save0 = saveSlots[0];
            data.save1 = saveSlots[1];
            data.save2 = saveSlots[2];
            data.save3 = saveSlots[3];
            data.save4 = saveSlots[4];

            bf.Serialize(file, data);
            file.Close();
        }

        //public string GetSaveID(string savedName)
        //{
        //    string saveID = manager.name;
        //    return saveID;
        //}
    }

    [Serializable]
    class GameSavedData
    {
        public string savedGroupName;
        public string currentSceneName;
    }


    //Saveable list of names for LoadGame - buttons
    [Serializable]
    class SavedNameData
    {
        public string save1, save2, save3, save4, save0;
    }
