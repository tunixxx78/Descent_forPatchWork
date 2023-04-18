using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    
    SavingSystem savingSystem;
    [SerializeField] SelectableHero[] selectableHeroesArray = new SelectableHero[4];   
    [SerializeField] List<SelectHeroButton> selectHeroButtons;
    void Start()
    {
        //Get names of saveslots
        savingSystem= FindObjectOfType<SavingSystem>();
        savingSystem.LoadSaveNames();
        //create array for heroes
         
    }



    public void AddSelectedHeroes()
    {
        //debuglog, print sizes of lists/arraysLength);
        Debug.Log("selectableHeroesList koko: "+ selectableHeroesArray.Length);
        //add hero to savingSystem's list, not yet in list
        for(int i = 0; i < selectableHeroesArray.Length;i++)
        {
            //if here in index is selected 
            if (selectableHeroesArray[i].IsSelected())
            {   //no same heroes multiple times)
                //if (!savingSystem.partyHeroes.Contains(selectableHeroesArray[i]))
                //{
                    Debug.Log("lisäätään sankaria " + selectableHeroesArray[i].heroName);
                    savingSystem.partyHeroes[i] 
                    = selectableHeroesArray[i]; //add hero to proper index in partyHeroes-Array
                //}
                
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
