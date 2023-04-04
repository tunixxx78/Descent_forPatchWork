using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.UI;

public class LoadGameButton : MonoBehaviour
{

    public int buttonId;
    public string buttonDisplayName;
    public SavingSystem savingSystem;

    public GameObject newGamePanel;

    

    [SerializeField] private TMP_Text TM_loadGameName;

    private void Awake()
    {
        savingSystem = FindObjectOfType<SavingSystem>();
    }

    void Start()
    {
        LoadSavedNames();
    }


    private void LoadSavedNames()
    {
        buttonDisplayName = savingSystem.saveSlots[buttonId];
        if (buttonDisplayName == null || buttonDisplayName == "")
        {
            buttonDisplayName = "Start New Game";
        }
        TM_loadGameName.text = buttonDisplayName;

    }

    public void StartPlaying()
    {
        //jos on jo joku nimi(ei start new game) eli kohtaan on tallennettu jo peli
        if (buttonDisplayName != "Start New Game")
        {
            //loads save based on buttonId and changes to proper scene
            savingSystem.Load(buttonId);
            SceneManager.LoadScene(savingSystem.activeScene);
        }
        //tehd‰‰n uusi peli
        else
        {
            //Pit‰‰ laittaa avaamaan paneeli, NewGamePanel
            savingSystem.activeSaveSlot = this.buttonId;
            newGamePanel.SetActive(true);
        }
    }
}
