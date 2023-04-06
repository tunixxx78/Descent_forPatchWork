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
        foreach (SelectableHero hero in selectableHeroesList)
        {
            if (hero.IsSelected())
            {
                savingSystem.partyHeroes.Add(hero);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
