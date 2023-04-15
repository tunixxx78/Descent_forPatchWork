using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//<<<<<<< HEAD
using Unity.UI;
using System;
using Unity.VisualScripting;
//=======
//using Unity.UI;
//>>>>>>> origin/main

public class LoadGameButton : MonoBehaviour
{

    public int buttonId;
    public string buttonDisplayName;
    public SavingSystem savingSystem;

    public GameObject newGamePanel;
    public string defaultEmpty = "start new game";

    

    [SerializeField] private TMP_Text TM_loadGameName;

    private void Awake()
    {
        savingSystem = FindObjectOfType<SavingSystem>();       
    }

    private void OnEnable()
    {
        Debug.Log("updated load game button names - _ - _");
        LoadSavedNames();
    }

    private void LoadSavedNames()
    {
        buttonDisplayName = savingSystem.saveSlots[buttonId];
//<<<<<<< HEAD
        //(Debug.Log("tallennetussa nimess? " + buttonId + ": "+ buttonDisplayName);
//=======
        Debug.Log("tallennetussa nimess? "+ buttonDisplayName);
//>>>>>>> origin/main
        if (buttonDisplayName == null || buttonDisplayName == "")
        {
            buttonDisplayName = defaultEmpty;
        }
        TM_loadGameName.text = buttonDisplayName;

    }


    public void StartPlaying()
    {
        //jos on jo joku nimi(ei start new game) eli kohtaan on tallennettu jo peli
        if (buttonDisplayName != defaultEmpty)
        {
            //loads save based on buttonId and changes to proper scene
            savingSystem.LoadGame(buttonId);
            SceneManager.LoadScene(savingSystem.activeScene);
        }
        //tehd??n uusi peli
        else
        {
//<<<<<<< HEAD
//=======
            //Pit?? laittaa avaamaan paneeli, NewGamePanel
//>>>>>>> origin/main
            savingSystem.activeSaveSlot = this.buttonId;
            newGamePanel.SetActive(true); //activate NewGamePanel in Canvas
        }
    }
}
