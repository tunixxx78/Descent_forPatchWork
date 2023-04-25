using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
       // Debug.Log("updated load game button names - _ - _");
        LoadSavedNames();
    }

    private void LoadSavedNames()
    {
        buttonDisplayName = savingSystem.saveSlots[buttonId];

        Debug.Log("tallennetussa nimess? "+ buttonDisplayName);

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
            //loads save based on buttonId and changes to proper scene(not used here)
            //savingSystem.LoadGame(buttonId);

            //This just sets the propper Button ID/saveslot number 
            //so Dataholder whichever knows which slot to load
            savingSystem.LoadLoadingSettings(buttonId);
            //Go to next scene
           SceneManager.LoadScene(savingSystem.activeScene);
        }
        //tehd??n uusi peli
        else
        {

            savingSystem.activeSaveSlot = this.buttonId;
            newGamePanel.SetActive(true); //activate NewGamePanel in Canvas
        }
    }
}
