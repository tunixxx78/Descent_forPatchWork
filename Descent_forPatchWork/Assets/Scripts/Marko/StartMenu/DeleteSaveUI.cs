using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DeleteSaveUI : MonoBehaviour
{
    public int deletableSaveId;
    SavingSystem savingSystem;
    public string deleteText = "Do you really want to delete ";
    [SerializeField] TMP_Text TM_deletionText;
    [SerializeField] DeleteSaveButton activatedButton;

    private void Awake()
    {
        savingSystem = FindObjectOfType<SavingSystem>();
        TM_deletionText.text = "ÖRRÖRÖR";
    }

    public void ActivateDeletePanel()
    {
        
        UpdateText();
    }

   public void SetActivatedButton(DeleteSaveButton button)
    {
        activatedButton = button;
    }

    public void DeleteSave()
    {
        savingSystem.saveSlots[deletableSaveId] = null;
        Debug.Log("savingsystemin teksti " 
            + savingSystem.saveSlots[deletableSaveId]);
        savingSystem.groupName = null;
        Debug.Log("saving system groupname: " + savingSystem.groupName);
        savingSystem.SaveSavedNames();
        activatedButton.UpdateButtonText();
    }

    private void UpdateText()
    {

        Debug.Log("Löytyi tekstikenttä");
        TM_deletionText.text = deleteText 
            + savingSystem.saveSlots[deletableSaveId] + " ?";                          
    }

}
