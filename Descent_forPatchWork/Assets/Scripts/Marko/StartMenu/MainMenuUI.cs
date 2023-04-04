using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    SavingSystem savingSystem;
    void Start()
    {
        //Get names of saveslots
        savingSystem= FindObjectOfType<SavingSystem>();
        savingSystem.LoadSaveNames();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
