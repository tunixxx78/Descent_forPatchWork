using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameCreator : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField groupNameInput;
    SavingSystem savingSystem;
    private void Start()
    {
        savingSystem = FindObjectOfType<SavingSystem>();
    }
    public void SetGroupName()
    {
        //oletusnimi tyhille nimikentille nameless group + ´paikkanumero
        if(groupNameInput.text == "")
        {
            savingSystem.groupName = 
                "Nameless Group " + (1 + savingSystem.activeSaveSlot);
        }

        else { savingSystem.groupName = groupNameInput.text; }

        //saving party first time..
        savingSystem.saveSlots[savingSystem.activeSaveSlot]
            = savingSystem.groupName;
        savingSystem.SaveSavedNames();
        savingSystem.SaveGame();
    }


}
