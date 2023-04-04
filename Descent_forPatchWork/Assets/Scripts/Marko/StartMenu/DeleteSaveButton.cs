using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeleteSaveButton : MonoBehaviour
{

    public int buttonId;
    private string savedNameText = "";
    [SerializeField] TMP_Text TM_buttonText;
    SavingSystem savingSystem;
    

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteSave()
    {
        savingSystem.saveSlots[buttonId] = null;
        savingSystem.groupName= null;
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        savedNameText = savingSystem.saveSlots[buttonId];
        Debug.Log("tallennetussa nimessä " + savedNameText);
        if (savedNameText == null || savedNameText == "") {
            savedNameText = "Empty Save";
        }
        else
        {
            TM_buttonText.text = "Delete "+ savedNameText;
        }
        Debug.Log("savedin namen teksti " + savedNameText);
        Debug.Log("tmp teksti " + TM_buttonText.text);
    }
}
