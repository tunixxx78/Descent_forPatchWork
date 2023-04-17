using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameCreator : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField groupNameInput;

    [SerializeField] TMP_Text partyNameHeroSelectText;
    SavingSystem savingSystem;
    private void Start()
    {
        savingSystem = FindObjectOfType<SavingSystem>();
    }

    public void SetGroupName()
    {
        //default name for empty field: nameless group + number
        if(groupNameInput.text == "")
        {
            savingSystem.groupName = 
                "Nameless Group " + (1 + savingSystem.activeSaveSlot);
        }

        else { savingSystem.groupName = groupNameInput.text; }

        //saving party first time..
        //Debug.Log("group's name is " + savingSystem.groupName);
        //savingSystem.saveSlots[savingSystem.activeSaveSlot]
        //    = savingSystem.groupName;
        
        //update name in hero selection panel
        partyNameHeroSelectText.text = savingSystem.groupName;
        EmptyNameField();
    }

    public void EmptyNameField()
    {
        groupNameInput.text = "";  //wanna empty name input field..
    }

    public void StartNewGame(string firstScene)
    {

        savingSystem.saveSlots[savingSystem.activeSaveSlot]
            = savingSystem.groupName;
        //go to first scene of adventuring - map scene??

        savingSystem.activeScene = firstScene;
        savingSystem.SaveSavedNames();
        savingSystem.SaveGame();
        Debug.Log("aktiivinen skene on "+ savingSystem.activeScene);
        SceneManager.LoadScene(firstScene);
    }

}
