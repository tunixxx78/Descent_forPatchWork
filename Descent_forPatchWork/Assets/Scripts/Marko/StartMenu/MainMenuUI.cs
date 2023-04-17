using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    
    SavingSystem savingSystem;
    [SerializeField] List<SelectableHero> selectableHeroesList;    
    [SerializeField] List<SelectHeroButton> selectHeroButtons;
    void Start()
    {
        //Get names of saveslots
        savingSystem= FindObjectOfType<SavingSystem>();
        savingSystem.LoadSaveNames();
    }



    public void AddSelectedHeroes()
    {
        //debuglog, print sizes of lists/arrays
        Debug.Log("paryheroes koko: " + savingSystem.partyHeroes.Count);
        Debug.Log("selectableHeroesList koko: "+ selectableHeroesList.Count);
        //add hero to savingSystem's list, not yet in list
        for(int i = 0; i < selectableHeroesList.Count;i++)
        {

            if (selectableHeroesList[i].IsSelected())
            {   //no same heroes multiple times)
                if (!savingSystem.partyHeroes.Contains(selectableHeroesList[i]))
                {
                    Debug.Log("lisäätään sankaria " + selectableHeroesList[i].heroName);
                    savingSystem.partyHeroes.Add(selectableHeroesList[i]);
                }
                
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
