using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeleteSaveButton : MonoBehaviour
{

    public int buttonId;
    public TMP_Text TM_buttonText;
    SavingSystem savingSystem;
    [SerializeField] DeleteSaveUI deleteSaveUI;
    
    

    private void Awake()
    {
        savingSystem = FindObjectOfType<SavingSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        TM_buttonText.text = buttonId.ToString();
        UpdateButtonText();
    }

    public void SetDeletableId()
    {
        deleteSaveUI.deletableSaveId = buttonId;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Deletes only the name from the savedNames-list
    //(true save �n saveslot.json will just get overwritten
    //when a new game is created on that slot)
    public void DeleteSave()
    {
        //savingSystem.saveSlots[buttonId] = null;
        //Debug.Log("savingsystemin teksti " + savingSystem.saveSlots[buttonId]);
        //savingSystem.groupName= null;
        //Debug.Log("saving system groupname: " + savingSystem.groupName);
        //savingSystem.SaveSavedNames();
        
        UpdateButtonText();
    }

    public void UpdateButtonText()
    {
        if (savingSystem.saveSlots[buttonId] == null || savingSystem.saveSlots[buttonId] == "") {

            TM_buttonText.text = "Empty Save";
            GetComponent<Button>().interactable = false;

        }
        else
        {
            TM_buttonText.text = "Delete "+ savingSystem.saveSlots[buttonId];
        }

        Debug.Log("tmp teksti " + TM_buttonText.text);
    }
}
